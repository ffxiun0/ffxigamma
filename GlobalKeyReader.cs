using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ffxigamma {
    class GlobalKeyReader : Component {
        private Timer timer;
        private GlobalKeys prev;

        public delegate void GlobalKeyEventHandler(object sender, GlobalKeyEventArgs e);
        public event GlobalKeyEventHandler GlobalKeyDown;
        public event GlobalKeyEventHandler GlobalKeyUp;

        public GlobalKeyReader() {
            this.timer = new Timer();
            this.timer.Interval = 30;
            this.timer.Tick += timer_Tick;
            this.prev = GlobalKeys.Empty;
        }

        public void Start() {
            if (timer.Enabled) return;

            prev = GlobalKeys.Now;
            timer.Start();
        }

        public void Stop() {
            if (!timer.Enabled) return;

            timer.Stop();
        }

        public bool Enabled {
            get {
                return timer.Enabled;
            }
            set {
                if (value)
                    Start();
                else
                    Stop();
            }
        }

        void timer_Tick(object sender, EventArgs e) {
            var now = GlobalKeys.Now;
            var down = GlobalKeys.Diff(prev, now);
            var up = GlobalKeys.Diff(now, prev);
            prev = now;

            if (GlobalKeyDown != null && down.Count > 0)
                GlobalKeyDown(this, new GlobalKeyEventArgs(now, down));

            if (GlobalKeyUp != null && up.Count > 0)
                GlobalKeyUp(this, new GlobalKeyEventArgs(now, up));
        }
    }
}
