/*
 * Copyright (c) 2018 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Windows.Forms;

namespace ffxigamma {
    class Popup {
        public static void Warning(string s) {
            MessageBox.Show(s, App.AppName, MessageBoxButtons.OK,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        public static void Error(string s) {
            MessageBox.Show(s, App.AppName, MessageBoxButtons.OK,
                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        public static void Exception(Exception ex, string s) {
            var cname = ex.GetType().FullName;
            var msg = string.Format("{0}\n\n{1}:\n{2}", s, cname, ex.Message);
            Error(msg);
        }

        public static bool YesNoWarning(string s) {
            var ret = MessageBox.Show(s, App.AppName, MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.DefaultDesktopOnly);
            return ret == DialogResult.Yes;
        }

        public static void Warning(IWin32Window owner, string s) {
            MessageBox.Show(owner, s, App.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(IWin32Window owner, string s) {
            MessageBox.Show(owner, s, App.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool YesNoWarning(IWin32Window owner, string s) {
            var ret = MessageBox.Show(owner, s, App.AppName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            return ret == DialogResult.Yes;
        }
    }
}
