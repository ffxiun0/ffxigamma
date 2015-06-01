using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class Settings : Form {
        private InputHotKey inputHotKey;
        private ImageTextEditor imageTextEditor;
        private Config config;

        public Settings() {
            InitializeComponent();
            inputHotKey = new InputHotKey();
            imageTextEditor = new ImageTextEditor();
            Config = Config.Default;
        }

        public Config Config {
            set {
                config = value;
                SetConfigToUI(config);
            }
            get {
                return config;
            }
        }

        private void SetConfigToUI(Config config) {
            uiAppGamma.Text = config.AppGamma.ToString();
            uiSystemGamma.Text = config.SystemGamma.ToString();
            uiAdminMode.Checked = config.AdminMode;
            uiStartUpFFXI.Checked = config.StartFFXI;

            uiName.Text = "";
            uiNameList.Items.Clear();
            foreach (var s in config.NameList)
                uiNameList.Items.Add(s);

            uiEnableImageGamma.Checked = config.EnableImageGamma;
            uiImageFolder.Text = config.ImageFolder;
            uiImageFormatName.Text = config.ImageFormatName;

            uiEnableHotKey.Checked = config.EnableHotkey;
            inputHotKey.HotKey.Key = (Keys)config.HotKey;
            inputHotKey.HotKey.Control = config.HotKeyControl;
            inputHotKey.HotKey.Shift = config.HotKeyShift;
            inputHotKey.HotKey.Alt = config.HotKeyAlt;
            uiHotKey.Text = inputHotKey.HotKey.ToString();

            uiEnableText.Checked = config.EnableText;
            uiTextList.Items.Clear();
            foreach (var text in config.ImageTextList)
                uiTextList.Items.Add(text);
        }

        private Config GetConfigFromUI() {
            var config = new Config();

            config.AppGamma = double.Parse(uiAppGamma.Text);
            config.SystemGamma = double.Parse(uiSystemGamma.Text);
            config.AdminMode = uiAdminMode.Checked;
            config.StartFFXI = uiStartUpFFXI.Checked;

            var nlist = new List<string>();
            foreach (string name in uiNameList.Items)
                nlist.Add(name);
            config.NameList = nlist.ToArray();

            config.EnableImageGamma = uiEnableImageGamma.Checked;
            config.ImageFolder = uiImageFolder.Text;
            config.ImageFormatName = uiImageFormatName.Text;

            config.EnableHotkey = uiEnableHotKey.Checked;
            config.HotKey = (int)inputHotKey.HotKey.Key;
            config.HotKeyControl = inputHotKey.HotKey.Control;
            config.HotKeyShift = inputHotKey.HotKey.Shift;
            config.HotKeyAlt = inputHotKey.HotKey.Alt;

            config.EnableText = uiEnableText.Checked;
            var itlist = new List<ImageText>();
            foreach (ImageText imageText in uiTextList.Items)
                itlist.Add(imageText);
            config.ImageTextList = itlist.ToArray();

            return config;
        }

        private void ShowWarning(string s) {
            MessageBox.Show(this, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Settings_Load(object sender, EventArgs e) {
            Text = App.AppName + " " + App.Version;
            if (Program.IsAdminMode())
                Text += " (" + Properties.Resources.AdminMode + ")";
        }

        private void uiNameAdd_Click(object sender, EventArgs e) {
            if (uiName.Text.Length > 0)
                uiNameList.Items.Add(uiName.Text);
            uiName.Text = "";
        }

        private void uiNameDelete_Click(object sender, EventArgs e) {
            while (uiNameList.SelectedItems.Count > 0)
                uiNameList.Items.Remove(uiNameList.SelectedItems[0]);
        }

        private void uiReset_Click(object sender, EventArgs e) {
            Config = Config.Default;
        }

        private void uiOk_Click(object sender, EventArgs e) {
            try {
                config = GetConfigFromUI();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (FormatException ex) {
                ShowWarning(ex.Message);
            }
            catch (OverflowException ex) {
                ShowWarning(ex.Message);
            }
        }

        private void uiEditImageFolder_Click(object sender, EventArgs e) {
            uiFolderDialog.SelectedPath = uiImageFolder.Text;
            if (uiFolderDialog.ShowDialog(this) == DialogResult.OK)
                uiImageFolder.Text = uiFolderDialog.SelectedPath;
        }

        private void uiEditHotKey_Click(object sender, EventArgs e) {
            if (inputHotKey.ShowDialog(this) == DialogResult.OK)
                uiHotKey.Text = inputHotKey.HotKey.ToString();
        }

        private void uiEnableHotKey_Click(object sender, EventArgs e) {
            if (uiEnableHotKey.Checked && !Program.IsAdminMode())
                ShowWarning(Properties.Resources.HotKeyWarning);
        }

        private void uiTextAdd_Click(object sender, EventArgs e) {
            imageTextEditor.Config = new ImageText();
            if (imageTextEditor.ShowDialog(this) == DialogResult.OK)
                uiTextList.Items.Add(imageTextEditor.Config);
        }

        private void uiTextEdit_Click(object sender, EventArgs e) {
            if (uiTextList.SelectedIndices.Count != 1) return;
            var index = uiTextList.SelectedIndex;

            imageTextEditor.Config = (ImageText)uiTextList.Items[index];
            if (imageTextEditor.ShowDialog(this) == DialogResult.OK)
                uiTextList.Items[index] = imageTextEditor.Config;
        }

        private void uiTextDelete_Click(object sender, EventArgs e) {
            while (uiTextList.SelectedItems.Count > 0)
                uiTextList.Items.Remove(uiTextList.SelectedItems[0]);
        }
    }
}
