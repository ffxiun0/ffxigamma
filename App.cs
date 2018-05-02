using CLParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class App : Form {
        public const string Version = "2.0.1";
        public const string AppName = "FFXI Gamma";
        private const string AppFolderName = "ffxigamma";
        private const string ConfigFileName = "config.xml";
        private const int SetWindowPositionTimeout = 5000;
        private const int SetWindowPositionDelay = 100;
        private const float VolumeStep = 0.02f;

        private Config config;
        private Screen[] prevScreens;
        private Settings editSettings;
        private VolumeIndicator volumeIndicator;

        public bool EnableAutoStartProgram { get; set; } = false;

        public App() {
            InitializeComponent();

            Icon = Properties.Resources.Icon;

            config = LoadConfig();
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
            var validWindows = ExtractValidWindows(wndInfos);
            var usingScreens = FindUsingScreens(validWindows);

            if (ScreenHasChanged(prevScreens, usingScreens))
                SetScreenGamma(usingScreens);

            prevScreens = usingScreens;
        }

        private IEnumerable<WindowInfo> ExtractValidWindows(IEnumerable<WindowInfo> wndInfos) {
            if (FFXI.IsFullScreenMode() && FFXI.IsRunning())
                return new WindowInfo[0];

            var result = from wndInfo in wndInfos
                         where IsValidWindow(wndInfo)
                         select wndInfo;
            return result.ToArray();
        }

        private bool IsValidWindow(WindowInfo wndInfo) {
            if (wndInfo.Window.IsIconic()) return false;

            var ws = config.GetWindowSettings(wndInfo.Name);
            if (ws == null) return false;
            if (ws.AlwaysGamma) return true;

            return IsForegroundWindow(wndInfo);
        }

        private bool IsForegroundWindow(WindowInfo wndInfo) {
            var fg = Window.GetForegroundWindow();
            if (fg == null) return false;

            return fg.GetWindowText() == wndInfo.Name;
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

        private void ApplyConfig(Config config) {
            globalKeyReader.Enabled =
                config.EnableHotKeyCapture || config.EnableHotKeyVolumeControl;

            var names = from ws in config.WindowSettingsList select ws.Name;
            names = new HashSet<string>(names);
            windowMonitor.Filter = wnd => names.Contains(wnd.GetWindowText());
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
                    ApplyConfig(config);
                    ResetScreenGamma();
                    SaveConfig(config);
                    windowMonitor.Reset();
                }
            }
        }

        private static void ShowCotextMenu(NotifyIcon notifyIcon) {
            var type = typeof(NotifyIcon);
            var mi = type.GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
                mi.Invoke(notifyIcon, null);
        }

        private void ShowWarning(string s) {
            MessageBox.Show(null, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowError(string s) {
            MessageBox.Show(null, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ShowYesNoWarning(string s) {
            var ret = MessageBox.Show(null, s, Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            return ret == DialogResult.Yes;
        }

        private IEnumerable<Window> GetTargetWindows() {
            var wnds = from p in Process.GetProcesses()
                       where config.GetWindowSettings(p.MainWindowTitle) != null
                       select new Window(p.MainWindowHandle);
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
            if (wnd.IsIconic()) return false;

            var ws = config.GetWindowSettings(wnd.GetWindowText());
            return ws != null;
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

        private void CaptureSaveFolder() {
            var wnd = Window.GetForegroundWindow();
            CaptureSaveFolder(wnd);
        }

        private void CaptureSaveFolder(Window wnd) {
            if (!IsCapturableWindow(wnd)) return;

            var time = DateTime.Now;
            var path = GetCaptureFileName(time);
            using (var bmp = CaptureWithEffects(wnd, time)) {
                SaveImage(bmp, path);
            }
            ShowNotifySaved(path);
        }

        private string GetCaptureFileName(DateTime time) {
            var folder = config.ImageFolder;
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
            catch (IOException e) {
                ShowError(e.Message);
            }
            catch (UnauthorizedAccessException ex) {
                ShowError(ex.Message);
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
                ShowError(Properties.Resources.ProgramStartFail);
        }

        private void StartProgramFFXI() {
            if (FFXI.IsRunning()) {
                ShowWarning(Properties.Resources.FFXIAlreadyRunning);
                return;
            }

            if (FFXI.Start() == StartResult.Failure)
                ShowError(Properties.Resources.FFXIStartFail);
        }

        private void StartProgramCustom() {
            if (!Program.IsAdminMode() && FFXI.IsRunning()) {
                if (!ShowYesNoWarning(Properties.Resources.AdminModeWarning))
                    return;
            }

            var cl = CommandLine.Parse(config.StartProgramCommandLine);
            if (cl == null) {
                ShowError(Properties.Resources.CommandLineFormattError);
                return;
            }

            if (!cl.IsEmpty) {
                if (ProcessEx.Start(cl.Exe, cl.Args) == StartResult.Failure)
                    ShowError(Properties.Resources.ProgramStartFail);
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
                if (!ShowYesNoWarning(Properties.Resources.AdminModeWarning))
                    return;
            }

            if (Program.RestartAdminMode(args) == StartResult.Success)
                Close();
        }

        private void RestartUserMode() {
            if (!Program.IsAdminMode()) return;

            if (Program.RestartUserMode() == StartResult.Success)
                Close();
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
            foreach(var screen in Screen.AllScreens) {
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
            LifetimeServices.LeaseTime = TimeSpan.Zero;

            var channel = new IpcServerChannel("ffxigamma");
            ChannelServices.RegisterChannel(channel, true);
            var remote = new RemoteControl(this);
            RemotingServices.Marshal(remote, "remote-control", typeof(RemoteControl));
        }

        public static RemoteControl GetRemoteControl() {
            var channel = new IpcClientChannel();
            ChannelServices.RegisterChannel(channel, true);
            var remote = (RemoteControl)Activator.GetObject(typeof(RemoteControl),
                "ipc://ffxigamma/remote-control");
            return remote;
        }

        private void HotKeyCapture(GlobalKeyEventArgs e) {
            if (!config.EnableHotKeyCapture) return;

            if (IsKeyDown("Capture", e))
                CaptureSaveFolder();
        }

        private void HotKeyVolumeControl(GlobalKeyEventArgs e) {
            if (!config.EnableHotKeyVolumeControl) return;

            var mute = IsKeyDownHold("Mute", e);
            var volumeUp = IsKeyDownHold("VolumeUp", e);
            var volumeDown = IsKeyDownHold("VolumeDown", e);

            if (mute || volumeUp || volumeDown) {
                if (!volumeIndicator.Visible)
                    volumeIndicator.Window = GetForegroundWindow();

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

        private Window GetForegroundWindow() {
            var wnd = Window.GetForegroundWindow();
            if (wnd == null) return null;

            var ws = config.GetWindowSettings(wnd.GetWindowText());
            if (ws == null) return null;

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

            if (EnableAutoStartProgram || config.StartFFXI)
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
            var wnds = GetTargetWindows();
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
                CaptureSaveFolder((Window)uiContextCaptureSaveFolder.Tag);
        }

        private void uiContextCaptureSaveFolder_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            CaptureSaveFolder((Window)e.ClickedItem.Tag);
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
    }
}
