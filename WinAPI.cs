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

        public static Rectangle GetClientRect(IntPtr hWnd) {
            var rect = new RECT();
            GetClientRect(hWnd, ref rect);
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

        public static int BitBlt(Graphics dest, Rectangle rect, Graphics src, Point srcPos, int rop) {
            var hdcDest = dest.GetHdc();
            var hdcSrc = src.GetHdc();
            try {
                return WinAPI.BitBlt(hdcDest, rect.X, rect.Y, rect.Width, rect.Height,
                    hdcSrc, srcPos.X, srcPos.Y, rop);
            }
            finally {
                dest.ReleaseHdc(hdcDest);
                src.ReleaseHdc(hdcSrc);
            }
        }

        public static Image GetShieldIcon() {
            var sii = new SHSTOCKICONINFO();
            sii.cbSize = (uint)Marshal.SizeOf(sii);

            SHGetStockIconInfo(SIID_SHIELD, SHGSI_ICON | SHGSI_SMALLICON, ref sii);
            if (sii.hIcon == IntPtr.Zero) return null;

            var icon = Icon.FromHandle(sii.hIcon);
            return icon.ToBitmap();
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
        public static extern int GetClientRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool SetDeviceGammaRamp(IntPtr hDC, ushort[] lpRamp);

        [DllImport("gdi32.dll")]
        public static extern bool GetDeviceGammaRamp(IntPtr hDC, ushort[] lpRamp);

        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
            IntPtr hDestDC,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hSrcDC,
            int xSrc,
            int ySrc,
            int dwRop);
        public const int SRCCOPY = 13369376;

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetCurrentObject(IntPtr hdc, uint uObjectType);
        public const uint OBJ_BITMAP = 7;

        [DllImport("gdi32.dll")]
        public static extern int GetObject(IntPtr hgdiobj, int cbBuffer, ref BITMAP lpvObject);

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAP {
            public int bmType;
            public int bmWidth;
            public int bmHeight;
            public int bmWidthBytes;
            byte bmPlanes;
            byte bmBitsPixel;
            IntPtr bmBits;
        };

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetStockIconInfo(int siid, uint uFlags, ref SHSTOCKICONINFO psii);

        public const int SIID_SHIELD = 77;

        public const uint SHGSI_LARGEICON = 0x000000000;
        public const uint SHGSI_SMALLICON = 0x000000001;
        public const uint SHGSI_ICON = 0x000000100;

        public const int MAX_PATH = 260;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO {
            public uint cbSize;
            public IntPtr hIcon;
            public int iSysImageIndex;
            public int iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szPath;
        }
    }
}
