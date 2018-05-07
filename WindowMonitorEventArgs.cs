/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Collections.Generic;

namespace ffxigamma {
    class WindowMonitorEventArgs : EventArgs {
        public WindowMonitorEventArgs(IEnumerable<WindowInfo> wndInfos) {
            WindowInfo = wndInfos;
        }

        public IEnumerable<WindowInfo> WindowInfo { get; set; }
    }
}
