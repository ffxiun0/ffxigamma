using System;
using System.Collections.Generic;
using System.Linq;

namespace ffxigamma {
    delegate void WindowMonitorDelegate(object sender, WindowMonitorEventArgs e);

    class WindowMonitor {
        private HashSet<string> names;
        private Dictionary<WindowInfo, WindowInfo> map;

        public WindowMonitor() {
            names = new HashSet<string>();
            map = new Dictionary<WindowInfo, WindowInfo>();
            Expire = 1000;
        }

        public int Expire { get; set; }

        public IEnumerable<string> Names {
            get {
                return names.ToArray();
            }
            set {
                names = new HashSet<string>(value);
            }
        }

        public event WindowMonitorDelegate WindowOpened;
        public event WindowMonitorDelegate WindowClosed;
        public event WindowMonitorDelegate WindowUpdate;

        public void Reset() {
            map.Clear();
        }

        public void Update() {
            var targets = FindTargetWindows();
            var opened = UpdateWindows(targets);
            var closed = CleanupClosedWindows();

            NotifyWindowOpened(opened);
            NotifyWindowClosed(closed);
            NotifyWindowUpdate(targets);
        }

        public IEnumerable<WindowInfo> UpdateWindows(IEnumerable<WindowInfo> wndInfos) {
            var opened = new List<WindowInfo>();
            foreach (var wndInfo in wndInfos) {
                if (!map.ContainsKey(wndInfo))
                    opened.Add(wndInfo);
                map[wndInfo] = wndInfo;
            }
            return opened;
        }

        private IEnumerable<WindowInfo> FindTargetWindows() {
            return from wnd in Window.EnumWindows()
                   where names.Contains(wnd.GetWindowText())
                   select new WindowInfo(wnd);
        }

        public IEnumerable<WindowInfo> CleanupClosedWindows() {
            var now = DateTime.Now;
            var closed = (from wndInfo in map.Values
                          where (now - wndInfo.Time).TotalMilliseconds > Expire
                          select wndInfo).ToArray();

            foreach (var wndInfo in closed)
                map.Remove(wndInfo);

            return closed;
        }

        private void NotifyWindowOpened(IEnumerable<WindowInfo> opened) {
            if (WindowOpened == null) return;

            var args = new WindowMonitorEventArgs(opened);
            WindowOpened(this, args);
        }

        private void NotifyWindowClosed(IEnumerable<WindowInfo> closed) {
            if (WindowClosed == null) return;

            var args = new WindowMonitorEventArgs(closed);
            WindowClosed(this, args);
        }

        private void NotifyWindowUpdate(IEnumerable<WindowInfo> updates) {
            if (WindowUpdate == null) return;

            var args = new WindowMonitorEventArgs(updates);
            WindowUpdate(this, args);
        }
    }
}
