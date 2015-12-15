using System;
using System.Collections.Generic;
using System.Linq;

namespace ffxigamma {
    class WindowMonitorEventArgs : EventArgs {
        public WindowMonitorEventArgs(IEnumerable<WindowInfo> wndInfos) {
            WindowInfo = wndInfos;
        }

        public IEnumerable<WindowInfo> WindowInfo { get; set; }
    }
}
