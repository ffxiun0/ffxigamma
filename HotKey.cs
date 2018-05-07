/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System.Collections.Generic;
using System.Windows.Forms;

namespace ffxigamma {
    public class HotKey {
        public Keys Key { get; set; }
        public bool Control { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }

        public HotKey(Keys key, bool control, bool shift, bool alt) {
            this.Key = key;
            this.Control = control;
            this.Shift = shift;
            this.Alt = alt;
        }

        public HotKey()
            : this(Keys.None, false, false, false) { }

        public HotKey(HotKeySettings key)
            : this((Keys)key.Key, key.Control, key.Shift, key.Alt) { }

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
