using System;
using System.Windows.Forms;

namespace ffxigamma {
    class GlobalKeyReader : Control {
        private bool disposed;
        private Timer timer;
        private GlobalKeys prev;

        public delegate void GlobalKeyEventHandler(object sender, GlobalKeyEventArgs e);
        public event GlobalKeyEventHandler GlobalKeyDown;
        public event GlobalKeyEventHandler GlobalKeyUp;

        public GlobalKeyReader() {
            this.disposed = false;
            this.timer = new Timer();
            this.timer.Interval = 30;
            this.timer.Tick += timer_Tick;
            this.prev = GlobalKeys.Empty;
        }

        protected override void Dispose(bool disposing) {
            if (disposed) return;
            if (disposing)
                timer.Stop();
            disposed = true;
        }

        public void Start() {
            prev = GlobalKeys.Now;
            timer.Start();
        }

        public void Stop() {
            timer.Stop();
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
