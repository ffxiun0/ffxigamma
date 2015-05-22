using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ffxigamma {
    public class Config {
        public string Version { get; set; }
        public double AppGamma { get; set; }
        public double SystemGamma { get; set; }
        public List<string> NameList { get; set; }

        public static Config Default {
            get {
                var config = new Config();
                config.Version = App.Version;
                config.AppGamma = 1.8 / 2.2;
                config.SystemGamma = 1.0;
                config.NameList = new List<string>();
                config.NameList.Add("FINAL FANTASY XI");
                return config;
            }
        }

        public static Config Load(string path) {
            using (var fs = File.OpenRead(path)) {
                var xs = new XmlSerializer(typeof(Config));
                return (Config)xs.Deserialize(fs);
            }
        }

        public void Save(string path) {
            using (var fs = File.OpenWrite(path)) {
                fs.SetLength(0);
                var xs = new XmlSerializer(typeof(Config));
                xs.Serialize(fs, this);
            }
        }
    }
}
