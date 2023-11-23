/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using CLParser;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

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
            return Start(null, exe, args);
        }

        public static StartResult StartAdmin(string exe, params string[] args) {
            return Start("runas", exe, args);
        }

        private static StartResult Start(string operation, string exe, params string[] args) {
            var strArgs = CommandLine.ToString(args);
            var dir = GetCurrentDirectory(exe);
            NativeMethods.ShellExecute(IntPtr.Zero, operation, exe, strArgs, dir, NativeMethods.SW_SHOWNORMAL);
            return ToStartResult(NativeMethods.GetLastError());
        }

        public static StartResult StartUser(string exe, params string[] args) {
            if (!IsUserAnAdmin()) return Start(exe, args);

            using (var hToken = DuplicateShellToken()) {
                if (hToken.IsInvalid) return StartResult.Failure;

                return StartWithToken(hToken, exe, args);
            }
        }

        private static string GetCurrentDirectory(string path) {
            if (IsPathFullyQualified(path)) {
                var pathDir = Path.GetDirectoryName(path);
                if (pathDir.Length >= 0)
                    return pathDir;
            }
            return Environment.CurrentDirectory;
        }

        private static bool IsPathFullyQualified(string path) {
            return Regex.IsMatch(path, @"^[a-zA-Z]:[\\/]|^[\\/]{2}[^\\/]+[\\/]");
        }

        private static StartResult ToStartResult(int errorCode) {
            switch (errorCode) {
                case NativeMethods.ERROR_SUCCESS:
                    return StartResult.Success;
                case NativeMethods.ERROR_CANCELLED:
                    return StartResult.Cancelled;
                default:
                    return StartResult.Failure;
            }
        }

        private static SafeAccessTokenHandle DuplicateShellToken() {
            using (var hToken = OpenShellToken()) {
                if (hToken.IsInvalid) return SafeAccessTokenHandle.InvalidHandle;

                return DuplicateToken(hToken);
            }
        }

        private static SafeAccessTokenHandle OpenShellToken() {
            var pid = GetShellProcessId();
            using (var hProcess = NativeMethods.OpenProcess(NativeMethods.MAXIMUM_ALLOWED, false, pid)) {
                if (hProcess.IsInvalid) return SafeAccessTokenHandle.InvalidHandle;

                SafeAccessTokenHandle hToken;
                var ok = NativeMethods.OpenProcessToken(hProcess, NativeMethods.MAXIMUM_ALLOWED, out hToken);
                return ok ? hToken : SafeAccessTokenHandle.InvalidHandle;
            }
        }

        private static uint GetShellProcessId() {
            var hwnd = NativeMethods.GetShellWindow();
            uint pid;
            NativeMethods.GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }

        private static SafeAccessTokenHandle DuplicateToken(SafeAccessTokenHandle hToken) {
            SafeAccessTokenHandle hDupToken;
            var ok = NativeMethods.DuplicateTokenEx(hToken, NativeMethods.MAXIMUM_ALLOWED, IntPtr.Zero,
                NativeMethods.SECURITY_IMPERSONATION_LEVEL.SecurityDelegation,
                NativeMethods.TOKEN_TYPE.TokenPrimary, out hDupToken);
            return ok ? hDupToken : SafeAccessTokenHandle.InvalidHandle;
        }

        private static StartResult StartWithToken(SafeAccessTokenHandle hToken, string exe, params string[] args) {
            var commandLine = new StringBuilder(1024 + 1);
            commandLine.Append(CommandLine.ToString(exe, args));

            var dir = GetCurrentDirectory(exe);
            var si = new NativeMethods.STARTUPINFOW();
            si.cb = (uint)Marshal.SizeOf(si);
            var pi = new NativeMethods.PROCESS_INFORMATION();

            var ok = NativeMethods.CreateProcessWithToken(hToken, 0, null, commandLine,
                0, IntPtr.Zero, dir, ref si, out pi);

            if (ok) {
                NativeMethods.CloseHandle(pi.hProcess);
                NativeMethods.CloseHandle(pi.hThread);
                return StartResult.Success;
            } else {
                return ToStartResult(NativeMethods.GetLastError());
            }
        }
    }
}
