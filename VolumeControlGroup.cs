/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System.Collections.Generic;
using System.Linq;

namespace ffxigamma {
    class VolumeControlGroup : VolumeControl {
        private VolumeControl[] volumes;

        public VolumeControlGroup(IEnumerable<VolumeControl> volumes) {
            this.volumes = volumes.ToArray();
        }

        public override bool Active {
            get {
                return volumes.Any(v => v.Active);
            }
        }

        public override bool Mute {
            get {
                return volumes.All(v => v.Mute);
            }
            set {
                foreach (var volume in volumes)
                    volume.Mute = value;
            }
        }

        public override float Volume {
            get {
                var vc = volumes.FirstOrDefault(v => v.Active);
                if (vc == null)
                    return 0;
                return vc.Volume;
            }
            set {
                foreach (var volume in volumes)
                    volume.Volume = value;
            }
        }
    }
}
