/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;

namespace ffxigamma {
    class MasterVolumeControl : VolumeControl {
        private IAudioEndpointVolume volume;

        public MasterVolumeControl() {
            volume = GetMasterVolumeControl();
            MinVolume = 0.0001f;
        }

        public override bool Active {
            get {
                return true;
            }
        }

        public override bool Mute {
            get {
                bool result;
                volume.GetMute(out result);
                return result;
            }
            set {
                volume.SetMute(value, ref Guid);
            }
        }

        public override float Volume {
            get {
                float result;
                volume.GetMasterVolumeLevelScalar(out result);
                return result;
            }
            set {
                if (value < MinVolume) value = MinVolume;
                if (value > MaxVolume) value = MaxVolume;
                volume.SetMasterVolumeLevelScalar(value, ref Guid);
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
