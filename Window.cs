using System;
using System.Drawing;

namespace ffxigamma {
    class Window {
        private IntPtr hWnd;

        public Window(IntPtr hWnd) {
            this.hWnd = hWnd;
        }

        public string GetWindowText() {
            return WinAPI.GetWindowText(hWnd);
        }

        public Rectangle GetWindowRect() {
            return WinAPI.GetWindowRect(hWnd);
        }

        public static Window GetForegroundWindow() {
            IntPtr hWnd = WinAPI.GetForegroundWindow();
            if (hWnd == IntPtr.Zero) return null;
            return new Window(hWnd);
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            if (obj == null) return false;
            var w = obj as Window;
            if (w == null) return false;
            return hWnd == w.hWnd;
        }

        public override int GetHashCode() {
            return hWnd.GetHashCode();
        }
    }
}
