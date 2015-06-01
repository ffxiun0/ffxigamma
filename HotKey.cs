using System.Collections.Generic;
using System.Windows.Forms;

namespace ffxigamma {
    public class HotKey {
        public Keys Key { get; set; }
        public bool Control { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }

        public HotKey() {
            this.Key = Keys.None;
            this.Control = false;
            this.Shift = false;
            this.Alt = false;
        }

        public HotKey(Keys key, bool control, bool shift, bool alt) {
            this.Key = key;
            this.Control = control;
            this.Shift = shift;
            this.Alt = alt;
        }

        public override string ToString() {
            var list = new List<string>();

            if (Control) list.Add("Ctrl");
            if (Shift) list.Add("Shift");
            if (Alt) list.Add("Alt");
            if (Key != Keys.None) list.Add(Key.ToString());

            return string.Join("+", list);
        }
    }
}
