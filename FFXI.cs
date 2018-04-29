using Microsoft.Win32;
using System.Diagnostics;

namespace ffxigamma {
    class FFXI {
        private const string KeyFFXI = @"SOFTWARE\PlayOnline\Square\FinalFantasyXI";
        private const string KeyInst = @"SOFTWARE\PlayOnline\InstallFolder";

        private static object GetRegistryValue(string key, string name, object defaultValue) {
            var subKey = Registry.LocalMachine.OpenSubKey(key);
            if (subKey == null) return defaultValue;

            return subKey.GetValue(name, defaultValue);
        }

        public static bool IsFullScreenMode() {
            var value = (int)GetRegistryValue(KeyFFXI, "0034", -1);
            return value == 0;
        }

        public static bool IsRunning() {
            var procs = Process.GetProcessesByName("pol");
            return procs.Length > 0;
        }

        private static string GetProgramPath() {
            var path = (string)GetRegistryValue(KeyInst, "0001", null);
            if (path == null) return null;

            return path + @"\" + "polboot.exe";
        }

        public static StartResult Start() {
            if (FFXI.IsRunning()) return StartResult.Success;

            var path = FFXI.GetProgramPath();
            if (path == null) return StartResult.Failure;

            return ProcessEx.Start(path);
        }
    }
}
