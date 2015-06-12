using System;

namespace ffxigamma {
    class WindowMonitorEventArgs : EventArgs {
        public WindowMonitorEventArgs(WindowInfo wndInfo) {
            WindowInfo = wndInfo;
        }

        public WindowInfo WindowInfo { get; set; }
    }
}
