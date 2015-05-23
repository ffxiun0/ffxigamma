using System;
using System.IO;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class App : Form {
        public const string Version = "1.0.1";
        private const string AppName = "FFXI Gamma";
        private const string AppFolderName = "ffxigamma";
        private const string ConfigFileName = "config.xml";

        private Config config;
        private Screen prevScreen;
        private Settings editSettings;

        public App() {
            InitializeComponent();
            editSettings = new Settings();
            editSettings.Text = AppName + " " + Version;
        }

        private void UpdateScreenGamma() {
            var activeScreen = GetActiveScreen();

            if (ScreenHasChanged(activeScreen, prevScreen)) {
                foreach (var screen in Screen.AllScreens) {
                    double gamma = screen.Equals(activeScreen)
                        ? config.AppGamma : config.SystemGamma;
                    WinAPI.SetDeviceGammaRamp(screen, gamma);
                }
            }

            prevScreen = activeScreen;
        }

        private void ResetScreenGamma() {
            foreach (var screen in Screen.AllScreens)
                WinAPI.SetDeviceGammaRamp(screen, config.SystemGamma);
        }

        private Screen GetActiveScreen() {
            if (!FFXI.IsWindowMode()) return null;

            var wnd = Window.GetForegroundWindow();
            if (wnd == null) return null;

            if (!IsTargetWindow(wnd)) return null;

            return Screen.FromRectangle(wnd.GetWindowRect());
        }

        private bool IsTargetWindow(Window wnd) {
            var name = wnd.GetWindowText();
            foreach (var s in config.NameList) {
                if (s == name)
                    return true;
            }
            return false;
        }

        static bool ScreenHasChanged(Screen a, Screen b) {
            if (a == null && b == null) return false;
            if (a == null || b == null) return true;
            return !a.Equals(b);
        }

        private void LoadConfig() {
            try {
                config = Config.Load(GetConfigPath());
            }
            catch (IOException) {
                config = Config.Default;
            }
            catch (InvalidOperationException) {
                config = Config.Default;
            }
            catch (UnauthorizedAccessException) {
                config = Config.Default;
            }
        }

        private void SaveConfig() {
            InitAppFolder();
            try {
                config.Version = Version;
                config.Save(GetConfigPath());
            }
            catch (IOException ex) {
                ShowError(ex.Message);
            }
            catch (InvalidOperationException ex) {
                ShowError(ex.Message);
            }
            catch (UnauthorizedAccessException ex) {
                ShowError(ex.Message);
            }
        }

        private string GetConfigPath() {
            return GetAppFolder() + @"\" + ConfigFileName;
        }

        private string GetAppFolder() {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\" + AppFolderName;
        }

        private void InitAppFolder() {
            var path = GetAppFolder();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void EditSettings() {
            if (editSettings.Visible) {
                editSettings.Activate();
            } else {
                editSettings.Config = config;
                if (editSettings.ShowDialog(this) == DialogResult.OK) {
                    config = editSettings.Config;
                    ResetScreenGamma();
                }
            }
        }

        private void ShowError(string s) {
            MessageBox.Show(this, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void App_Load(object sender, EventArgs e) {
            Visible = false;
            LoadConfig();
            ResetScreenGamma();
            uiTimer.Start();
        }

        private void uiTimer_Tick(object sender, EventArgs e) {
            UpdateScreenGamma();
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e) {
            ResetScreenGamma();
            SaveConfig();
        }

        private void uiExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void uiSettings_Click(object sender, EventArgs e) {
            EditSettings();
        }

        private void uiNotifyIcon_DoubleClick(object sender, EventArgs e) {
            EditSettings();
        }
    }
}
