using System;
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
        }

        private WindowSettings GetFromUI() {
            var ws = new WindowSettings();
            ws.Name = uiName.Text;
            ws.X = (int)uiPositionX.Value;
            ws.Y = (int)uiPositionY.Value;
            return ws;
        }

        private void uiOk_Click(object sender, EventArgs e) {
            if (uiName.Text.Length <= 0) return;

            windowSettings = GetFromUI();
            DialogResult = DialogResult.OK;
        }
    }
}
