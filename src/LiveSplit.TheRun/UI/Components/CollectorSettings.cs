using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components;

public partial class CollectorSettings : UserControl
{
    public LayoutMode Mode { get; set; }

    private readonly string UploadKeyFile = "Livesplit.TheRun/uploadkey.txt";
    private readonly string UploadKeyFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public string UploadKey => GetUploadKey();
    public string ValidatedUsername { get; private set; }

    public string Path { get; set; }
    public bool IsStatsUploadingEnabled { get; set; }
    public bool IsUploadOnResetEnabled { get; set; }
    public bool IsLiveTrackingEnabled { get; set; }
    public bool IsToastEnabled { get; set; }
    public bool IsLayoutPathUploadEnabled { get; set; }

    public enum ConnectionStatus { None, Validating, Connected, Error }
    public ConnectionStatus Status { get; private set; } = ConnectionStatus.None;

    public CollectorSettings()
    {
        InitializeComponent();

        chkStatsUploadEnabled.DataBindings.Add("Checked", this, "IsStatsUploadingEnabled",
            false, DataSourceUpdateMode.OnPropertyChanged);
        chkUploadOnReset.DataBindings.Add("Checked", this, "IsUploadOnResetEnabled",
            false, DataSourceUpdateMode.OnPropertyChanged);
        chkLiveTrackingEnabled.DataBindings.Add("Checked", this, "IsLiveTrackingEnabled",
            false, DataSourceUpdateMode.OnPropertyChanged);
        chkToastEnabled.DataBindings.Add("Checked", this, "IsToastEnabled",
            false, DataSourceUpdateMode.OnPropertyChanged);
        chkLayoutPathUpload.DataBindings.Add("Checked", this, "IsLayoutPathUploadEnabled",
            false, DataSourceUpdateMode.OnPropertyChanged);

        Path = "";
        IsStatsUploadingEnabled = true;
        IsUploadOnResetEnabled = true;
        IsLiveTrackingEnabled = true;
        IsToastEnabled = true;
        IsLayoutPathUploadEnabled = false;
    }

    public void SetSettings(XmlNode node)
    {
        var element = (XmlElement)node;

        Version version = SettingsHelper.ParseVersion(element["Version"]);
        Path = SettingsHelper.ParseString(element["Path"]);
        txtPath.Text = GetUploadKey();
        IsStatsUploadingEnabled = element["IsStatsUploadingEnabled"] == null || SettingsHelper.ParseBool(element["IsStatsUploadingEnabled"]);
        IsUploadOnResetEnabled = element["IsUploadOnResetEnabled"] == null || SettingsHelper.ParseBool(element["IsUploadOnResetEnabled"]);
        IsLiveTrackingEnabled = element["IsLiveTrackingEnabled"] == null || SettingsHelper.ParseBool(element["IsLiveTrackingEnabled"]);
        IsToastEnabled = element["IsToastEnabled"] == null || SettingsHelper.ParseBool(element["IsToastEnabled"]);
        IsLayoutPathUploadEnabled = element["IsLayoutPathUploadEnabled"] != null && SettingsHelper.ParseBool(element["IsLayoutPathUploadEnabled"]);

        if (IsHandleCreated)
        {
            _ = ValidateKeyAsync();
        }
        else
        {
            EventHandler handler = null;
            handler = (s, e) =>
            {
                HandleCreated -= handler;
                _ = ValidateKeyAsync();
            };
            HandleCreated += handler;
        }
    }

    private string GetUploadKey()
    {
        if (!string.IsNullOrEmpty(Path))
        {
            string key = Path;
            SaveUploadKey(key);
            return key;
        }

        if (!string.IsNullOrEmpty(txtPath.Text))
        {
            return txtPath.Text;
        }

        string filePath = System.IO.Path.Combine(UploadKeyFolder, UploadKeyFile);
        if (!File.Exists(filePath))
        {
            return "";
        }

        return File.ReadAllText(filePath).Trim();
    }

