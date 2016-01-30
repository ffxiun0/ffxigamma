using System;

namespace ffxigamma {
    class ApplicationVolumeControl : VolumeControl {
        private ISimpleAudioVolume control;

        public ApplicationVolumeControl(int pid) {
            control = GetAppVolumeControl(pid);
            if (control == null)
                throw new VolumeControlNotFoundException("Application volume is not found.");
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
                control.GetMasterVolume(out result);
                return result;
            }
            set {
                if (value < MinVolume) value = MinVolume;
                if (value > MaxVolume) value = MaxVolume;
                control.SetMasterVolume(value, ref Guid);
            }
        }

        private static ISimpleAudioVolume GetAppVolumeControl(int pid) {
            var mmde = (IMMDeviceEnumerator)new MMDeviceEnumerator();

            IMMDevice mmd;
            mmde.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out mmd);

            IAudioSessionManager2 asm2;
            try {
                object o;
                var iid = typeof(IAudioSessionManager2).GUID;
                mmd.Activate(ref iid, CLSCTX.INPROC_SERVER, IntPtr.Zero, out o);
                asm2 = (IAudioSessionManager2)o;
            }
            catch (InvalidCastException e) {
                throw new VolumeControlNotSupportedException(
                    "Application volume is not supported.", e);
            }

            IAudioSessionEnumerator ase;
            asm2.GetSessionEnumerator(out ase);

            int count;
            ase.GetCount(out count);
            for (int i = 0; i < count; i++) {
                IAudioSessionControl asc;
                ase.GetSession(i, out asc);
                var asc2 = (IAudioSessionControl2)asc;

                uint sessionPid;
                asc2.GetProcessId(out sessionPid);
                if (sessionPid == pid)
                    return (ISimpleAudioVolume)asc2;
            }

            return null;
        }
    }
}
