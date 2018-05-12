/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace ffxigamma {
    public class Config {
        public string Version { get; set; }
        public double AppGamma { get; set; }
        public double SystemGamma { get; set; }
        public bool AdminMode { get; set; }
        public bool StartFFXI { get; set; }
        public string StartProgramType { get; set; }
        public bool EnableSaveWindowPosition { get; set; }
        public bool EnableImageGamma { get; set; }
        public string ImageFolder { get; set; }
        public string ImageFormatName { get; set; }
        public bool EnableHotKeyCapture { get; set; }
        public bool EnableHotKeyVolumeControl { get; set; }
        public bool EnableImageText { get; set; }
        public ImageText[] ImageTextList { get; set; }
        public bool EnableNotify { get; set; }

        public Config() {
            this.AppGamma = 1.8 / 2.2;
            this.SystemGamma = 1.0;
            this.AdminMode = false;
            this.StartFFXI = false;
            this.StartProgramType = "ffxi";
            this.EnableSaveWindowPosition = false;
            this.WindowSettingsList = new WindowSettings[] {
                new WindowSettings() { Name = "FINAL FANTASY XI" },
            };
            this.EnableImageGamma = true;
            this.ImageFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.ImageFormatName = "jpg";
            this.EnableHotKeyCapture = false;
            this.EnableHotKeyVolumeControl = false;
            this.EnableImageText = true;
            this.ImageTextList = new ImageText[] {
                new ImageText() {
                    Text = "Copyright © 2002-@yyyy@ SQUARE ENIX CO., LTD. All Rights Reserved.",
                    Font = new Font("Meiryo", 9, FontStyle.Bold | FontStyle.Italic),
                    ForeColor = Color.White,
                    BackColor = Color.Black,
                    Edge = true,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    MarginX = 0,
                    MarginY = 0,
                }
            };
            this.EnableNotify = true;
            this.HotKeySettingsList = new HotKeySettings[] {
                new HotKeySettings("Capture", Keys.F11),
                new HotKeySettings("Mute", Keys.F12),
                new HotKeySettings("VolumeUp", Keys.F12, false, true, false),
                new HotKeySettings("VolumeDown", Keys.F11,false, true, false),
            };
        }

        public static Config Default {
            get {
                return new Config { Version = App.Version };
            }
        }

        [XmlIgnore]
        public ImageFormat ImageFormat {
            get {
                if (ImageFormatName == "png")
                    return ImageFormat.Png;
                else if (ImageFormatName == "jpg")
                    return ImageFormat.Jpeg;
                else
                    return ImageFormat.Png;
            }
            set {
                if (ImageFormat.Png.Equals(value))
                    ImageFormatName = "png";
                else if (ImageFormat.Jpeg.Equals(value))
                    ImageFormatName = "jpg";
                else
                    ImageFormatName = "png";
            }
        }

        private Dictionary<string, WindowSettings> windowSettingsMap =
            new Dictionary<string, WindowSettings>();

        public WindowSettings[] WindowSettingsList {
            get {
                return windowSettingsMap.Values.ToArray();
            }
            set {
                windowSettingsMap = value.ToDictionary(ws => ws.Name);
            }
        }

        public WindowSettings GetWindowSettings(string name) {
            if (windowSettingsMap.ContainsKey(name))
                return windowSettingsMap[name];
            else
                return null;
        }

        private Dictionary<string, HotKeySettings> hotKeySettingsMap =
            new Dictionary<string, HotKeySettings>();

        public HotKeySettings[] HotKeySettingsList {
            get {
                return hotKeySettingsMap.Values.ToArray();
            }
            set {
                hotKeySettingsMap = value.ToDictionary(key => key.Name);
            }
        }

        public HotKey GetHotKey(string name) {
            if (hotKeySettingsMap.ContainsKey(name))
                return new HotKey(hotKeySettingsMap[name]);
            else
                return new HotKey(Keys.None, false, false, false);

        }

        public void SetHotKey(string name, HotKey hotkey) {
            hotKeySettingsMap[name] = new HotKeySettings(name, hotkey);
        }

        public void RemoveHotKey(string name) {
            hotKeySettingsMap.Remove(name);
        }

        public static Config Load(string path) {
            var doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(path);

            var xnr = new XmlNodeReader(doc.DocumentElement);
            var xs = new XmlSerializer(typeof(Config));
            var config = (Config)xs.Deserialize(xnr);

            return config;
        }

        public void Save(string path) {
            using (var fs = File.OpenWrite(path)) {
                fs.SetLength(0);
                var xs = new XmlSerializer(typeof(Config));
                xs.Serialize(fs, this);
            }
        }
    }

    public class HotKeySettings {
        public string Name { get; set; }
        public int Key { get; set; }
        public bool Control { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }

        public HotKeySettings(string name, Keys key, bool control, bool shift, bool alt) {
            Name = name;
            Key = (int)key;
            Control = control;
            Shift = shift;
            Alt = alt;
        }

        public HotKeySettings()
            : this("", Keys.None, false, false, false) { }

        public HotKeySettings(string name, Keys key)
            : this(name, key, false, false, false) { }

        public HotKeySettings(string name, HotKey key)
            : this(name, key.Key, key.Control, key.Shift, key.Alt) { }
    }

    public class ImageText {
        public string Text { get; set; }
        public string FontName { get; set; }
        public int ForeColorValue { get; set; }
        public int BackColorValue { get; set; }
        public bool Edge { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public float MarginX { get; set; }
        public float MarginY { get; set; }

        public ImageText() {
            this.Text = "";
            this.Font = SystemFonts.DefaultFont;
            this.ForeColor = Color.White;
            this.BackColor = Color.Black;
            this.Edge = true;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.MarginX = 0;
            this.MarginY = 0;
        }

        [XmlIgnore]
        public Font Font {
            get {
                var fc = new FontConverter();
                return (Font)fc.ConvertFromString(FontName);
            }
            set {
                var fc = new FontConverter();
                FontName = fc.ConvertToString(value);
            }
        }

        [XmlIgnore]
        public Color ForeColor {
            get {
                return Color.FromArgb(ForeColorValue);
            }
            set {
                ForeColorValue = value.ToArgb();
            }
        }

        [XmlIgnore]
        public Color BackColor {
            get {
                return Color.FromArgb(BackColorValue);
            }
            set {
                BackColorValue = value.ToArgb();
            }
        }

        public override string ToString() {
            return Text;
        }
    }

    public class WindowSettings {
        public WindowSettings() {
            Name = "";
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            ChangeSize = false;
            AlwaysGamma = false;
        }

        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool ChangeSize { get; set; }
        public bool AlwaysGamma { get; set; }

        public override string ToString() {
            return string.Format("{0} - ({1},{2})", Name, X, Y);
        }
    }
}
