using System.Windows.Forms;

namespace ffxigamma {
    class GlobalKeys {
        private bool[] keys;
        private int count;

        public GlobalKeys(bool[] keys, int count) {
            this.keys = keys;
            this.count = count;
        }

        public GlobalKeys(bool[] keys) {
            this.keys = keys;
            this.count = CountKeys(keys);
        }

        private static int CountKeys(bool[] keys) {
            int count = 0;
            for (int i = 0; i < keys.Length; i++) {
                if (keys[i])
                    count++;
            }
            return count;
        }

        public bool Contains(Keys key) {
            return keys[(int)key];
        }

        public int Count {
            get {
                return count;
            }
        }

        public static GlobalKeys Empty {
            get {
                return new GlobalKeys(new bool[256], 0);
            }
        }

        public static GlobalKeys Now {
            get {
                var keys = new bool[256];
                var count = 0;
                for (int i = 0; i < 256; i++) {
                    if ((WinAPI.GetAsyncKeyState(i) & 0x8000) != 0) {
                        keys[i] = true;
                        count++;
                    }
                }
                return new GlobalKeys(keys, count);
            }
        }

        public static GlobalKeys Diff(GlobalKeys a, GlobalKeys b) {
            var keys = new bool[256];
            int count = 0;
            for (int i = 0; i < 256; i++) {
                if (!a.keys[i] && b.keys[i]) {
                    keys[i] = true;
                    count++;
                }
            }
            return new GlobalKeys(keys, count);
        }
    }
}
