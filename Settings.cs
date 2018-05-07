﻿/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using CLParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class Settings : Form {
        private InputHotKey inputHotKeyCapture;
        private InputHotKey inputHotKeyMute;
        private InputHotKey inputHotKeyVolumeUp;
        private InputHotKey inputHotKeyVolumeDown;
        private ImageTextEditor imageTextEditor;
        private WindowSettingsEditor windowSettingsEditor;
        private Config config;

        public Settings() {
            InitializeComponent();

            Icon = Properties.Resources.Icon;

            inputHotKeyCapture = new InputHotKey();
            inputHotKeyMute = new InputHotKey();
            inputHotKeyVolumeUp = new InputHotKey();
            inputHotKeyVolumeDown = new InputHotKey();
            imageTextEditor = new ImageTextEditor();
            windowSettingsEditor = new WindowSettingsEditor();

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
            uiAutoStartProgram.Checked = config.StartFFXI;
            StartProgramType = config.StartProgramType;
            uiStartProgramCommandLine.Text = config.StartProgramCommandLine;

            uiSaveWindowPosition.Checked = config.EnableSaveWindowPosition;
            uiWindowSettingsList.Items.Clear();
            foreach (var s in config.WindowSettingsList)
                uiWindowSettingsList.Items.Add(s);

            uiEnableImageGamma.Checked = config.EnableImageGamma;
            uiImageFolder.Text = config.ImageFolder;
            uiImageFormatName.Text = config.ImageFormatName;

            uiEnableHotKeyCapture.Checked = config.EnableHotKeyCapture;
            inputHotKeyCapture.HotKey = config.GetHotKey("Capture");
            uiHotKeyCapture.Text = inputHotKeyCapture.HotKey.ToString();

            uiEnableHotKeyVolumeControl.Checked = config.EnableHotKeyVolumeControl;
            inputHotKeyMute.HotKey = config.GetHotKey("Mute");
            uiHotKeyMute.Text = inputHotKeyMute.HotKey.ToString();
            inputHotKeyVolumeUp.HotKey = config.GetHotKey("VolumeUp");
            uiHotKeyVolumeUp.Text = inputHotKeyVolumeUp.HotKey.ToString();
            inputHotKeyVolumeDown.HotKey = config.GetHotKey("VolumeDown");
            uiHotKeyVolumeDown.Text = inputHotKeyVolumeDown.HotKey.ToString();

            uiEnableImageText.Checked = config.EnableImageText;
            uiImageTextList.Items.Clear();
            foreach (var text in config.ImageTextList)
                uiImageTextList.Items.Add(text);

            uiEnableNotify.Checked = config.EnableNotify;
        }

        private Config GetConfigFromUI() {
            var config = new Config();

            config.AppGamma = double.Parse(uiAppGamma.Text);
            config.SystemGamma = double.Parse(uiSystemGamma.Text);
            config.AdminMode = uiAdminMode.Checked;
            config.StartFFXI = uiAutoStartProgram.Checked;
            config.StartProgramType = StartProgramType;
            config.StartProgramCommandLine = uiStartProgramCommandLine.Text;

            config.EnableSaveWindowPosition = uiSaveWindowPosition.Checked;
            var wslist = new List<WindowSettings>();
            foreach (WindowSettings ws in uiWindowSettingsList.Items)
                wslist.Add(ws);
            config.WindowSettingsList = wslist.ToArray();

            config.EnableImageGamma = uiEnableImageGamma.Checked;
            config.ImageFolder = uiImageFolder.Text;
            config.ImageFormatName = uiImageFormatName.Text;

            config.EnableHotKeyCapture = uiEnableHotKeyCapture.Checked;
            config.HotKeySettingsList = new HotKeySettings[0];
            config.SetHotKey("Capture", inputHotKeyCapture.HotKey);

            config.EnableHotKeyVolumeControl = uiEnableHotKeyVolumeControl.Checked;
            config.SetHotKey("Mute", inputHotKeyMute.HotKey);
            config.SetHotKey("VolumeUp", inputHotKeyVolumeUp.HotKey);
            config.SetHotKey("VolumeDown", inputHotKeyVolumeDown.HotKey);

            config.EnableImageText = uiEnableImageText.Checked;
            var itlist = new List<ImageText>();
            foreach (ImageText imageText in uiImageTextList.Items)
                itlist.Add(imageText);
            config.ImageTextList = itlist.ToArray();

            config.EnableNotify = uiEnableNotify.Checked;

            return config;
        }

        private bool CheckConfig() {
            if (CommandLine.Parse(uiStartProgramCommandLine.Text) == null) {
                ShowError(Properties.Resources.CommandLineFormattError);
                return false;
            }

            return true;
        }

        private Dictionary<string, int> programTypeMap = new Dictionary<string, int>() {
            {"ffxi", 0 },
            {"program", 1 },
        };

        private string StartProgramType {
            get {
                foreach (var pair in programTypeMap) {
                    if (pair.Value == uiStartProgramType.SelectedIndex)
                        return pair.Key;
                }
                return "ffxi";
            }
            set {
                if (programTypeMap.ContainsKey(value))
                    uiStartProgramType.SelectedIndex = programTypeMap[value];
                else
                    uiStartProgramType.SelectedIndex = 0;
            }
        }

        private void ShowWarning(string s) {
            MessageBox.Show(this, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowError(string s) {
            MessageBox.Show(this, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Settings_Load(object sender, EventArgs e) {
            Text = App.AppName + " " + App.Version;
            if (Program.IsAdminMode())
                Text += " (" + Properties.Resources.AdminMode + ")";
        }

        private void uiSaveWindowPosition_Click(object sender, EventArgs e) {
            if (uiSaveWindowPosition.Checked)
                ShowWarning(Properties.Resources.RequireAdminWarning);
        }

        private void uiWindowSettingsAdd_Click(object sender, EventArgs e) {
            windowSettingsEditor.WindowSettings = new WindowSettings();
            if (windowSettingsEditor.ShowDialog(this) == DialogResult.OK) {
                var ws = windowSettingsEditor.WindowSettings;
                uiWindowSettingsList.Items.Add(ws);
            }
        }

        private void uiWindowSettingsEdit_Click(object sender, EventArgs e) {
            if (uiWindowSettingsList.SelectedIndices.Count != 1) return;
            var index = uiWindowSettingsList.SelectedIndex;

            windowSettingsEditor.WindowSettings = (WindowSettings)uiWindowSettingsList.Items[index];
            if (windowSettingsEditor.ShowDialog(this) == DialogResult.OK)
                uiWindowSettingsList.Items[index] = windowSettingsEditor.WindowSettings;
        }

        private void uiWindowSettingsDelete_Click(object sender, EventArgs e) {
            while (uiWindowSettingsList.SelectedItems.Count > 0)
                uiWindowSettingsList.Items.Remove(uiWindowSettingsList.SelectedItems[0]);
        }

        private void uiReset_Click(object sender, EventArgs e) {
            Config = Config.Default;
        }

        private void uiOk_Click(object sender, EventArgs e) {
            if (!CheckConfig()) return;

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
            if (inputHotKeyCapture.ShowDialog(this) == DialogResult.OK)
                uiHotKeyCapture.Text = inputHotKeyCapture.HotKey.ToString();
        }

        private void uiEnableHotKey_Click(object sender, EventArgs e) {
            if (uiEnableHotKeyCapture.Checked)
                ShowWarning(Properties.Resources.RequireAdminWarning);
        }

        private void uiImageTextAdd_Click(object sender, EventArgs e) {
            imageTextEditor.Config = new ImageText();
            if (imageTextEditor.ShowDialog(this) == DialogResult.OK)
                uiImageTextList.Items.Add(imageTextEditor.Config);
        }

        private void uiImageTextEdit_Click(object sender, EventArgs e) {
            if (uiImageTextList.SelectedIndices.Count != 1) return;
            var index = uiImageTextList.SelectedIndex;

            imageTextEditor.Config = (ImageText)uiImageTextList.Items[index];
            if (imageTextEditor.ShowDialog(this) == DialogResult.OK)
                uiImageTextList.Items[index] = imageTextEditor.Config;
        }

        private void uiImageTextDelete_Click(object sender, EventArgs e) {
            while (uiImageTextList.SelectedItems.Count > 0)
                uiImageTextList.Items.Remove(uiImageTextList.SelectedItems[0]);
        }

        private void uiEnableVolumeControl_Click(object sender, EventArgs e) {
            if (uiEnableHotKeyVolumeControl.Checked)
                ShowWarning(Properties.Resources.RequireAdminWarning);
        }

        private void uiEditHotKeyMute_Click(object sender, EventArgs e) {
            if (inputHotKeyMute.ShowDialog(this) == DialogResult.OK)
                uiHotKeyMute.Text = inputHotKeyMute.HotKey.ToString();
        }

        private void uiEditHotKeyVolumeUp_Click(object sender, EventArgs e) {
            if (inputHotKeyVolumeUp.ShowDialog(this) == DialogResult.OK)
                uiHotKeyVolumeUp.Text = inputHotKeyVolumeUp.HotKey.ToString();
        }

        private void uiEditHotKeyVolumeDown_Click(object sender, EventArgs e) {
            if (inputHotKeyVolumeDown.ShowDialog(this) == DialogResult.OK)
                uiHotKeyVolumeDown.Text = inputHotKeyVolumeDown.HotKey.ToString();
        }

        private void uiStartProgramType_SelectedIndexChanged(object sender, EventArgs e) {
            var enabled = StartProgramType == "program";
            uiStartProgramCommandLine.Enabled = enabled;
            uiEditProgramCommandLine.Enabled = enabled;
        }

        private void uiEditProgramCommandLine_Click(object sender, EventArgs e) {
            if (uiProgramDialog.ShowDialog(this) == DialogResult.OK) {
                var path = uiProgramDialog.FileName;

                var exe = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
                if (path.Contains(exe))
                    uiStartProgramCommandLine.Text = "";
                else
                    uiStartProgramCommandLine.Text = CommandLine.ToString(path);
            }
        }
    }
}
