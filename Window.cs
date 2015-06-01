using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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

        public Rectangle GetClientRect() {
            return WinAPI.GetClientRect(hWnd);
        }

        public static Window GetForegroundWindow() {
            IntPtr hWnd = WinAPI.GetForegroundWindow();
            if (hWnd == IntPtr.Zero) return null;
            return new Window(hWnd);
        }

        public bool IsIconic() {
            return WinAPI.IsIconic(hWnd);
        }

        public Bitmap Capture() {
            if (IsIconic()) return null;

            var rect = GetRealResolutionClientRect();
            var bmp = CreateRealResolutionBitmap(rect);

            using (var gBmp = Graphics.FromImage(bmp))
            using (var gWnd = Graphics.FromHwnd(hWnd)) {
                WinAPI.BitBlt(gBmp, rect, gWnd, Point.Empty, WinAPI.SRCCOPY);
            }

            return bmp;
        }

        private Bitmap CreateRealResolutionBitmap(Rectangle rect) {
            var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

            var scale = GetCompatibilityScreenScaling();
            var dpiX = bmp.HorizontalResolution / scale.X;
            var dpiY = bmp.VerticalResolution / scale.Y;
            bmp.SetResolution(dpiX, dpiY);

            return bmp;
        }

        private Rectangle GetRealResolutionClientRect() {
            var rect = GetClientRect();
            var scale = GetCompatibilityScreenScaling();

            var width = (int)Math.Floor(rect.Width / scale.X + 0.5);
            var height = (int)Math.Floor(rect.Height / scale.Y + 0.5);

            return new Rectangle(0, 0, width, height);
        }

        private PointF GetCompatibilityScreenScaling() {
            var win = GetWindowRect();
            var dev = GetDeviceRect();

            if (dev.IsEmpty)
                return new PointF(1, 1);

            var scaleX = (float)win.Width / dev.Width;
            var scaleY = (float)win.Height / dev.Height;

            return new PointF(scaleX, scaleY);
        }

        private Rectangle GetDeviceRect() {
            using (var g = Graphics.FromHwnd(hWnd)) {
                var hdc = g.GetHdc();
                try {
                    var hbmp = WinAPI.GetCurrentObject(hdc, WinAPI.OBJ_BITMAP);
                    if (hbmp == IntPtr.Zero)
                        return Rectangle.Empty;

                    var sbmp = new WinAPI.BITMAP();
                    if (WinAPI.GetObject(hbmp, Marshal.SizeOf(sbmp), ref sbmp) == 0)
                        return Rectangle.Empty;

                    return new Rectangle(0, 0, sbmp.bmWidth, sbmp.bmHeight);
                }
                finally {
                    g.ReleaseHdc(hdc);
                }
            }
        }
    }
}
