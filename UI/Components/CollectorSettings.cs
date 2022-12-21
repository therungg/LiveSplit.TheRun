using System;
using System.Xml;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public partial class CollectorSettings : UserControl
    {

        public LayoutMode Mode { get; set; }

        public string Path { get; set; }
        public bool IsEnabled { get; set; }

        public CollectorSettings()
        {
            InitializeComponent();

            txtPath.DataBindings.Add("Text", this, "Path", false, DataSourceUpdateMode.OnPropertyChanged);
            chkIsEnabled.DataBindings.Add("Checked", this, "IsEnabled", false, DataSourceUpdateMode.OnPropertyChanged);

            Path = "";
            IsEnabled = true;
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            Version version = SettingsHelper.ParseVersion(element["Version"]);
            Path = SettingsHelper.ParseString(element["Path"]);
            IsEnabled = SettingsHelper.ParseBool(element["IsEnabled"]);
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
                SettingsHelper.CreateSetting(document, parent, "IsEnabled", IsEnabled);
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
