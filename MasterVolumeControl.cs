using System;

namespace ffxigamma {
    class MasterVolumeControl : VolumeControl {
        private IAudioEndpointVolume control;

        public MasterVolumeControl() {
            control = GetMasterVolumeControl();
            MinVolume = 0.0001f;
        }

        public override bool Mute {
            get {
                bool result;
                control.GetMute(out result);
                return result;
            }
            set {
                control.SetMute(value, ref Guid);
            }
        }

        public override float Volume {
            get {
                float result;
                control.GetMasterVolumeLevelScalar(out result);
                return result;
            }
            set {
                if (value < MinVolume) value = MinVolume;
                if (value > MaxVolume) value = MaxVolume;
                control.SetMasterVolumeLevelScalar(value, ref Guid);
            }
        }

        private static IAudioEndpointVolume GetMasterVolumeControl() {
            var mmde = (IMMDeviceEnumerator)new MMDeviceEnumerator();

            IMMDevice mmd;
            mmde.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out mmd);

            object o;
            var iid = typeof(IAudioEndpointVolume).GUID;
            mmd.Activate(ref iid, CLSCTX.INPROC_SERVER, IntPtr.Zero, out o);

            return (IAudioEndpointVolume)o;
        }
    }
}
