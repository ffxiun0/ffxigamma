/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ffxigamma {
    class SpeakerLevel : UserControl {
        private float value = 0.0f;

        public SpeakerLevel() {
            DoubleBuffered = true;
        }

        [DefaultValue(0.0f)]
        public float Value {
            get {
                return value;
            }
            set {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                this.value = value;
                Refresh();
            }
        }

        [DefaultValue(typeof(Color), "Lime")]
        public Color EnabledColor { get; set; } = Color.Lime;
        [DefaultValue(typeof(Color), "Green")]
        public Color EnabledEdgeColor { get; set; } = Color.Green;

        [DefaultValue(typeof(Color), "Gray")]
        public Color DisabledColor { get; set; } = Color.Gray;
        [DefaultValue(typeof(Color), "#606060")]
        public Color DisabledEdgeColor { get; set; } = Color.FromArgb(0x60, 0x60, 0x60);

        protected override void OnPaint(PaintEventArgs e) {
            var brush = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(brush, 0, 0, Width, Height);

            var color = Enabled ? EnabledColor : DisabledColor;
            var edgeColor = Enabled ? EnabledEdgeColor : DisabledEdgeColor;

            foreach (var o in offsets)
                DrawBar(e.Graphics, o.X, o.Y, edgeColor);

            DrawBar(e.Graphics, 0, 0, color);
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

        private void DrawBar(Graphics g, float x, float y, Color color) {
            float mx = 1;
            float my = 1;
            x += mx;
            y += my;
            float w = Width - mx * 2 - 1;
            float h = Height - my * 2 - 1;

            var pen = new Pen(color);
            g.DrawRectangle(pen, x, y, w - 2, h - 2);

            var brush = new SolidBrush(color);
            int w2 = (int)((w - 3) * Value);
            var innter = new RectangleF(x + 1, y + 1, w2, h - 3);
            g.FillRectangle(brush, innter);
        }
    }
}
