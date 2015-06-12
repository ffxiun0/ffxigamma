using System;
using System.Drawing;

namespace ffxigamma {
    class WindowInfo {
        public string Name { get; set; }
        public IntPtr Handle { get; set; }
        public int ProcessId { get; set; }
        public Rectangle Rect { get; set; }
        public DateTime Time { get; set; }

        public WindowInfo(Window wnd) {
            Name = wnd.GetWindowText();
            Handle = wnd.Handle;
            ProcessId = wnd.GetProcessId();
            Rect = wnd.GetWindowRect();
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
