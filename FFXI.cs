using Microsoft.Win32;

namespace ffxigamma {
    class FFXI {
        private const string KeyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\PlayOnline\Square\FinalFantasyXI";

        public static bool IsWindowMode() {
            var value = (int)Registry.GetValue(KeyName, "0034", 0);
            return value == 1;
        }
    }
}
