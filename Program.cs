using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace ffxigamma {
    static class Program {
        [STAThread]
        static void Main() {
            var config = App.LoadConfig();

            if (HaveOption("/restart")) {
                if (!WaitForClose(5)) {
                    ShowError(Properties.Resources.RestartFail);
                    return;
                }
            }

            if (IsRunning()) {
                ShowWarning(Properties.Resources.AlreadyRunning);
                return;
            }

            if (config.AdminMode && !IsAdminMode()) {
                RestartAdminMode();
                return;
            }

            if (HaveOption("/ffxi") || config.StartFFXI) {
                if (!FFXI.Start())
                    ShowError(Properties.Resources.FFXIStartFail);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }

        private static void ShowWarning(string s) {
            MessageBox.Show(s, App.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void ShowError(string s) {
            MessageBox.Show(s, App.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static bool HaveOption(string option) {
            option = option.ToLower();

            var args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i++) {
                if (args[i].ToLower() == option)
                    return true;
            }

            return false;
        }

        private static bool IsRunning() {
            string name = Process.GetCurrentProcess().ProcessName;
            var procs = Process.GetProcessesByName(name);
            return procs.Length > 1;
        }

        public static bool IsAdminMode() {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool RestartAdminMode(params string[] args) {
            try {
                var path = Environment.GetCommandLineArgs()[0];
                var psi = new ProcessStartInfo(path);
                psi.Arguments = "/restart " + string.Join(" ", args);
                psi.Verb = "runas";
                Process.Start(psi);
                return true;
            }
            catch (Win32Exception) {
                return false;
            }
        }

        private static bool WaitForClose(int seconds) {
            var begin = DateTime.Now;
            while (IsRunning()) {
                var span = DateTime.Now - begin;
                if (span.TotalSeconds > seconds)
                    return false;
                Thread.Sleep(100);
            }
            return true;
        }
    }
}
