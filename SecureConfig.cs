/*
 * Copyright (c) 2018 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Xml;
using System.Xml.Serialization;

namespace ffxigamma {
    public class SecureConfig {
        public string Version { get; set; }
        public string StartProgramCommandLine { get; set; }

        public SecureConfig() {
            this.StartProgramCommandLine = "";
        }

        public static SecureConfig Default {
            get {
                return new SecureConfig { Version = App.Version };
            }
        }

        public static SecureConfig Load(string path) {
            if (!CheckPermission(path)) return Default;

            var doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(path);

            var xnr = new XmlNodeReader(doc.DocumentElement);
            var xs = new XmlSerializer(typeof(SecureConfig));
            var config = (SecureConfig)xs.Deserialize(xnr);

            return config;
        }

        public void Save(string path) {
            CreateEmptyFile(path);
            ChangePermission(path);

            using (var fs = File.OpenWrite(path)) {
                fs.SetLength(0);
                var xs = new XmlSerializer(typeof(SecureConfig));
                xs.Serialize(fs, this);
            }
        }

        private static void CreateEmptyFile(string path) {
            if (File.Exists(path)) return;

            using (var fs = File.OpenWrite(path)) {
                fs.SetLength(0);
            }
        }

        private static void ChangePermission(string path) {
            var adminUser = "Administrators";
            var adminRule = new FileSystemAccessRule(adminUser,
                FileSystemRights.FullControl, AccessControlType.Allow);

            var currentUser = WindowsIdentity.GetCurrent().Name;
            var userRule = new FileSystemAccessRule(currentUser,
                FileSystemRights.Read, AccessControlType.Allow);

            var sec = File.GetAccessControl(path);
            sec.SetAccessRuleProtection(true, false);
            sec.SetOwner(new NTAccount(adminUser));
            sec.AddAccessRule(adminRule);
            sec.AddAccessRule(userRule);

            File.SetAccessControl(path, sec);
        }

        private static bool CheckPermission(string path) {
            if (!File.Exists(path)) return false;

            var ac = File.GetAccessControl(path);
            var owner = ac.GetOwner(typeof(NTAccount));

            return owner.Value == @"BUILTIN\Administrators";
        }
    }
}
