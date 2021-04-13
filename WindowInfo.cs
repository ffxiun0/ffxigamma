/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Drawing;

namespace ffxigamma {
    class WindowInfo {
        public Window Window { get; set; }
        public string Name { get; set; }
        public IntPtr Handle { get; set; }
        public int ProcessId { get; set; }
        public Rectangle Rect { get; set; }
        public bool IsIconic { get; set; }
        public bool IsZoomed { get; set; }
        public DateTime Time { get; set; }

        public WindowInfo(Window wnd) {
            Window = wnd;
            Name = wnd.GetWindowText();
            Handle = wnd.Handle;
            ProcessId = wnd.GetProcessId();
            Rect = wnd.GetWindowRect();
            IsIconic = wnd.IsIconic();
            IsZoomed = wnd.IsZoomed();
            Time = DateTime.Now;
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            var wi = (WindowInfo)obj;
            if (wi == null) return false;
            return Name == wi.Name && Handle == wi.Handle && ProcessId == wi.ProcessId;
        }

        public override int GetHashCode() {
            return Name.GetHashCode() ^ Handle.GetHashCode() ^ ProcessId.GetHashCode();
        }
    }
}
