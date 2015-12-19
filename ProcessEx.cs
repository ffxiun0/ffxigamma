using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ffxigamma {
    class ProcessEx {
        public static bool IsUserAnAdmin() {
            return NativeMethods.IsUserAnAdmin();
        }

        public static bool Start(string exe, params string[] args) {
            try {
                var psi = new ProcessStartInfo(exe);
                psi.Arguments = QuoteArguments(args);
                Process.Start(psi);
                return true;
            }
            catch (Win32Exception) {
                return false;
            }
        }

        public static bool StartAdmin(string exe, params string[] args) {
            try {
                var psi = new ProcessStartInfo(exe);
                psi.Arguments = QuoteArguments(args);
                psi.Verb = "runas";
                Process.Start(psi);
                return true;
            }
            catch (Win32Exception) {
                return false;
            }
        }

        public static bool StartUser(string exe, params string[] args) {
            if (!IsUserAnAdmin()) return Start(exe, args);

            var hToken = DuplicateShellToken();
            if (hToken == IntPtr.Zero) return false;
            try {
                return StartWithToken(hToken, exe, args);
            }
            finally {
                NativeMethods.CloseHandle(hToken);
            }
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

        private static bool StartWithToken(IntPtr hToken, string exe, params string[] args) {
            var commandLine = new StringBuilder(1024 + 1);
            commandLine.Append(MakeCommandLine(exe, args));

            var si = new NativeMethods.STARTUPINFOW();
            si.cb = (uint)Marshal.SizeOf(si);
            var pi = new NativeMethods.PROCESS_INFORMATION();

            return NativeMethods.CreateProcessWithToken(hToken, 0, exe, commandLine,
                0, IntPtr.Zero, null, ref si, out pi);
        }

        private static string MakeCommandLine(string exe, params string[] args) {
            var commandLine = Concat(exe, args);
            return QuoteArguments(commandLine);
        }

        private static IEnumerable<T> Concat<T>(T element, IEnumerable<T> list) {
            yield return element;
            foreach (var item in list)
                yield return item;
        }

        private static string QuoteArguments(IEnumerable<string> args) {
            var quotedArgs = from arg in args select QuoteArgument(arg);
            return string.Join(" ", quotedArgs);
        }

        private static string QuoteArgument(string s) {
            if (s.Contains(" "))
                return "\"" + s + "\"";
            else
                return s;
        }
    }
}
