/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;

namespace ffxigamma {
    public abstract class VolumeControl {
        public abstract bool Active { get; }
        public abstract bool Mute { get; set; }
        public abstract float Volume { get; set; }

        public float MinVolume { get; set; } = 0.0f;
        public float MaxVolume { get; set; } = 1.0f;

        public static Guid Guid = new Guid("6D4D7741-2D2C-48B4-BDDA-5822F22EA00B");

        private static bool AppVolumeSupported = true;

        public static VolumeControl FromProcessId(int pid) {
            if (AppVolumeSupported) {
                try {
                    return new ApplicationVolumeControl(pid);
                }
                catch (VolumeControlNotFoundException) {
                    return null;
                }
                catch (VolumeControlNotSupportedException) {
                    AppVolumeSupported = false;
                }
            }
            return new MasterVolumeControl();
        }
    }
}
