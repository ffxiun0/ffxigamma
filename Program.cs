using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ffxigamma {
    static class Program {
        [STAThread]
        static void Main() {
            if (IsRunning()) {
                MessageBox.Show("既に起動しています。", "FFXI Gamma",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new App());
            }
        }

        private static bool IsRunning() {
            string name = Process.GetCurrentProcess().ProcessName;
            var procs = Process.GetProcessesByName(name);
            return procs.Length > 1;
        }
    }
}
