/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;

namespace ffxigamma {
    class GlobalKeyEventArgs : EventArgs {
        public GlobalKeys State { get; set; }
        public GlobalKeys Trigger { get; set; }
        public bool Repeat { get; set; }

        public GlobalKeyEventArgs(GlobalKeys state, GlobalKeys trigger) {
            State = state;
            Trigger = trigger;
            Repeat = false;
        }
    }
}
