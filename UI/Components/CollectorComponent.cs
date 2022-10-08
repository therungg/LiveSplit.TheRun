using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public class CollectorComponent : LogicComponent
    {
        public override string ComponentName => "therun.gg";

        private LiveSplitState State { get; set; }
        private CollectorSettings Settings { get; set; }

        private readonly HttpClient httpClient;

        private string SplitWebhookUrl => "https://therun.gg/api/livesplit";
        private string FileUploadBaseUrl => "https://2uxp372ks6nwrjnk6t7lqov4zu0solno.lambda-url.eu-west-1.on.aws/";

        private string GameName = "";
        private string CategoryName = "";

        public CollectorComponent(LiveSplitState state)
        {
            State = state;
            Settings = new CollectorSettings();

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "cross-site");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Disposition", "attachment");

            GameName = State.Run.GameName;
            CategoryName = State.Run.CategoryName;

            State.OnStart += HandleSplit;
            State.OnSplit += HandleSplit;
            State.OnSkipSplit += HandleSplit;
            State.OnUndoSplit += HandleSplit;

            State.OnReset += HandleReset;
        }

        public async Task UpdateSplitsState()
        {
            object returnData = buildLiveRunData();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var content = new StringContent(serializer.Serialize(returnData));

            await httpClient.PostAsync(SplitWebhookUrl, content);
        }
        
        private object buildLiveRunData()
        {
            var run = State.Run;

            TimeSpan? CurrentTime = State.CurrentTime[State.CurrentTimingMethod];

            var MetaData = new
            {
                game = GameName,
                category = CategoryName,
                platform = run.Metadata.PlatformName,
                region = run.Metadata.RegionName,
                emulator = run.Metadata.UsesEmulator
            };

            List<object> runData = new List<object>();

            foreach (var segment in run)
            {
                runData.Add(new
                {
                    name = segment.Name,
                    splitTime = segment.SplitTime,
                    pbSplitTime = segment.PersonalBestSplitTime,
                    bestPossible = segment.BestSegmentTime,
                    comparisons = segment.Comparisons
                });
            }

            return new
            {
                metadata = MetaData,
                currentTime = State.CurrentTime,
                currentSplitName = State.CurrentSplit != null ? State.CurrentSplit.Name : "",
                timingMethod = State.CurrentTimingMethod,
                currentDuration = State.CurrentAttemptDuration,
                startTime = State.AttemptStarted,
                endTime = State.AttemptEnded,
                uploadKey = Settings.Path,
                runData = runData
            };
        }

        // TODO: Log or tell user when splits are invalid or when an error occurs. Don't just continue silently.
        public async void HandleSplit(object sender, object e)
        {
            if (!AreSplitsValid()) return;

            try
            {
                await UpdateSplitsState();
            } catch { }
        }

        public async void HandleReset(object sender, TimerPhase value)
        {
            if (!AreSplitsValid()) return;

            try
            {
                await UpdateSplitsState();
                await UploadSplits();
            }
            catch { }
        }

        private bool AreSplitsValid()
        {
            return GameName != "" && CategoryName != "" && Settings.Path.Length == 36;
        }

        public async Task UploadSplits()
        {
            string UploadKey = Settings.Path;
            string FileName = HttpUtility.UrlEncode(GameName) + "-" + HttpUtility.UrlEncode(CategoryName) + ".lss";
            string FileUploadUrl = FileUploadBaseUrl + "?filename=" + FileName + "&uploadKey=" + UploadKey;

            var result = await httpClient.GetAsync(FileUploadUrl);
            var responseBody = await result.Content.ReadAsStringAsync();

            // Something went wrong, but the backend will handle the error, LiveSplit should just keep going.
            // Probably the upload key was not filled in.
            if (!result.IsSuccessStatusCode) return;

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var JSONObj = ser.Deserialize<Dictionary<string, string>>(responseBody);

            string url = HttpUtility.UrlDecode(JSONObj["url"]);
            string[] urlParts = url.Split('&').Select(urlPart => urlPart.StartsWith("X-Amz-Credential") || urlPart.StartsWith("X-Amz-Security-Token") || urlPart.StartsWith("X-Amz-SignedHeaders") ? HttpUtility.UrlEncode(urlPart).Replace("%3d", "=") : urlPart).ToArray();

            string newUrl = string.Join("&", urlParts).Replace(GameName, HttpUtility.UrlEncode(GameName)).Replace(CategoryName, HttpUtility.UrlEncode(CategoryName));

            StringContent content = new StringContent(XmlRunAsString());
            content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

            var res = await httpClient.PutAsync(newUrl, content);
        }

        private string XmlRunAsString()
        {
            Model.RunSavers.XMLRunSaver runSaver = new Model.RunSavers.XMLRunSaver();
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            runSaver.Save(State.Run, stream);

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public override void Dispose()
        {
            State.OnStart -= HandleSplit;
            State.OnSplit -= HandleSplit;
            State.OnSkipSplit -= HandleSplit;
            State.OnUndoSplit -= HandleSplit;
            State.OnReset -= HandleReset;

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

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) {  }
    }
}
