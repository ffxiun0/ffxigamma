using System;

namespace ffxigamma {
    class ApplicationVolumeControl : VolumeControl {
        private IAudioSessionControl2 session;
        private ISimpleAudioVolume volume;

        public ApplicationVolumeControl(int pid) {
            session = GetSessionControl(pid);
            if (session == null)
                throw new VolumeControlNotFoundException("Application volume is not found.");

            volume = (ISimpleAudioVolume)session;
        }

        public override bool Active {
            get {
                AudioSessionState state;
                session.GetState(out state);
                return state == AudioSessionState.Active;
            }
        }

        public override bool Mute {
            get {
                bool result;
                volume.GetMute(out result);
                return result;
            }
            set {
                if (!Active) return;

                volume.SetMute(value, ref Guid);
            }
        }

        public override float Volume {
            get {
                float result;
                volume.GetMasterVolume(out result);
                return result;
            }
            set {
                if (!Active) return;

                if (value < MinVolume) value = MinVolume;
                if (value > MaxVolume) value = MaxVolume;
                volume.SetMasterVolume(value, ref Guid);
            }
        }

        private static IAudioSessionControl2 GetSessionControl(int pid) {
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

                AudioSessionState state;
                asc2.GetState(out state);
                if (state == AudioSessionState.Active) {
                    uint sessionPid;
                    asc2.GetProcessId(out sessionPid);
                    if (sessionPid == pid)
                        return asc2;
                }
            }

            return null;
        }
    }
}
