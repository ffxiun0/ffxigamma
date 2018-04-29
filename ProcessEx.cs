using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ffxigamma {
    enum StartResult {
        Success,
        Failure,
        Cancelled,
    }

    class ProcessEx {
        public static bool IsUserAnAdmin() {
            return NativeMethods.IsUserAnAdmin();
        }

        public static StartResult Start(string exe, params string[] args) {
            try {
                var psi = new ProcessStartInfo(exe);
                psi.Arguments = CommandLine.ToString(args);
                psi.WorkingDirectory = GetCurrentDirectory(exe);
                Process.Start(psi);
                return StartResult.Success;
            }
            catch (Win32Exception e) {
                return ToStartResult(e.NativeErrorCode);
            }
        }

        public static StartResult StartAdmin(string exe, params string[] args) {
            try {
                var psi = new ProcessStartInfo(exe);
                psi.Arguments = CommandLine.ToString(args);
                psi.WorkingDirectory = GetCurrentDirectory(exe);
                psi.Verb = "runas";
                Process.Start(psi);
                return StartResult.Success;
            }
            catch (Win32Exception e) {
                return ToStartResult(e.NativeErrorCode);
            }
        }

        public static StartResult StartUser(string exe, params string[] args) {
            if (!IsUserAnAdmin()) return Start(exe, args);

            var hToken = DuplicateShellToken();
            if (hToken == IntPtr.Zero) return StartResult.Failure;
            try {
                return StartWithToken(hToken, exe, args);
            }
            finally {
                NativeMethods.CloseHandle(hToken);
            }
        }

        private static string GetCurrentDirectory(string path) {
            try {
                return Path.GetDirectoryName(path);
            }
            catch (ArgumentException) {
                return "";
            }
        }

        private static StartResult ToStartResult(int errorCode) {
            if (errorCode == NativeMethods.ERROR_CANCELLED)
                return StartResult.Cancelled;
            else
                return StartResult.Failure;
        }

        private static IntPtr DuplicateShellToken() {
            var hToken = GetShellToken();
            if (hToken == IntPtr.Zero) return IntPtr.Zero;
            try {
                return DuplicateToken(hToken);
            }
            finally {
                NativeMethods.CloseHandle(hToken);
            }
        }

        private static IntPtr GetShellToken() {
            var pid = GetShellProcessId();
            var hProcess = NativeMethods.OpenProcess(NativeMethods.MAXIMUM_ALLOWED, false, pid);
            if (hProcess == IntPtr.Zero) return IntPtr.Zero;
            try {
                IntPtr hToken;
                var ok = NativeMethods.OpenProcessToken(hProcess, NativeMethods.MAXIMUM_ALLOWED, out hToken);
                return ok ? hToken : IntPtr.Zero;
            }
            finally {
                NativeMethods.CloseHandle(hProcess);
            }
        }

        private static uint GetShellProcessId() {
            var hwnd = NativeMethods.GetShellWindow();
            uint pid;
            NativeMethods.GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }

        private static IntPtr DuplicateToken(IntPtr hToken) {
            IntPtr hDupToken;
            var ok = NativeMethods.DuplicateTokenEx(hToken, NativeMethods.MAXIMUM_ALLOWED, IntPtr.Zero,
                NativeMethods.SECURITY_IMPERSONATION_LEVEL.SecurityDelegation,
                NativeMethods.TOKEN_TYPE.TokenPrimary, out hDupToken);
            return ok ? hDupToken : IntPtr.Zero;
        }

        private static StartResult StartWithToken(IntPtr hToken, string exe, params string[] args) {
            var commandLine = new StringBuilder(1024 + 1);
            commandLine.Append(CommandLine.ToString(exe, args));

            var dir = GetCurrentDirectory(exe);
            var si = new NativeMethods.STARTUPINFOW();
            si.cb = (uint)Marshal.SizeOf(si);
            var pi = new NativeMethods.PROCESS_INFORMATION();

            var ok = NativeMethods.CreateProcessWithToken(hToken, 0, exe, commandLine,
                0, IntPtr.Zero, dir, ref si, out pi);

            if (ok)
                return StartResult.Success;
            else
                return ToStartResult(NativeMethods.GetLastError());
        }
    }
}
