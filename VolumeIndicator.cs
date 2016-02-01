using System;
using System.Drawing;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class VolumeIndicator : Form {
        private Window window = null;
        private VolumeControl volumeControl = new NullVolumeControl();

        public VolumeIndicator() {
            InitializeComponent();

            Visible = false;
            SetInactiveTopMost();
        }

        private class NullVolumeControl : VolumeControl {
            public override bool Active {
                get { return false; }
            }
            public override bool Mute {
                get { return false; }
                set { }
            }
            public override float Volume {
                get { return 0; }
                set { }
            }
        }

        public VolumeControl VolumeControl {
            get {
                return volumeControl;
            }
            set {
                volumeControl = value;
                UpdateStates();
            }
        }

        public Window Window {
            get {
                return window;
            }
            set {
                window = value;
                VolumeControl = GetVolumeControl(window);
            }
        }

        public bool Mute {
            get {
                return VolumeControl.Mute;
            }
            set {
                volumeControl.Mute = value;
                UpdateStates();
                ShowTimeout();
            }
        }

        public float Volume {
            get {
                return volumeControl.Volume;
            }
            set {
                volumeControl.Volume = value;
                UpdateStates();
                ShowTimeout();

                if (Mute)
                    Mute = false;
            }
        }

        public int Timeout {
            get {
                return timer.Interval;
            }
            set {
                timer.Interval = value;
            }
        }

        private void UpdateStates() {
            speakerIcon.Mute = volumeControl.Mute;
            speakerIcon.Enabled = volumeControl.Active;
            speakerLevel.Value = volumeControl.Volume;
            speakerLevel.Enabled = volumeControl.Active && !volumeControl.Mute;
        }

        public void ShowTimeout() {
            if (window == null) return;

            Location = GetCenterLocation(window);
            timer.Stop();
            timer.Start();
            Visible = true;
        }

        private VolumeControl GetVolumeControl(Window window) {
            if (window == null)
                return new NullVolumeControl();

            var pid = window.GetProcessId();
            var vc = VolumeControl.FromProcessId(pid);
            if (vc == null)
                return new NullVolumeControl();

            return vc;
        }

        private Point GetCenterLocation(Window window) {
            var rect = window.GetWindowRect();
            int x = rect.Left + (rect.Width - Width) / 2;
            int y = rect.Top + (rect.Height - Height) / 2;

            return new Point(x, y);
        }

        protected override bool ShowWithoutActivation {
            get {
                return true;
            }
        }

        private void SetInactiveTopMost() {
            NativeMethods.SetWindowPos(Handle,
                NativeMethods.HWND_TOPMOST,
                0, 0, 0, 0,
                NativeMethods.SWP_NOSIZE |
                NativeMethods.SWP_NOMOVE |
                NativeMethods.SWP_NOACTIVATE |
                NativeMethods.SWP_SHOWWINDOW |
                NativeMethods.SWP_NOSENDCHANGING);
        }

        private void timer_Tick(object sender, EventArgs e) {
            timer.Stop();
            Visible = false;
        }
    }
}
