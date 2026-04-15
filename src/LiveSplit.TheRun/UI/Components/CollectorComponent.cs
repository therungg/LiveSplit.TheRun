using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

using LiveSplit.Model;

namespace LiveSplit.UI.Components;

public class CollectorComponent : LogicComponent
{
    public override string ComponentName => LastSyncTime != null
        ? "therun.gg (synced " + FormatTimeAgo(LastSyncTime.Value) + ")"
        : "therun.gg";

    private LiveSplitState State { get; set; }
    private CollectorSettings Settings { get; set; }

    private readonly HttpClient httpClient;
    private UploadToast toast;
    private DateTime? LastSyncTime;
    private CancellationTokenSource liveCts;

    private string SplitWebhookUrl => "https://dspc6ekj2gjkfp44cjaffhjeue0fbswr.lambda-url.eu-west-1.on.aws/";
    private string FileUploadBaseUrl => "https://2uxp372ks6nwrjnk6t7lqov4zu0solno.lambda-url.eu-west-1.on.aws/";

    private string GameName = "";
    private string CategoryName = "";

    private bool TimerPaused = false;
    private bool WasJustResumed = false;
    private TimeSpan CurrentPausedTime = TimeSpan.Zero;
    private TimeSpan TimePausedBeforeResume = TimeSpan.Zero;

    public CollectorComponent(LiveSplitState state)
    {
        State = state;
        Settings = new CollectorSettings();

        httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(15);
        httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "cross-site");
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Disposition", "attachment");

        SetGameAndCategory();

        State.OnStart += HandleSplit;
        State.OnSplit += HandleSplit;
        State.OnSkipSplit += HandleSplit;
        State.OnUndoSplit += HandleSplit;
        State.OnUndoAllPauses += HandleSplit;

