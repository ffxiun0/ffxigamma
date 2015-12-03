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

        public void Reset() {
            map.Clear();
        }

        public void Update() {
            var opened = UpdateWindows();
            NotifyWindowOpened(opened);

            var closed = CleanupClosedWindows();
            NotifyWindowClosed(closed);
        }

        public IEnumerable<WindowInfo> UpdateWindows() {
            var opened = new List<WindowInfo>();
            foreach (var wndInfo in GetTargetWindows()) {
                if (!map.ContainsKey(wndInfo))
                    opened.Add(wndInfo);
                map[wndInfo] = wndInfo;
            }
            return opened;
        }

        private IEnumerable<WindowInfo> GetTargetWindows() {
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

        private void NotifyWindowClosed(IEnumerable<WindowInfo> closed) {
            if (WindowClosed == null) return;

            foreach (var wndInfo in closed) {
                var closeArgs = new WindowMonitorEventArgs(wndInfo);
                WindowClosed(this, closeArgs);
            }
        }

        private void NotifyWindowOpened(IEnumerable<WindowInfo> opened) {
            if (WindowOpened == null) return;

            foreach (var wndInfo in opened) {
                var openArgs = new WindowMonitorEventArgs(wndInfo);
                WindowOpened(this, openArgs);
            }
        }
    }
}
