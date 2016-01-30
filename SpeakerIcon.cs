using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ffxigamma {
    class SpeakerIcon : UserControl {
        public SpeakerIcon() {
            DoubleBuffered = true;
        }

        [DefaultValue(typeof(Color), "Lime")]
        public Color EnabledColor { get; set; } = Color.Lime;
        [DefaultValue(typeof(Color), "Green")]
        public Color EnabledEdgeColor { get; set; } = Color.Green;
        [DefaultValue(typeof(Color), "Red")]
        public Color DisabledColor { get; set; } = Color.Red;
        [DefaultValue(typeof(Color), "#FFE0E0")]
        public Color DisabledEdgeColor { get; set; } = Color.FromArgb(0xff, 0xe0, 0xe0);

        protected override void OnPaint(PaintEventArgs e) {
            var brush = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(brush, 0, 0, Width, Height);

            var color = Enabled ? EnabledColor : DisabledColor;
            var edgeColor = Enabled ? EnabledEdgeColor : DisabledEdgeColor;

            foreach (var o in offsets)
                DrawSpeaker(e.Graphics, o.X, o.Y, edgeColor);
            DrawSpeaker(e.Graphics, 0, 0, color);

            if (!Enabled) {
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

        static PointF[] offsets = {
            new PointF(-1, -1),
            new PointF(0, -1),
            new PointF(1, -1),
            new PointF(1, 0),
            new PointF(1, 1),
            new PointF(0, 1),
            new PointF(-1, 1),
            new PointF(-1, 0),
        };

        static PointF[] speakerPoints = {
            new PointF(1, 1),
            new PointF(2, 1),
            new PointF(3, 0),
            new PointF(3, 4),
            new PointF(2, 3),
            new PointF(1, 3),
            new PointF(1, 1),
        };

        static PointF[] mutePoints = {
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
