﻿/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using CLParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class App : Form {
        public const string Version = "2.3.1";
        public const string AppName = "FFXI Gamma";
        private const string AppFolderName = "ffxigamma";
        private const string ConfigFileName = "config.xml";
        private const string SecureConfigFileName = "sconfig.xml";
        private const int SetWindowPositionTimeout = 5000;
        private const int SetWindowPositionDelay = 100;
        private const float VolumeStep = 0.02f;
        private const string pipeName = "ffxigamma-remote-control";

        private Config config;
        private SecureConfig secureConfig;
        private Screen[] prevScreens;
        private Settings editSettings;
        private VolumeIndicator volumeIndicator;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EnableAutoStartProgram { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool OpenSettings { get; set; } = false;

        public App() {
            InitializeComponent();

            Icon = Properties.Resources.Icon;

            config = LoadConfig(true);
            secureConfig = LoadSecureConfig(true);
            prevScreens = new Screen[0];
            editSettings = new Settings();
            volumeIndicator = new VolumeIndicator();

            SetShieldIcon(uiContextStartFFXI);
            SetShieldIcon(uiContextRestartAdminMode);

            uiNotifyIcon.Icon = new Icon(Properties.Resources.Icon, SystemInformation.SmallIconSize);
        }

        private void SetShieldIcon(ToolStripMenuItem menuItem) {
            menuItem.ImageScaling = ToolStripItemImageScaling.None;
            menuItem.Image = NativeMethods.GetShieldIcon();
        }

        private void UpdateScreenGamma(IEnumerable<WindowInfo> wndInfos) {
            var gammaWindows = ExtractGammaWindows(wndInfos);
            var usingScreens = FindUsingScreens(gammaWindows);

            if (ScreenHasChanged(prevScreens, usingScreens))
                SetScreenGamma(usingScreens);

            prevScreens = usingScreens;
        }

        private IEnumerable<WindowInfo> ExtractGammaWindows(IEnumerable<WindowInfo> wndInfos) {
            if (FFXI.IsFullScreenMode() && FFXI.IsRunning())
                return new WindowInfo[0];

            var result = from wndInfo in wndInfos
                         where IsGammaWindow(wndInfo.Window)
                         select wndInfo;
            return result.ToArray();
        }

        private bool IsGammaWindow(Window wnd) {
            if (!IsTargetWindow(wnd)) return false;
            if (wnd.IsIconic()) return false;

            var ws = config.GetWindowSettings(wnd.GetWindowText());
            if (ws == null) return false;

            return ws.AlwaysGamma || wnd.IsForeground();
        }

        private bool IsTargetWindow(Window wnd) {
            if (wnd == null) return false;
            if (!wnd.IsVisible()) return false;

            var ws = config.GetWindowSettings(wnd.GetWindowText());
            if (ws == null) return false;

            return !wnd.IsExplorer();
        }

        private static Screen[] FindUsingScreens(IEnumerable<WindowInfo> wndInfos) {
            var screens = from wi in wndInfos select Screen.FromRectangle(wi.Rect);
            screens = screens.Distinct();
            screens = screens.OrderBy(screen => screen.DeviceName);
            return screens.ToArray();
        }

        private static bool ScreenHasChanged(Screen[] a, Screen[] b) {
            if (a.Length != b.Length) return true;

            for (int i = 0; i < a.Length; i++) {
                if (!a[i].Equals(b[i]))
                    return true;
            }
            return false;
        }

        private void SetScreenGamma(IEnumerable<Screen> gammaScreens) {
            foreach (var screen in Screen.AllScreens) {
                double gamma = gammaScreens.Contains(screen)
                    ? config.AppGamma : config.SystemGamma;
                NativeMethods.SetDeviceGammaRamp(screen, gamma);
            }
        }

        private void ResetScreenGamma() {
            foreach (var screen in Screen.AllScreens)
                NativeMethods.SetDeviceGammaRamp(screen, config.SystemGamma);
            prevScreens = new Screen[0];
        }

        public static Config LoadConfig(bool showError) {
            try {
                return Config.Load(GetConfigPath());
            }
            catch (FileNotFoundException) { return Config.Default; }
            catch (DirectoryNotFoundException) { return Config.Default; }
            catch (InvalidOperationException) { return Config.Default; }
            catch (Exception ex) {
                if (showError)
                    Popup.Exception(ex, Properties.Resources.ConfigLoadError);
                return Config.Default;
            }
        }

        private void SaveConfig(Config config) {
            InitAppFolder();
            try {
                config.Version = Version;
                config.Save(GetConfigPath());
            }
            catch (Exception ex) {
                Popup.Exception(ex, Properties.Resources.ConfigSaveError);
            }
        }

        private static SecureConfig LoadSecureConfig(bool showError) {
            try {
                return SecureConfig.Load(GetSecureConfigPath());
            }
            catch (FileNotFoundException) { return SecureConfig.Default; }
            catch (DirectoryNotFoundException) { return SecureConfig.Default; }
            catch (InvalidOperationException) { return SecureConfig.Default; }
            catch (Exception ex) {
                if (showError)
                    Popup.Exception(ex, Properties.Resources.ConfigLoadError);
                return SecureConfig.Default;
            }
        }

        private void SaveSecureConfig(SecureConfig config) {
            InitAppFolder();
            try {
                config.Version = Version;
                config.Save(GetSecureConfigPath());
            }
            catch (Exception ex) {
                Popup.Exception(ex, Properties.Resources.ConfigSaveError);
            }
        }

        private void ApplyConfig(Config config) {
            globalKeyReader.Enabled =
                config.EnableHotKeyCapture || config.EnableHotKeyVolumeControl;

            windowMonitor.Filter = wnd => IsTargetWindow(wnd);
        }

        private static string GetConfigPath() {
            return GetAppFolder() + @"\" + ConfigFileName;
        }

        private static string GetSecureConfigPath() {
            return GetAppFolder() + @"\" + SecureConfigFileName;
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
                editSettings.SecureConfig = secureConfig;
                if (editSettings.ShowDialog(this) == DialogResult.OK)
                    ApplySettings(editSettings);
            }
        }

        private void ApplySettings(Settings settings) {
            config = settings.Config;
            SaveConfig(config);

            if (Program.IsAdminMode()) {
                secureConfig = settings.SecureConfig;
                SaveSecureConfig(secureConfig);
            }

            ApplyConfig(config);
            ResetScreenGamma();
            windowMonitor.Reset();

            if (settings.Action == SettingsAction.RestartAdmin)
                RestartAdminMode("/settings");
        }

        private static void ShowCotextMenu(NotifyIcon notifyIcon) {
            var type = typeof(NotifyIcon);
            var mi = type.GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
                mi.Invoke(notifyIcon, null);
        }

        private IEnumerable<Window> GetTargetProcessWindows() {
            var wnds = from p in Process.GetProcesses()
                       select new Window(p.MainWindowHandle);
            wnds = from w in wnds where IsTargetWindow(w) select w;
            return wnds.ToArray();
        }

        private List<Window> GetCapturableWindows(IEnumerable<Window> wnds) {
            var capWnds = from wnd in wnds
                          where IsCapturableWindow(wnd)
                          select wnd;
            return capWnds.ToList();
        }

        private bool IsCapturableWindow(Window wnd) {
            if (wnd == null) return false;

            return IsTargetWindow(wnd) && !wnd.IsIconic();
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
            if (!IsCapturableWindow(wnd)) return;

            var bmp = CaptureWithEffects(wnd);
            Clipboard.SetDataObject(bmp);
        }

        private void CaptureSaveAs(Window wnd) {
            if (uiSaveAs.Tag != null) return;
            if (!IsCapturableWindow(wnd)) return;

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

        private void CaptureSaveToFolder() {
            var wnd = Window.GetForegroundWindow();
            CaptureSaveToFolder(wnd);
        }

        private void CaptureSaveToFolder(Window wnd) {
            if (!IsCapturableWindow(wnd)) return;

            var time = DateTime.Now;
            var folder = GetCaptureSaveFolder(time);
            if (!CreateSubFolder(folder)) return;

            var path = GetCaptureFileName(folder, time);
            using (var bmp = CaptureWithEffects(wnd, time)) {
                SaveImage(bmp, path);
            }
            ShowNotifySaved(path);
        }

        private bool CreateSubFolder(string path) {
            if (!config.EnableImageSubFolder) return true;
            if (Directory.Exists(path)) return true;

            try {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (IOException ex) {
                Popup.Error(ex.Message);
            }
            catch (UnauthorizedAccessException ex) {
                Popup.Error(ex.Message);
            }

            return false;
        }

        private string GetCaptureSaveFolder(DateTime time) {
            if (config.EnableImageSubFolder)
                return config.ImageFolder + @"\" + time.ToString(GetSubFolderFormat());
            else
                return config.ImageFolder;
        }

        private string GetSubFolderFormat() {
            switch (config.ImageSubFolderFormat) {
                case "yyyy": return "yyyy";
                case "yyyy/mm": return @"yyyy\\MM";
                case "yyyy/mm/dd": return @"yyyy\\MM\\dd";
                default: return @"yyyy\\MM\\dd";
            }
        }

        private string GetCaptureFileName(string folder, DateTime time) {
            var name = time.ToString("yyyyMMdd-HHmmss.ff");
            var suffix = config.ImageFormatName;

            return folder + @"\" + name + "." + suffix;
        }

        private void ShowNotifySaved(string path) {
            if (!config.EnableNotify) return;

            var title = Properties.Resources.NotifySaved;
            var text = Path.GetDirectoryName(path) + "\n" + Path.GetFileName(path);
            uiNotifyIcon.ShowBalloonTip(1000, title, text, ToolTipIcon.Info);
            uiNotifyIcon.Tag = path;
        }

        private Bitmap CaptureWithEffects(Window wnd) {
            return CaptureWithEffects(wnd, DateTime.Now);
        }

        private Bitmap CaptureWithEffects(Window wnd, DateTime time) {
            var bmp = wnd.Capture();

            if (config.EnableImageGamma) {
                var gamma = new Gamma(config.AppGamma);
                gamma.ApplyTo(bmp);
            }

            if (config.EnableImageText) {
                var keywords = new KeywordExchanger(time);
                foreach (var imageText in config.ImageTextList) {
                    var writer = new ImageTextWriter(imageText);
                    writer.Text = keywords.Replace(writer.Text);
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
            catch (IOException ex) {
                Popup.Error(ex.Message);
            }
            catch (UnauthorizedAccessException ex) {
                Popup.Error(ex.Message);
            }
        }

        private static ImageFormat GetImageFormat(string path) {
            path = path.ToLower();
            if (path.EndsWith(".png"))
                return ImageFormat.Png;
            else if (path.EndsWith(".jpg"))
                return ImageFormat.Jpeg;
            else
                return ImageFormat.Png;
        }

        private void SetMuteItems(ToolStripMenuItem menuItem, IEnumerable<Window> wnds) {
            var winVols = from wnd in wnds
                          select new { Window = wnd, Volume = GetVolumeControl(wnd) };
            winVols = winVols.Where(w => w.Volume.Active);
            winVols = winVols.ToArray();

            menuItem.DropDownItems.Clear();

            if (winVols.Count() == 0) {
                menuItem.Enabled = false;
                menuItem.Checked = false;
                menuItem.Tag = null;
            } else if (winVols.Count() == 1) {
                var vols = winVols.Select(w => w.Volume);
                menuItem.Tag = new VolumeControlGroup(vols);
                UpdateMuteStates();
            } else {
                var vols = winVols.Select(w => w.Volume);
                menuItem.Tag = new VolumeControlGroup(vols);
                foreach (var wv in winVols) {
                    if (wv.Volume.Active) {
                        var item = new ToolStripMenuItem(wv.Window.GetWindowText());
                        item.Tag = wv.Volume;
                        item.Checked = wv.Volume.Mute;
                        menuItem.DropDownItems.Add(item);
                    }
                }
                UpdateMuteStates();
            }
        }

        private static VolumeControl GetVolumeControl(Window wnd) {
            if (wnd == null)
                return new NullVolumeControl();

            var vc = VolumeControl.FromProcessId(wnd.GetProcessId());
            if (vc == null)
                return new NullVolumeControl();

            return vc;
        }

        private void UpdateMuteStates() {
            UpdateMuteState(uiContextMute);
            foreach (ToolStripMenuItem item in uiContextMute.DropDownItems)
                UpdateMuteState(item);
        }

        private static void UpdateMuteState(ToolStripMenuItem menuItem) {
            if (menuItem == null) return;
            if (menuItem.Tag == null) return;

            var vc = (VolumeControl)menuItem.Tag;
            menuItem.Enabled = vc.Active;
            menuItem.Checked = vc.Mute;
        }

        private void StartProgram() {
            if (config.StartProgramType == "ffxi")
                StartProgramFFXI();
            else if (config.StartProgramType == "program")
                StartProgramCustom();
            else
                Popup.Error(Properties.Resources.ProgramStartFail);
        }

        private void StartProgramFFXI() {
            if (FFXI.IsRunning()) {
                Popup.Warning(Properties.Resources.FFXIAlreadyRunning);
                return;
            }

            if (FFXI.Start() == StartResult.Failure)
                Popup.Error(Properties.Resources.FFXIStartFail);
        }

        private void StartProgramCustom() {
            if (!Program.IsAdminMode() && FFXI.IsRunning()) {
                if (!Popup.YesNoWarning(Properties.Resources.AdminModeWarning))
                    return;
            }

            var cl = CommandLine.Parse(secureConfig.StartProgramCommandLine);
            if (cl == null) {
                Popup.Error(Properties.Resources.CommandLineFormattError);
                return;
            }

            if (!cl.IsEmpty) {
                if (ProcessEx.Start(cl.Exe, cl.Args) == StartResult.Failure)
                    Popup.Error(Properties.Resources.ProgramStartFail);
            }
        }

        public void StartProgramAsAdmin() {
            if (Program.IsAdminMode())
                StartProgram();
            else
                RestartAdminMode("/ffxi");
        }

        public void AutoStartProgram() {
            if (config.AdminMode)
                StartProgramAsAdmin();
            else
                StartProgram();
        }

        private void RestartAdminMode(params string[] args) {
            if (Program.IsAdminMode()) return;

            if (FFXI.IsRunning()) {
                if (!Popup.YesNoWarning(Properties.Resources.AdminModeWarning))
                    return;
            }

            switch (Program.RestartAdminMode(args)) {
                case StartResult.Success:
                    Close();
                    break;
                case StartResult.Failure:
                    Popup.Error(Properties.Resources.RestartFail);
                    break;
            }
        }

        private void RestartUserMode() {
            if (!Program.IsAdminMode()) return;

            switch (Program.RestartUserMode()) {
                case StartResult.Success:
                    Close();
                    break;
                case StartResult.Failure:
                    Popup.Error(Properties.Resources.RestartFail);
                    break;
            }
        }

        private static void SetWindowPositionRetry(WindowInfo wndInfo, WindowSettings wndSettings) {
            var wnd = new Window(wndInfo.Handle);

            var begin = DateTime.Now;
            while (!wnd.IsVisible()) {
                var span = DateTime.Now - begin;
                if (span.TotalMilliseconds > SetWindowPositionTimeout)
                    break;
                Thread.Sleep(SetWindowPositionDelay);
            }

            if (wndSettings.ChangeSize)
                wnd.SetPosition(wndSettings.X, wndSettings.Y, wndSettings.Width, wndSettings.Height);
            else
                wnd.SetPosition(wndSettings.X, wndSettings.Y);
        }

        private void SetWindowPosition(IEnumerable<WindowInfo> wndInfos) {
            if (!config.EnableSaveWindowPosition) return;
            if (FFXI.IsFullScreenMode() && FFXI.IsRunning()) return;

            foreach (var wndInfo in wndInfos)
                SetWindowPosition(wndInfo);
        }

        private void SetWindowPosition(WindowInfo wndInfo) {
            foreach (var wndSettings in config.WindowSettingsList) {
                if (wndSettings.Name == wndInfo.Name) {
                    Task.Run(() => {
                        SetWindowPositionRetry(wndInfo, wndSettings);
                    });
                }
            }
        }

        private void SaveWindowPosition(IEnumerable<WindowInfo> wndInfos) {
            if (!config.EnableSaveWindowPosition) return;
            if (FFXI.IsFullScreenMode() && FFXI.IsRunning()) return;

            foreach (var wndInfo in wndInfos)
                SaveWindowPosition(wndInfo);
        }

        private void SaveWindowPosition(WindowInfo wndInfo) {
            if (wndInfo.IsIconic) return;
            if (wndInfo.IsZoomed) return;
            if (!IntersectsWithScreen(wndInfo.Rect)) return;

            foreach (var wndSettings in config.WindowSettingsList) {
                if (wndSettings.Name == wndInfo.Name) {
                    wndSettings.X = wndInfo.Rect.X;
                    wndSettings.Y = wndInfo.Rect.Y;
                    wndSettings.Width = wndInfo.Rect.Width;
                    wndSettings.Height = wndInfo.Rect.Height;
                }
            }
            SaveConfig(config);
        }

        private static bool IntersectsWithScreen(Rectangle rect) {
            foreach (var screen in Screen.AllScreens) {
                if (screen.Bounds.IntersectsWith(rect))
                    return true;
            }
            return false;
        }

        private bool SaveWindowPositionIsEnabled() {
            if (!config.EnableSaveWindowPosition) return false;
            if (FFXI.IsFullScreenMode() && FFXI.IsRunning()) return false;
            return true;
        }

        private void StartRemoteControl() {
            remoteControl.Name = pipeName;
            remoteControl.ServerStart();
        }

        public static RemoteControl GetRemoteControl() {
            return new RemoteControl(pipeName);
        }

        private void HotKeyCapture(GlobalKeyEventArgs e) {
            if (!config.EnableHotKeyCapture) return;

            if (IsKeyDown("Capture", e))
                CaptureSaveToFolder();
        }

        private void HotKeyVolumeControl(GlobalKeyEventArgs e) {
            if (!config.EnableHotKeyVolumeControl) return;

            var mute = IsKeyDownHold("Mute", e);
            var volumeUp = IsKeyDownHold("VolumeUp", e);
            var volumeDown = IsKeyDownHold("VolumeDown", e);

            if (mute || volumeUp || volumeDown) {
                if (!volumeIndicator.Visible)
                    volumeIndicator.Window = GetForegroundVolumeControlWindow();

                if (mute)
                    volumeIndicator.Mute = !volumeIndicator.Mute;
                if (volumeUp)
                    volumeIndicator.Volume += VolumeStep;
                if (volumeDown)
                    volumeIndicator.Volume -= VolumeStep;
                if (volumeUp || volumeDown)
                    e.Repeat = true;
            }
        }

        private Window GetForegroundVolumeControlWindow() {
            var wnd = Window.GetForegroundWindow();
            if (wnd == null) return null;
            if (!IsTargetWindow(wnd)) return null;
            if (wnd.IsIconic()) return null;

            return wnd;
        }

        private bool IsKeyDown(string name, GlobalKeyEventArgs e) {
            return KeyDownTest(name, e.Trigger, e.State);
        }

        private bool IsKeyDownHold(string name, GlobalKeyEventArgs e) {
            return KeyDownTest(name, e.State, e.State);
        }

        private bool KeyDownTest(string name, GlobalKeys trigger, GlobalKeys state) {
            var hotkey = config.GetHotKey(name);

            var down = trigger.Contains(hotkey.Key);
            var ctrl = state.Contains(Keys.ControlKey) == hotkey.Control;
            var shift = state.Contains(Keys.ShiftKey) == hotkey.Shift;
            var alt = state.Contains(Keys.Menu) == hotkey.Alt;

            return down && ctrl && shift && alt;
        }

        private void App_Load(object sender, EventArgs e) {
            Visible = false;
            ApplyConfig(config);
            ResetScreenGamma();
            windowMonitor.Start();
            StartRemoteControl();

            if (OpenSettings)
                EditSettings();
            else if (EnableAutoStartProgram || config.StartFFXI)
                AutoStartProgram();
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e) {
            ResetScreenGamma();
            SaveConfig(config);
        }

        private void uiContextExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void uiContextOption_Click(object sender, EventArgs e) {
            EditSettings();
        }

        private void uiNotifyIcon_Click(object sender, EventArgs e) {
            ShowCotextMenu(uiNotifyIcon);
        }

        private void uiNotifyIcon_BalloonTipClicked(object sender, EventArgs e) {
            NativeMethods.OpenFolderAndSelectItem((string)uiNotifyIcon.Tag);
        }

        private void uiContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            var wnds = GetTargetProcessWindows();
            SetMuteItems(uiContextMute, wnds);

            var capWnds = GetCapturableWindows(wnds);
            SetCapturableItems(uiContextCaptureCopy, capWnds);
            SetCapturableItems(uiContextCaptureSaveAs, capWnds);
            SetCapturableItems(uiContextCaptureSaveFolder, capWnds);

            uiContextStartFFXI.Enabled = !FFXI.IsRunning() || config.StartProgramType == "program";
            uiContextRestartAdminMode.Enabled = !Program.IsAdminMode();
            uiContextRestartUserMode.Enabled = Program.IsAdminMode() && !config.AdminMode;
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
                CaptureSaveToFolder((Window)uiContextCaptureSaveFolder.Tag);
        }

        private void uiContextCaptureSaveFolder_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            CaptureSaveToFolder((Window)e.ClickedItem.Tag);
        }

        private void uiContextMute_Click(object sender, EventArgs e) {
            if (uiContextMute.Tag == null) return;

            var mute = !uiContextMute.Checked;
            var vc = (VolumeControl)uiContextMute.Tag;
            vc.Mute = mute;
            UpdateMuteStates();
            uiContextMute.Checked = mute;
        }

        private void uiContextMute_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (e.ClickedItem.Tag == null) return;

            var vc = (VolumeControl)e.ClickedItem.Tag;
            vc.Mute = !vc.Mute;
            UpdateMuteStates();
        }

        private void uiContextStartFFXI_Click(object sender, EventArgs e) {
            StartProgramAsAdmin();
        }

        private void uiContextRestartAdminMode_Click(object sender, EventArgs e) {
            RestartAdminMode();
        }

        private void uiContextRestartUserMode_Click(object sender, EventArgs e) {
            RestartUserMode();
        }

        private void globalKeyReader_GlobalKeyDown(object sender, GlobalKeyEventArgs e) {
            HotKeyCapture(e);
            HotKeyVolumeControl(e);
        }

        private void windowMonitor_WindowOpend(object sender, WindowMonitorEventArgs e) {
            SetWindowPosition(e.WindowInfo);
        }

        private void windowMonitor_WindowClosed(object sender, WindowMonitorEventArgs e) {
            SaveWindowPosition(e.WindowInfo);
        }

        private void windowMonitor_WindowUpdate(object sender, WindowMonitorEventArgs e) {
            UpdateScreenGamma(e.WindowInfo);
        }

        private void RemoteControl_CommandReceived(object sender, string command) {
            if (command == "StartProgram")
                AutoStartProgram();
        }
    }
}
