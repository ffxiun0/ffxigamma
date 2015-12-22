using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ffxigamma {
    delegate void GlobalKeyEventHandler(object sender, GlobalKeyEventArgs e);

    class GlobalKeyReader : Component {
        private bool disposed;
        private Timer timer;
        private GlobalKeys prev;

        public event GlobalKeyEventHandler GlobalKeyDown;
        public event GlobalKeyEventHandler GlobalKeyUp;

        public GlobalKeyReader() {
            disposed = false;
            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            prev = GlobalKeys.Empty;
        }

        public GlobalKeyReader(IContainer container) : this() {
            container.Add(this);
        }

        protected override void Dispose(bool disposing) {
            if (disposed) return;
            if (disposing)
                timer.Dispose();
            disposed = true;
            base.Dispose(disposing);
        }

        [DefaultValue(30)]
        public int Interval {
            get {
                return timer.Interval;
            }
            set {
                timer.Interval = value;
            }
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

        [DefaultValue(false)]
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

        void Timer_Tick(object sender, EventArgs e) {
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
