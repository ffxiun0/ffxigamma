/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;

namespace ffxigamma {
    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var config = App.LoadConfig();

            if (HaveOption("/restart")) {
                if (!WaitForClose(5)) {
                    ShowError(Properties.Resources.RestartFail);
                    return;
                }
            }

            if (IsRunning()) {
                if (HaveOption("/ffxi") || config.StartFFXI) {
                    if (!RemoteStartProgram())
                        ShowError(Properties.Resources.RemoteControlFail);
                } else {
                    ShowWarning(Properties.Resources.AlreadyRunning);
                }
                return;
            }

            if (config.AdminMode && !IsAdminMode()) {
                if (FFXI.IsRunning()) {
                    if (!ShowYesNoWarning(Properties.Resources.AdminModeWarning))
                        return;
                }
                RestartAdminMode();
                return;
            }

            var app = new App();
            app.EnableAutoStartProgram = HaveOption("/ffxi") || config.StartFFXI;
            app.OpenSettings = HaveOption("/settings");

            Application.Run(app);
        }

        private static void ShowWarning(string s) {
            MessageBox.Show(s, App.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void ShowError(string s) {
            MessageBox.Show(s, App.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static bool ShowYesNoWarning(string s) {
            var ret = MessageBox.Show(null, s, App.AppName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            return ret == DialogResult.Yes;
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
            return ProcessEx.IsUserAnAdmin();
        }

        public static StartResult RestartAdminMode(params string[] args) {
            var path = Environment.GetCommandLineArgs()[0];
            var newArgs = new List<string>();
            newArgs.Add("/restart");
            newArgs.AddRange(args);
            return ProcessEx.StartAdmin(path, newArgs.ToArray());
        }

        public static StartResult RestartUserMode(params string[] args) {
            var path = Environment.GetCommandLineArgs()[0];
            var newArgs = new List<string>();
            newArgs.Add("/restart");
            newArgs.AddRange(args);
            return ProcessEx.StartUser(path, newArgs.ToArray());
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

        private static bool RemoteStartProgram() {
            try {
                var remote = App.GetRemoteControl();
                remote.StartProgram();
                return true;
            }
            catch (RemotingException) {
                return false;
            }
        }
    }
}
