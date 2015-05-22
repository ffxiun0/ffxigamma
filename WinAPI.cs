using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ffxigamma {
    class WinAPI {
        public static string GetWindowText(IntPtr hWnd) {
            int len = GetWindowTextLength(hWnd);
            var sb = new StringBuilder(len + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        public static Rectangle GetWindowRect(IntPtr hWnd) {
            var rect = new RECT();
            GetWindowRect(hWnd, ref rect);
            return new Rectangle(rect.left, rect.top,
                rect.right - rect.left, rect.bottom - rect.top);
        }

        public static bool SetDeviceGammaRamp(Screen screen, double gamma) {
            return SetDeviceGammaRamp("DISPLAY", screen.DeviceName, gamma);
        }

        public static bool SetDeviceGammaRamp(string driver, string device, double gamma) {
            var hDC = CreateDC(driver, device, null, IntPtr.Zero);
            if (hDC == IntPtr.Zero) return false;
            try {
                return SetDeviceGammaRamp(hDC, gamma);
            }
            finally {
                DeleteDC(hDC);
            }
        }

        public static bool SetDeviceGammaRamp(IntPtr hDC, double gamma) {
            var ramp = new ushort[256 * 3];
            for (int i = 0; i < 256; i++) {
                ushort gi = (ushort)Math.Floor(Math.Pow(i / 255.0, gamma) * 255.0 + 0.5);
                ramp[i] = ramp[i + 256] = ramp[i + 512] = (ushort)(gi << 8);
            }
            return SetDeviceGammaRamp(hDC, ramp);
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool SetDeviceGammaRamp(IntPtr hDC, ushort[] lpRamp);

        [DllImport("gdi32.dll")]
        public static extern bool GetDeviceGammaRamp(IntPtr hDC, ref ushort[] lpRamp);
    }
}
