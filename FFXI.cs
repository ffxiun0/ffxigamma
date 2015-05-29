using Microsoft.Win32;
using System.Diagnostics;

namespace ffxigamma {
    class FFXI {
        private const string KeyName = @"SOFTWARE\PlayOnline\Square\FinalFantasyXI";

        private static object GetRegistryValue(string name, object defaultValue) {
            var subKey = Registry.LocalMachine.OpenSubKey(KeyName);
            if (subKey == null) return defaultValue;

            return subKey.GetValue(name, defaultValue);
        }

        public static bool IsFullScreenMode() {
            var value = (int)GetRegistryValue("0034", -1);
            return value == 0;
        }

        public static bool IsRunning() {
            var procs = Process.GetProcessesByName("pol");
            return procs.Length > 0;
        }
    }
}