    private void SaveUploadKey(string key)
    {
        string filePath = System.IO.Path.Combine(UploadKeyFolder, UploadKeyFile);
        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, key);
        Path = "";
    }

    public XmlNode GetSettings(XmlDocument document)
    {
        XmlElement parent = document.CreateElement("Settings");
        CreateSettingsNode(document, parent);
        return parent;
    }

    public int GetSettingsHashCode()
    {
        return CreateSettingsNode(null, null);
    }

    private int CreateSettingsNode(XmlDocument document, XmlElement parent)
    {
        return SettingsHelper.CreateSetting(document, parent, "Version", "1.0.0") ^
            SettingsHelper.CreateSetting(document, parent,
                "IsStatsUploadingEnabled", IsStatsUploadingEnabled) ^
            SettingsHelper.CreateSetting(document, parent,
                "IsUploadOnResetEnabled", IsUploadOnResetEnabled) ^
            SettingsHelper.CreateSetting(document, parent,
                "IsLiveTrackingEnabled", IsLiveTrackingEnabled) ^
            SettingsHelper.CreateSetting(document, parent,
                "IsToastEnabled", IsToastEnabled) ^
            SettingsHelper.CreateSetting(document, parent,
                "IsLayoutPathUploadEnabled", IsLayoutPathUploadEnabled);
    }

    private void txtPath_Leave(object sender, EventArgs e)
    {
        SaveUploadKey(txtPath.Text);
    }

    private async void btnTest_Click(object sender, EventArgs e)
    {
        await ValidateKeyAsync();
    }

    private async Task ValidateKeyAsync()
    {
        string key = GetUploadKey();
        if (string.IsNullOrWhiteSpace(key))
        {
            SetStatus(ConnectionStatus.None);
            ShowUserInfo(null, null);
            return;
        }

        btnTest.Enabled = false;
        btnTest.Text = "...";
        SetStatus(ConnectionStatus.Validating);
        ShowUserInfo(null, null);

        try
        {
            string url = "https://api.therun.gg/users/uploadKey/validate/" + key;

            using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string body = await result.Content.ReadAsStringAsync();

            if (IsDisposed || !IsHandleCreated) return;

            if (result.IsSuccessStatusCode)
            {
                var serializer = new JavaScriptSerializer();
                var json = serializer.Deserialize<Dictionary<string, object>>(body);
                var data = (Dictionary<string, object>)((Dictionary<string, object>)json["result"])["data"];
                string username = (string)data["username"];
                string picture = data.ContainsKey("picture") ? (string)data["picture"] : null;

                ValidatedUsername = username;
                SetStatus(ConnectionStatus.Connected);
                ShowUserInfo(username, picture);
            }
            else
            {
                ValidatedUsername = null;
                SetStatus(ConnectionStatus.Error);
                lnkUsername.Text = "Invalid upload key.";
                lnkUsername.Visible = true;
            }
        }
        catch
        {
            if (IsDisposed || !IsHandleCreated) return;

            ValidatedUsername = null;
            SetStatus(ConnectionStatus.Error);
            lnkUsername.Text = "Could not reach therun.gg.";
            lnkUsername.Visible = true;
        }
        finally
        {
            if (!IsDisposed && IsHandleCreated)
            {
                btnTest.Enabled = true;
            }
        }
    }

    private void SetStatus(ConnectionStatus status)
    {
        Status = status;

        switch (status)
        {
            case ConnectionStatus.Connected:
                btnTest.ForeColor = System.Drawing.Color.FromArgb(0, 150, 0);
                btnTest.Text = "Test";
                break;
            case ConnectionStatus.Error:
                btnTest.ForeColor = System.Drawing.Color.FromArgb(200, 0, 0);
                btnTest.Text = "Test";
                break;
            case ConnectionStatus.Validating:
                btnTest.ForeColor = System.Drawing.SystemColors.ControlText;
                btnTest.Text = "...";
                break;
            default:
                btnTest.ForeColor = System.Drawing.SystemColors.ControlText;
                btnTest.Text = "Test";
                break;
        }

        lnkLive.Visible = status == ConnectionStatus.Connected && ValidatedUsername != null;
    }

    private void ShowUserInfo(string username, string pictureUrl)
    {
        if (username == null)
        {
            picUser.Visible = false;
            picUser.Image = null;
            lnkUsername.Visible = false;
            lnkUsername.Text = "";
            lnkUsername.Tag = null;
            lnkLive.Visible = false;
            return;
        }

        lnkUsername.Text = "therun.gg/" + username;
        lnkUsername.Tag = "https://therun.gg/" + username;
        lnkUsername.Visible = true;

        lnkLive.Tag = "https://therun.gg/live/" + username;
        lnkLive.Visible = true;

        if (!string.IsNullOrEmpty(pictureUrl))
        {
            try
            {
                picUser.LoadAsync(pictureUrl);
                picUser.Visible = true;
            }
            catch
            {
                picUser.Visible = false;
            }
        }
        else
        {
            picUser.Visible = false;
        }
    }

    private void lnkUploadKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://therun.gg/livesplit") { UseShellExecute = true });
    }

    private void lnkUsername_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (lnkUsername.Tag is string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }

    private void lnkLive_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (lnkLive.Tag is string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
