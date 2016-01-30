using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ffxigamma {
    delegate void GlobalKeyEventHandler(object sender, GlobalKeyEventArgs e);

    class GlobalKeyReader : Component {
        private bool disposed;
        private Timer timer;
        private Timer repeatTimer;
        private GlobalKeys now;
        private GlobalKeys down;
        private GlobalKeys up;
        private GlobalKeys prev;

        public event GlobalKeyEventHandler GlobalKeyDown;
        public event GlobalKeyEventHandler GlobalKeyUp;

        private const int DefaultInterval = 30;
        private const int DefaultRepeatDelay = 250;
        private const int DefaultRepeatInterval = 20;

        public GlobalKeyReader() {
            disposed = false;

            timer = new Timer();
            timer.Interval = DefaultInterval;
            timer.Tick += Timer_Tick;

            repeatTimer = new Timer();
            repeatTimer.Interval = DefaultRepeatDelay;
            repeatTimer.Tick += RepeatTimer_Tick;

            now = GlobalKeys.Empty;
            down = GlobalKeys.Empty;
            up = GlobalKeys.Empty;
            prev = GlobalKeys.Empty;
        }

        public GlobalKeyReader(IContainer container) : this() {
            container.Add(this);
        }

        protected override void Dispose(bool disposing) {
            if (disposed) return;
            if (disposing) {
                timer.Dispose();
                repeatTimer.Dispose();
            }
            disposed = true;
            base.Dispose(disposing);
        }

        [DefaultValue(DefaultInterval)]
        public int Interval {
            get {
                return timer.Interval;
            }
            set {
                timer.Interval = value;
            }
        }

        [DefaultValue(DefaultRepeatDelay)]
        public int RepeatDelay { get; set; } = DefaultRepeatDelay;

        [DefaultValue(DefaultRepeatInterval)]
        public int RepeatInterval { get; set; } = DefaultRepeatInterval;

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
            now = GlobalKeys.Now;
            down = GlobalKeys.Diff(prev, now);
            up = GlobalKeys.Diff(now, prev);
            prev = now;

            if (up.Count > 0 || now.Count == 0)
                repeatTimer.Stop();

            if (GlobalKeyDown != null && down.Count > 0) {
                var args = new GlobalKeyEventArgs(now, down);
                GlobalKeyDown(this, args);
                if (args.Repeat) {
                    repeatTimer.Interval = RepeatDelay;
                    repeatTimer.Start();
                }
            }

            if (GlobalKeyUp != null && up.Count > 0)
                GlobalKeyUp(this, new GlobalKeyEventArgs(now, up));
        }

        private void RepeatTimer_Tick(object sender, EventArgs e) {
            if (GlobalKeyDown != null)
                GlobalKeyDown(this, new GlobalKeyEventArgs(now, down));

            repeatTimer.Interval = RepeatInterval;
        }
    }
}
