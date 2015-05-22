using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class Settings : Form {
        private Config config;

        public Settings() {
            InitializeComponent();
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
            uiName.Text = "";
            uiNameList.Items.Clear();
            foreach (var s in config.NameList)
                uiNameList.Items.Add(s);
        }

        private Config GetConfigFromUI() {
            var config = new Config();

            config.AppGamma = double.Parse(uiAppGamma.Text);
            config.SystemGamma = double.Parse(uiSystemGamma.Text);
            config.NameList = new List<string>();
            foreach (string item in uiNameList.Items)
                config.NameList.Add(item);

            return config;
        }

        private void ShowWarning(string s) {
            MessageBox.Show(this, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void uiAdd_Click(object sender, EventArgs e) {
            if (uiName.Text.Length > 0)
                uiNameList.Items.Add(uiName.Text);
            uiName.Text = "";
        }

        private void uiDelete_Click(object sender, EventArgs e) {
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
    }
}
