﻿/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ffxigamma {
    class NativeMethods {
        public static string GetModuleFileName() {
            var sb = new StringBuilder(1024);

            while (true) {
                var len = GetModuleFileName(IntPtr.Zero, sb, (uint)sb.Capacity);
                if (len == 0)
                    throw new Exception("GetModuleFileName");
                else if (len < sb.Capacity)
                    return sb.ToString();

                sb.Capacity *= 2;
            }
        }

        public static string GetWindowText(IntPtr hWnd) {
            int len = GetWindowTextLength(hWnd);
            var sb = new StringBuilder(len + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        public static string GetClassName(IntPtr hWnd) {
            var sb = new StringBuilder(128);

            while (true) {
                var len = GetClassName(hWnd, sb, sb.Capacity);
                if (len <= 0)
                    return "";
                else if (len < sb.Capacity)
                    return sb.ToString();

                sb.Capacity *= 2;
            }
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
            using (var hDC = CreateDC(driver, device, null, IntPtr.Zero)) {
                if (hDC.IsInvalid) return false;

                return SetDeviceGammaRamp(hDC, gamma);
            }
        }

        public static bool SetDeviceGammaRamp(SafeDeviceContextHandle hDC, double gamma) {
            var ramp = new ushort[256 * 3];
            for (int i = 0; i < 256; i++) {
                ushort gi = (ushort)Math.Floor(Math.Pow(i / 255.0, gamma) * 255.0 + 0.5);
                ramp[i] = ramp[i + 256] = ramp[i + 512] = (ushort)(gi << 8);
            }
            return SetDeviceGammaRamp(hDC, ramp);
        }

        public static int BitBlt(Graphics dest, Rectangle rect, Graphics src, Point srcPos, int rop) {
            using (var hdcDest = dest.GetHdcSafe())
            using (var hdcSrc = src.GetHdcSafe()) {
                return NativeMethods.BitBlt(hdcDest, rect.X, rect.Y, rect.Width, rect.Height,
                    hdcSrc, srcPos.X, srcPos.Y, rop);
            }
        }

        public static Image GetShieldIcon() {
            using (var hIcon = GetStockIcon(SIID_SHIELD, SHGSI_ICON | SHGSI_SMALLICON)) {
                if (hIcon.IsInvalid) return null;

                var icon = Icon.FromHandle(hIcon.DangerousGetHandle());
                return icon.ToBitmap();
            }
        }

        private static SafeIconHandle GetStockIcon(int siid, uint uFlags) {
            var sii = new SHSTOCKICONINFO();
            sii.cbSize = (uint)Marshal.SizeOf(sii);
            try {
                var hr = SHGetStockIconInfo(siid, uFlags, ref sii);
                Marshal.ThrowExceptionForHR(hr);
                return new SafeIconHandle(sii.hIcon);
            }
            catch (Exception) {
                return new SafeIconHandle(IntPtr.Zero);
            }
        }

        public static bool OpenFolderAndSelectItem(string path) {
            using (var pidl = ILCreateFromPath(path)) {
                if (pidl.IsInvalid) return false;

                try {
                    var hr = SHOpenFolderAndSelectItems(pidl, 0, IntPtr.Zero, 0);
                    Marshal.ThrowExceptionForHR(hr);
                    return true;
                }
                catch (Exception) {
                    return false;
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

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
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetWindowThreadProcessId(
            IntPtr hWnd,
            out uint lpdwProcessId
            );

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, IntPtr lParam);
        public delegate bool WNDENUMPROC(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags
            );
        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOREDRAW = 0x0008;
        public const uint SWP_NOACTIVATE = 0x0010;
        public const uint SWP_FRAMECHANGED = 0x0020;
        public const uint SWP_SHOWWINDOW = 0x0040;
        public const uint SWP_HIDEWINDOW = 0x0080;
        public const uint SWP_NOCOPYBITS = 0x0100;
        public const uint SWP_NOOWNERZORDER = 0x0200;
        public const uint SWP_NOSENDCHANGING = 0x0400;
        public const uint SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const uint SWP_NOREPOSITION = SWP_NOOWNERZORDER;
        public const uint SWP_DEFERERASE = 0x2000;
        public const uint SWP_ASYNCWINDOWPOS = 0x4000;
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        public const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern SafeCreateDCHandle CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool SetDeviceGammaRamp(SafeDeviceContextHandle hDC, ushort[] lpRamp);

        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
            SafeDeviceContextHandle hDestDC,
            int x,
            int y,
            int nWidth,
            int nHeight,
            SafeDeviceContextHandle hSrcDC,
            int xSrc,
            int ySrc,
            int dwRop);
        public const int SRCCOPY = 13369376;

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetCurrentObject(SafeDeviceContextHandle hdc, uint uObjectType);
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
        public static extern int SHGetStockIconInfo(int siid, uint uFlags, ref SHSTOCKICONINFO psii);

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

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        public extern static IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            int nShowCmd
            );

        public const int SW_HIDE = 0;
        public const int SW_MAXIMIZE = 3;
        public const int SW_MINIMIZE = 6;
        public const int SW_RESTORE = 9;
        public const int SW_SHOW = 5;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOWNORMAL = 1;

        [DllImport("shell32.dll")]
        public static extern int SHOpenFolderAndSelectItems(SafeItemIdList pidlFolder, uint cidl, IntPtr apidl, uint dwFlags);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern SafeItemIdList ILCreateFromPath(string pszPath);

        [DllImport("shell32.dll")]
        public static extern void ILFree(IntPtr pidl);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsUserAnAdmin();

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetModuleFileName(
            IntPtr hModule,
            StringBuilder lpFilename,
            uint nSize
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern SafeProcessHandle OpenProcess(
            uint dwDesiredAccess,
            bool bInheritHandle,
            uint dwProcessId
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetLastError();

        public const int ERROR_SUCCESS = 0;
        public const int ERROR_CANCELLED = 1223;

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        public static extern bool OpenProcessToken(
            SafeProcessHandle ProcessHandle,
            uint DesiredAccess,
            out SafeAccessTokenHandle TokenHandle
            );
        public const uint MAXIMUM_ALLOWED = 0x02000000;

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        public static extern bool DuplicateTokenEx(
            SafeAccessTokenHandle hExistingToken,
            uint dwDesiredAccess,
            IntPtr lpTokenAttributes,
            SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
            TOKEN_TYPE TokenType,
            out SafeAccessTokenHandle phNewToken
            );

        public enum TOKEN_TYPE {
            TokenPrimary = 1,
            TokenImpersonation,
        }

        public enum SECURITY_IMPERSONATION_LEVEL {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation,
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CreateProcessWithToken(
            SafeAccessTokenHandle hToken,
            uint dwLogonFlags,
            string lpApplicationName,
            StringBuilder lpCommandLine,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFOW lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInfo
            );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFOW {
            public uint cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public int wShowWindow;
            public int cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }
    }
}