        State.OnPause += HandlePause;
        State.OnResume += HandleResume;
        State.OnReset += HandleReset;
    }

    public async Task UpdateSplitsState()
    {
        liveCts?.Cancel();
        liveCts?.Dispose();
        liveCts = new CancellationTokenSource();
        var token = liveCts.Token;

        object returnData = buildLiveRunData();

        var serializer = new JavaScriptSerializer();
        var content = new StringContent(serializer.Serialize(returnData));

        await httpClient.PostAsync(SplitWebhookUrl, content, token);
    }

    private void SetGameAndCategory()
    {
        GameName = State.Run.GameName;
        CategoryName = State.Run.CategoryName;
    }

    private object buildLiveRunData()
    {
        IRun run = State.Run;
        TimeSpan? currentTime = State.CurrentTime[State.CurrentTimingMethod];
        List<object> runData = [];

        var metaData = new
        {
            game = GameName,
            category = CategoryName,
            platform = run.Metadata.PlatformName,
            region = run.Metadata.RegionName,
            emulator = run.Metadata.UsesEmulator,
            variables = run.Metadata.VariableValueNames
        };

        foreach (ISegment segment in run)
        {
            List<object> comparisons = [];

            foreach (string key in segment.Comparisons.Keys)
            {
                comparisons.Add(new
                {
                    name = key,
                    time = ConvertTime(segment.Comparisons[key])
                });
            }

            runData.Add(new
            {
                name = segment.Name,
                splitTime = ConvertTime(segment.SplitTime),
                pbSplitTime = ConvertTime(segment.PersonalBestSplitTime),
                bestPossible = ConvertTime(segment.BestSegmentTime),
                comparisons = comparisons
            });
        }

        return new
        {
            metadata = metaData,
            currentTime = ConvertTime(State.CurrentTime),
            currentSplitName = State.CurrentSplit != null ? State.CurrentSplit.Name : "",
            currentSplitIndex = State.CurrentSplitIndex,
            timingMethod = State.CurrentTimingMethod,
            currentDuration = State.CurrentAttemptDuration.TotalMilliseconds,
            startTime = State.AttemptStarted.Time.ToUniversalTime(),
            endTime = State.AttemptEnded.Time.ToUniversalTime(),
            uploadKey = Settings.UploadKey,
            isPaused = TimerPaused,
            isGameTimePaused = State.IsGameTimePaused,
            gameTimePauseTime = State.GameTimePauseTime,
            totalPauseTime = State.PauseTime,
            currentPauseTime = TimePausedBeforeResume,
            timePausedAt = State.TimePausedAt.TotalMilliseconds,
            wasJustResumed = WasJustResumed,
            currentComparison = State.CurrentComparison,
            runData = runData
        };
    }

    private double? ConvertTime(Time time)
    {
        if (time[State.CurrentTimingMethod] == null)
        {
            return null;
        }

        var timeSpan = (TimeSpan)time[State.CurrentTimingMethod];

        return timeSpan.TotalMilliseconds;
    }

    public void HandlePause(object sender, object e)
    {
        TimerPaused = true;
        HandleSplit(sender, e);
    }

    public void HandleResume(object sender, object e)
    {
        TimePausedBeforeResume = (TimeSpan)(State.PauseTime - CurrentPausedTime);
        CurrentPausedTime = (TimeSpan)State.PauseTime;
        TimerPaused = false;
        WasJustResumed = true;

        HandleSplit(sender, e);
    }

    public async void HandleSplit(object sender, object e)
    {
        SetGameAndCategory();
        if (!AreSplitsValid() || !Settings.IsLiveTrackingEnabled)
        {
            return;
        }

        try
        {
            await UpdateSplitsState();

            if (State.CurrentSplitIndex == State.Run.Count)
            {
                await UploadSplits();
            }
        }
        catch { }

        WasJustResumed = false;
    }

    public async void HandleReset(object sender, TimerPhase value)
    {
        SetGameAndCategory();
        if (!AreSplitsValid())
        {
            return;
        }

        try
        {
            if (Settings.IsLiveTrackingEnabled)
            {
                await UpdateSplitsState();
            }

            if (Settings.IsUploadOnResetEnabled)
            {
                await UploadSplits();
            }
        }
        catch { }
    }

    private bool AreSplitsValid()
    {
        return GameName != "" && CategoryName != "" && Settings.UploadKey.Length == 36;
    }

    public async Task UploadSplits()
    {
        if (!Settings.IsStatsUploadingEnabled)
        {
            return;
        }

        ShowToast(t => t.ShowUploading());

        try
        {
            await UploadSplitsCore();
            LastSyncTime = DateTime.Now;
            ShowToast(t => t.ShowSuccess());
        }
        catch
        {
            ShowToast(t => t.ShowError());
            throw;
        }
    }

    private async Task UploadSplitsCore()
    {
        string fileName = HttpUtility.UrlEncode(GameName) + "-" + HttpUtility.UrlEncode(CategoryName) + ".lss";
        string fileUploadUrl = FileUploadBaseUrl + "?filename=" + fileName + "&uploadKey=" + Settings.UploadKey;

        HttpResponseMessage result = await httpClient.GetAsync(fileUploadUrl);
        result.EnsureSuccessStatusCode();
        string responseBody = await result.Content.ReadAsStringAsync();

        var ser = new JavaScriptSerializer();
        Dictionary<string, string> jsonObj = ser.Deserialize<Dictionary<string, string>>(responseBody);

        string url = HttpUtility.UrlDecode(jsonObj["url"]);
        string correctlyEncodedUrl = EncodeUrl(url);

        var content = new StringContent(XmlRunAsString());
        content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

        HttpResponseMessage putResult = await httpClient.PutAsync(correctlyEncodedUrl, content);
        putResult.EnsureSuccessStatusCode();
    }

    private void ShowToast(Action<UploadToast> action)
    {
        if (!Settings.IsToastEnabled || State.Form == null || State.Form.IsDisposed)
        {
            return;
        }

        void invoke()
        {
            if (toast == null || toast.IsDisposed)
            {
                toast = new UploadToast(State.Form);
            }

            action(toast);
        }

        if (State.Form.InvokeRequired)
        {
            State.Form.Invoke((Action)invoke);
        }
        else
        {
            invoke();
        }
    }

    private string EncodeUrl(string url)
    {
        string[] urlParts = url.Split('&').Select(urlPart => urlPart.StartsWith("X-Amz-Credential") || urlPart.StartsWith("X-Amz-Security-Token") || urlPart.StartsWith("X-Amz-SignedHeaders") ? HttpUtility.UrlEncode(urlPart).Replace("%3d", "=") : urlPart).ToArray();

        string newUrl = string.Join("&", urlParts).Replace(GameName, HttpUtility.UrlEncode(GameName)).Replace(CategoryName, HttpUtility.UrlEncode(CategoryName));
        string username = newUrl.Replace("https://splits-bucket-main.s3.eu-west-1.amazonaws.com/", "").Split('/')[0];

        return newUrl.Replace(username, HttpUtility.UrlEncode(username));
    }

    private string XmlRunAsString()
    {
        var runSaver = new Model.RunSavers.XMLRunSaver();
        using var stream = new System.IO.MemoryStream();

        runSaver.Save(State.Run, stream);

        stream.Position = 0;

        var doc = new XmlDocument();
        doc.PreserveWhitespace = true;
        doc.Load(stream);

        StripDataForUpload(doc, Settings.IsLayoutPathUploadEnabled);

        return doc.OuterXml;
    }

    private static void StripDataForUpload(XmlDocument doc, bool keepLayoutPath)
    {
        var run = doc.DocumentElement;

        var gameIcon = run["GameIcon"];
        if (gameIcon != null)
        {
            gameIcon.InnerText = "";
        }

        if (!keepLayoutPath)
        {
            var layoutPath = run["LayoutPath"];
            if (layoutPath != null)
            {
                layoutPath.InnerText = "";
            }
        }

        var segments = run["Segments"];
        if (segments != null)
        {
            foreach (XmlElement segment in segments.GetElementsByTagName("Segment"))
            {
                var icon = segment["Icon"];
                if (icon != null)
                {
                    icon.InnerText = "";
                }
            }
        }
    }

    private static string FormatTimeAgo(DateTime time)
    {
        TimeSpan ago = DateTime.Now - time;

        if (ago.TotalSeconds < 60)
            return "just now";
        if (ago.TotalMinutes < 60)
            return (int)ago.TotalMinutes + "m ago";
        if (ago.TotalHours < 24)
            return (int)ago.TotalHours + "h ago";

        return time.ToString("MMM d HH:mm");
    }

    public override void Dispose()
    {
        State.OnStart -= HandleSplit;
        State.OnSplit -= HandleSplit;
        State.OnSkipSplit -= HandleSplit;
        State.OnUndoSplit -= HandleSplit;
        State.OnUndoAllPauses -= HandleSplit;

        State.OnPause -= HandlePause;
        State.OnResume -= HandleResume;
        State.OnReset -= HandleReset;

        try
        {
            SetGameAndCategory();
            if (AreSplitsValid() && Settings.IsStatsUploadingEnabled && !Settings.IsUploadOnResetEnabled)
            {
                ShowToast(t => t.ShowUploading());
                using var disposeCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                var uploadTask = Task.Run(() => UploadSplitsCore(), disposeCts.Token);
                if (uploadTask.Wait(TimeSpan.FromSeconds(5)))
                {
                    LastSyncTime = DateTime.Now;
                    ShowToast(t => t.ShowSuccess());
                }
            }
        }
        catch { }

        liveCts?.Dispose();
        toast?.Dispose();
        httpClient.Dispose();
    }

    public override XmlNode GetSettings(XmlDocument document)
    {
        return Settings.GetSettings(document);
    }

    public override Control GetSettingsControl(LayoutMode mode)
    {
        Settings.Mode = mode;
        return Settings;
    }

    public override void SetSettings(XmlNode settings)
    {
        Settings.SetSettings(settings);
    }

    public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
}
