﻿using System;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class WindowSettingsEditor : Form {
        private WindowSettings windowSettings;

        public WindowSettingsEditor() {
            InitializeComponent();
        }

        public WindowSettings WindowSettings {
            get {
                return windowSettings;
            }
            set {
                windowSettings = value;
                SetToUI(windowSettings);
            }
        }

        private void SetToUI(WindowSettings ws) {
            uiName.Text = ws.Name;
            uiPositionX.Value = ws.X;
            uiPositionY.Value = ws.Y;
            uiWidth.Value = ws.Width;
            uiHeight.Value = ws.Height;
        }

        private WindowSettings GetFromUI() {
            var ws = new WindowSettings();
            ws.Name = uiName.Text;
            ws.X = (int)uiPositionX.Value;
            ws.Y = (int)uiPositionY.Value;
            ws.Width = (int)uiWidth.Value;
            ws.Height = (int)uiHeight.Value;
            return ws;
        }

        private void uiOk_Click(object sender, EventArgs e) {
            if (uiName.Text.Length <= 0) return;

            windowSettings = GetFromUI();
            DialogResult = DialogResult.OK;
        }

        private void WindowSettingsEditor_Load(object sender, EventArgs e) {
            uiDisplay.Items.Clear();
            foreach (var screen in Screen.AllScreens)
                uiDisplay.Items.Add(new DisplayItem(screen));
        }

        private void uiDisplay_SelectionChangeCommitted(object sender, EventArgs e) {
            if (uiDisplay.SelectedIndex < 0) return;

            var item = (DisplayItem)uiDisplay.SelectedItem;
            uiPositionX.Value = item.Screen.Bounds.X;
            uiPositionY.Value = item.Screen.Bounds.Y;
            uiWidth.Value = item.Screen.Bounds.Width;
            uiHeight.Value = item.Screen.Bounds.Height;
        }
    }

    class DisplayItem {
        public Screen Screen { get; set; }

        public DisplayItem(Screen screen) {
            Screen = screen;
        }

        public override string ToString() {
            return Screen.DeviceName;
        }
    }
}
