using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class App : Form {
        public const string Version = "1.1";
        public const string AppName = "FFXI Gamma";
        private const string AppFolderName = "ffxigamma";
        private const string ConfigFileName = "config.xml";

        private Config config;
        private Screen prevScreen;
        private Settings editSettings;
        private GlobalKeyReader globalKeyReader;

        public App() {
            InitializeComponent();

            config = LoadConfig();

            editSettings = new Settings();

            globalKeyReader = new GlobalKeyReader();
            globalKeyReader.GlobalKeyDown += globalKeyReader_GlobalKeyDown;
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
            if (FFXI.IsFullScreenMode() && FFXI.IsRunning()) return null;

            var wnd = Window.GetForegroundWindow();
            if (wnd == null) return null;
            if (wnd.IsIconic()) return null;
            if (!IsTargetWindowName(wnd)) return null;

            return Screen.FromRectangle(wnd.GetWindowRect());
        }

        private bool IsTargetWindowName(Window wnd) {
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

        public static Config LoadConfig() {
            try {
                return Config.Load(GetConfigPath());
            }
            catch (IOException) {
                return Config.Default;
            }
            catch (InvalidOperationException) {
                return Config.Default;
            }
            catch (UnauthorizedAccessException) {
                return Config.Default;
            }
        }

        private void SaveConfig(Config config) {
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

        private static string GetConfigPath() {
            return GetAppFolder() + @"\" + ConfigFileName;
        }

        private static string GetAppFolder() {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\" + AppFolderName;
        }

        private static void InitAppFolder() {
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
                    SaveConfig(config);
                }
            }
        }

        private static void ShowCotextMenu(NotifyIcon notifyIcon) {
            var type = typeof(NotifyIcon);
            var mi = type.GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
                mi.Invoke(notifyIcon, null);
        }

        private void ShowError(string s) {
            MessageBox.Show(this, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private List<Window> GetCapturableWindows() {
            var result = new List<Window>();
            foreach (var p in Process.GetProcesses()) {
                var wnd = new Window(p.MainWindowHandle);
                if (IsTargetWindowName(wnd) && !wnd.IsIconic())
                    result.Add(wnd);
            }
            return result;
        }

        private static void SetCapturableItems(ToolStripMenuItem menuItem, List<Window> wnds) {
            menuItem.DropDownItems.Clear();

            if (wnds.Count == 0) {
                menuItem.Enabled = false;
                menuItem.Tag = null;
            } else if (wnds.Count == 1) {
                menuItem.Enabled = true;
                menuItem.Tag = wnds[0];
            } else {
                menuItem.Enabled = true;
                menuItem.Tag = null;
                foreach (var wnd in wnds) {
                    var item = new ToolStripMenuItem(wnd.GetWindowText());
                    item.Tag = wnd;
                    menuItem.DropDownItems.Add(item);
                }
            }
        }

        private void CaptureToClipboard(Window wnd) {
            var bmp = CaptureWithEffects(wnd);
            Clipboard.SetDataObject(bmp);
        }

        private void CaptureSaveAs(Window wnd) {
            if (uiSaveAs.Tag != null) return;

            uiSaveAs.Tag = "block ShowDialog()";
            try {
                using (var bmp = CaptureWithEffects(wnd)) {
                    if (uiSaveAs.ShowDialog(this) == DialogResult.OK)
                        SaveImage(bmp, uiSaveAs.FileName);
                }
            }
            finally {
                uiSaveAs.Tag = null;
            }
        }

        private void CaptureSaveFolder() {
            var wnd = Window.GetForegroundWindow();
            if (wnd == null) return;
            if (wnd.IsIconic()) return;
            if (!IsTargetWindowName(wnd)) return;

            CaptureSaveFolder(wnd);
        }

        private void CaptureSaveFolder(Window wnd) {
            var path = CaptureFileName();
            using (var bmp = CaptureWithEffects(wnd)) {
                SaveImage(bmp, path);
            }
            ShowNotifySaved(path);
        }

        private string CaptureFileName() {
            var folder = config.ImageFolder;
            var name = DateTime.Now.ToString("yyyyMMdd-HHmmss.ff");
            var suffix = config.ImageFormatName;

            return folder + @"\" + name + "." + suffix;
        }

        private void ShowNotifySaved(string path) {
            uiNotifyIcon.ShowBalloonTip(1000, Text, path, ToolTipIcon.None);
        }

        private Bitmap CaptureWithEffects(Window wnd) {
            var bmp = wnd.Capture();

            if (config.EnableImageGamma) {
                var gamma = new Gamma(config.AppGamma);
                gamma.ApplyTo(bmp);
            }

            if (config.EnableText) {
                foreach (var imageText in config.ImageTextList) {
                    var writer = new ImageTextWriter(imageText);
                    writer.WriteTo(bmp);
                }
            }

            return bmp;
        }

        private void SaveImage(Image image, string path) {
            try {
                using (var fs = File.OpenWrite(path)) {
                    fs.SetLength(0);
                    image.Save(fs, GetImageFormat(path));
                }
            }
            catch (IOException e) {
                ShowError(e.Message);
            }
            catch (UnauthorizedAccessException ex) {
                ShowError(ex.Message);
            }
        }

        private static ImageFormat GetImageFormat(string path){
            path = path.ToLower();
            if (path.EndsWith(".png"))
                return ImageFormat.Png;
            else if (path.EndsWith(".jpg"))
                return ImageFormat.Jpeg;
            else
                return ImageFormat.Png;
        }

        private void StartFFXI() {
            if (FFXI.IsRunning()) return;

            if (!Program.IsAdminMode()) {
                if (Program.RestartAdminMode("/ffxi"))
                    Close();
            } else {
                FFXI.Start();
            }
        }

        private bool IsHotKeyDown(GlobalKeyEventArgs e) {
            if (!config.EnableHotkey) return false;
            if (!e.Trigger.Contains((Keys)config.HotKey)) return false;
            if (e.State.Contains(Keys.ControlKey) != config.HotKeyControl) return false;
            if (e.State.Contains(Keys.ShiftKey) != config.HotKeyShift) return false;
            if (e.State.Contains(Keys.Menu) != config.HotKeyAlt) return false;

            return true;
        }

        private void App_Load(object sender, EventArgs e) {
            Visible = false;
            ResetScreenGamma();
            uiTimer.Start();
            globalKeyReader.Start();
        }

        private void uiTimer_Tick(object sender, EventArgs e) {
            UpdateScreenGamma();
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e) {
            ResetScreenGamma();
            SaveConfig(config);
        }

        private void uiExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void uiSettings_Click(object sender, EventArgs e) {
            EditSettings();
        }

        private void uiNotifyIcon_Click(object sender, EventArgs e) {
            ShowCotextMenu(uiNotifyIcon);
        }

        private void uiContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            var wnds = GetCapturableWindows();
            SetCapturableItems(uiContextCaptureCopy, wnds);
            SetCapturableItems(uiContextCaptureSaveAs, wnds);
            SetCapturableItems(uiContextCaptureSaveFolder, wnds);

            uiContextStartFFXI.Enabled = !FFXI.IsRunning();
        }

        private void uiContextCaptureCopy_Click(object sender, EventArgs e) {
            if (uiContextCaptureCopy.Tag != null)
                CaptureToClipboard((Window)uiContextCaptureCopy.Tag);
        }

        private void uiContextCaptureCopy_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            CaptureToClipboard((Window)e.ClickedItem.Tag);
        }

        private void uiContextCaptureSaveAs_Click(object sender, EventArgs e) {
            if (uiContextCaptureSaveAs.Tag != null)
                CaptureSaveAs((Window)uiContextCaptureCopy.Tag);
        }

        private void uiContextCaptureSaveAs_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            CaptureSaveAs((Window)e.ClickedItem.Tag);
        }

        private void uiContextCaptureSaveFolder_Click(object sender, EventArgs e) {
            if (uiContextCaptureSaveFolder.Tag != null)
                CaptureSaveFolder((Window)uiContextCaptureSaveFolder.Tag);
        }

        private void uiContextCaptureSaveFolder_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            CaptureSaveFolder((Window)e.ClickedItem.Tag);
        }

        private void uiContextStartFFXI_Click(object sender, EventArgs e) {
            StartFFXI();
        }

        private void globalKeyReader_GlobalKeyDown(object sender, GlobalKeyEventArgs e) {
            if(IsHotKeyDown(e))
                CaptureSaveFolder();
        }
    }
}
