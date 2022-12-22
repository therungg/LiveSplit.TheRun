using System;
using System.Xml;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public partial class CollectorSettings : UserControl
    {

        public LayoutMode Mode { get; set; }

        public string Path { get; set; }
        public bool IsStatsUploadingEnabled { get; set; }
        public bool IsLiveTrackingEnabled { get; set;  }

        public CollectorSettings()
        {
            InitializeComponent();

            txtPath.DataBindings.Add("Text", this, "Path", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStatsUploadEnabled.DataBindings.Add("Checked", this, "IsStatsUploadingEnabled",
                false, DataSourceUpdateMode.OnPropertyChanged);
            chkLiveTrackingEnabled.DataBindings.Add("Checked", this, "IsLiveTrackingEnabled",
                false, DataSourceUpdateMode.OnPropertyChanged);

            Path = "";
            IsStatsUploadingEnabled = true;
            IsLiveTrackingEnabled = true;
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            Version version = SettingsHelper.ParseVersion(element["Version"]);
            Path = SettingsHelper.ParseString(element["Path"]);
            IsStatsUploadingEnabled = element["IsStatsUploadingEnabled"] == null ? true : SettingsHelper.ParseBool(element["IsStatsUploadingEnabled"]);
            IsLiveTrackingEnabled = element["IsLiveTrackingEnabled"] == null ? true : SettingsHelper.ParseBool(element["IsLiveTrackingEnabled"]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
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
                SettingsHelper.CreateSetting(document, parent, "Path", Path) ^
                SettingsHelper.CreateSetting(document, parent,
                    "IsStatsUploadingEnabled", IsStatsUploadingEnabled) ^
                SettingsHelper.CreateSetting(document, parent,
                    "IsLiveTrackingEnabled", IsLiveTrackingEnabled);
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
