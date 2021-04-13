/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ffxigamma {
    public class Window {
        private IntPtr hWnd;

        public Window(IntPtr hWnd) {
            this.hWnd = hWnd;
        }

        public IntPtr Handle {
            get {
                return hWnd;
            }
        }

        public string GetWindowText() {
            return NativeMethods.GetWindowText(hWnd);
        }

        public string GetClassName() {
            return NativeMethods.GetClassName(hWnd);
        }

        public Rectangle GetWindowRect() {
            return NativeMethods.GetWindowRect(hWnd);
        }

        public Rectangle GetClientRect() {
            return NativeMethods.GetClientRect(hWnd);
        }

        public bool SetPosition(int x, int y) {
            return NativeMethods.SetWindowPos(hWnd, IntPtr.Zero, x, y, 0, 0, NativeMethods.SWP_NOSIZE);
        }

        public bool SetPosition(int x, int y, int w, int h) {
            return NativeMethods.SetWindowPos(hWnd, IntPtr.Zero, x, y, w, h, 0);
        }

        public bool IsIconic() {
            return NativeMethods.IsIconic(hWnd);
        }

        public bool IsZoomed() {
            return NativeMethods.IsZoomed(hWnd);
        }

        public bool IsVisible() {
            return NativeMethods.IsWindowVisible(hWnd);
        }

        public bool IsForeground() {
            var fg = GetForegroundWindow();
            if (fg == null) return false;

            return fg.Handle == Handle;
        }

        public bool IsExplorer() {
            var name = GetClassName();
            return name == "CabinetWClass" || name == "ExploreWClass";
        }

        public int GetProcessId() {
            uint pid;
            NativeMethods.GetWindowThreadProcessId(hWnd, out pid);
            return (int)pid;
        }

        public Bitmap Capture() {
            if (IsIconic()) return null;

            var rect = GetRealResolutionClientRect();
            var bmp = CreateRealResolutionBitmap(rect);

            using (var gBmp = Graphics.FromImage(bmp))
            using (var gWnd = Graphics.FromHwnd(hWnd)) {
                NativeMethods.BitBlt(gBmp, rect, gWnd, Point.Empty, NativeMethods.SRCCOPY);
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
                    var hbmp = NativeMethods.GetCurrentObject(hdc, NativeMethods.OBJ_BITMAP);
                    if (hbmp == IntPtr.Zero)
                        return Rectangle.Empty;

                    var sbmp = new NativeMethods.BITMAP();
                    if (NativeMethods.GetObject(hbmp, Marshal.SizeOf(sbmp), ref sbmp) == 0)
                        return Rectangle.Empty;

                    return new Rectangle(0, 0, sbmp.bmWidth, sbmp.bmHeight);
                }
                finally {
                    g.ReleaseHdc(hdc);
                }
            }
        }

        public static Window FindWindow(string className, string windowName) {
            var hWnd = NativeMethods.FindWindow(className, windowName);
            if (hWnd == IntPtr.Zero) return null;
            return new Window(hWnd);
        }

        public static IEnumerable<Window> EnumWindows() {
            var result = new List<Window>();

            NativeMethods.EnumWindows((hWnd, lParam) => {
                result.Add(new Window(hWnd));
                return true;
            }, IntPtr.Zero);

            return result;
        }

        public static Window GetForegroundWindow() {
            IntPtr hWnd = NativeMethods.GetForegroundWindow();
            if (hWnd == IntPtr.Zero) return null;
            return new Window(hWnd);
        }
    }
}
