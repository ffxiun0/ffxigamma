using System;

namespace ffxigamma {
    class GlobalKeyEventArgs : EventArgs {
        public GlobalKeys State { get; set; }
        public GlobalKeys Trigger { get; set; }

        public GlobalKeyEventArgs(GlobalKeys state, GlobalKeys trigger) {
            State = state;
            Trigger = trigger;
        }
    }
}
