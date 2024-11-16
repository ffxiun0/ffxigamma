/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
namespace ffxigamma {
    class NullVolumeControl : VolumeControl {
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
}
