/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ffxigamma {
    class SpeakerIcon : UserControl {
        private bool mute = false;

        public SpeakerIcon() {
            DoubleBuffered = true;
        }

        [DefaultValue(typeof(Color), "Lime")]
        public Color EnabledColor { get; set; } = Color.Lime;
        [DefaultValue(typeof(Color), "Green")]
        public Color EnabledEdgeColor { get; set; } = Color.Green;

        [DefaultValue(typeof(Color), "Gray")]
        public Color DisabledColor { get; set; } = Color.Gray;
        [DefaultValue(typeof(Color), "#606060")]
        public Color DisabledEdgeColor { get; set; } = Color.FromArgb(0x60, 0x60, 0x60);

        [DefaultValue(typeof(Color), "Red")]
        public Color MuteColor { get; set; } = Color.Red;
        [DefaultValue(typeof(Color), "#FFE0E0")]
        public Color MuteEdgeColor { get; set; } = Color.FromArgb(0xff, 0xe0, 0xe0);

        [DefaultValue(false)]
        public bool Mute {
            get {
                return mute;
            }
            set {
                mute = value;
                Refresh();
            }
        }

        private Color GetColor() {
            if (Enabled)
                return Mute ? MuteColor : EnabledColor;
            else
                return DisabledColor;
        }

        private Color GetEdgeColor() {
            if (Enabled)
                return Mute ? MuteEdgeColor : EnabledEdgeColor;
            else
                return DisabledEdgeColor;
        }

        protected override void OnPaint(PaintEventArgs e) {
            var brush = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(brush, 0, 0, Width, Height);

            var color = GetColor();
            var edgeColor = GetEdgeColor();

            foreach (var o in offsets)
                DrawSpeaker(e.Graphics, o.X, o.Y, edgeColor);
            DrawSpeaker(e.Graphics, 0, 0, color);

            if (Mute) {
                foreach (var o in offsets)
                    DrawMute(e.Graphics, o.X, o.Y, edgeColor);
                DrawMute(e.Graphics, 0, 0, color);
            }
        }

        private void DrawSpeaker(Graphics g, float x, float y, Color color) {
            var brush = new SolidBrush(color);
            var points = Translate(speakerPoints, x, y);
            g.FillPolygon(new SolidBrush(color), points);
        }

        private void DrawMute(Graphics g, float x, float y, Color color) {
            var pen = new Pen(color);
            var points = Translate(mutePoints, x, y);
            g.DrawLines(pen, points);
        }

        private static PointF[] offsets = {
            new PointF(-1, -1),
            new PointF(0, -1),
            new PointF(1, -1),
            new PointF(1, 0),
            new PointF(1, 1),
            new PointF(0, 1),
            new PointF(-1, 1),
            new PointF(-1, 0),
        };

        private static PointF[] speakerPoints = {
            new PointF(1, 1),
            new PointF(2, 1),
            new PointF(3, 0),
            new PointF(3, 4),
            new PointF(2, 3),
            new PointF(1, 3),
            new PointF(1, 1),
        };

        private static PointF[] mutePoints = {
            new PointF(0, 0),
            new PointF(4, 4),
        };

        private PointF[] Translate(PointF[] points, float x, float y) {
            float mx = 1;
            float my = 1;
            x += mx;
            y += my;
            float w = Width - mx * 2 - 1;
            float h = Height - my * 2 - 1;
            float sx = w / 4;
            float sy = h / 4;

            var scaled = from p in points
                         select new PointF(x + p.X * sx, y + p.Y * sy);
            return scaled.ToArray();
        }
    }
}
