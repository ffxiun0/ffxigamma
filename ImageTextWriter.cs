/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System.Drawing;

namespace ffxigamma {
    public enum HorizontalAlignment { Left, Center, Right };
    public enum VerticalAlignment { Top, Center, Bottom };

    class ImageTextWriter {
        public string Text { get; set; }
        public Font Font { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public float MarginX { get; set; }
        public float MarginY { get; set; }
        public bool Edge { get; set; }

        public ImageTextWriter() {
            this.Text = "";
            this.Font = SystemFonts.DefaultFont;
            this.ForeColor = SystemColors.ControlText;
            this.BackColor = SystemColors.ControlText;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.MarginX = 0;
            this.MarginY = 0;
            this.Edge = false;
        }

        public ImageTextWriter(ImageText config) {
            this.Text = config.Text;
            this.Font = config.Font;
            this.ForeColor = config.ForeColor;
            this.BackColor = config.BackColor;
            this.HorizontalAlignment = config.HorizontalAlignment;
            this.VerticalAlignment = config.VerticalAlignment;
            this.MarginX = config.MarginX;
            this.MarginY = config.MarginY;
            this.Edge = config.Edge;
        }

        public void WriteTo(Image image) {
            using (var g = Graphics.FromImage(image)) {
                var textSize = g.MeasureString(Text, Font);
                var pos = GetTextPosition(image.Size, textSize);

                if (Edge) {
                    var backBrush = new SolidBrush(BackColor);
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            g.DrawString(Text, Font, backBrush, pos.X + i, pos.Y + j);
                }
                var foreBrush = new SolidBrush(ForeColor);
                g.DrawString(Text, Font, foreBrush, pos);
            }
        }

        private PointF GetTextPosition(Size image, SizeF text) {
            float x = GetTextPositionX(image, text);
            float y = GetTextPositionY(image, text);
            return new PointF(x, y);
        }

        private float GetTextPositionX(Size image, SizeF text) {
            float x;
            switch (HorizontalAlignment) {
                default:
                case HorizontalAlignment.Left:
                    x = MarginX;
                    break;
                case HorizontalAlignment.Center:
                    x = (image.Width - text.Width) / 2.0f;
                    break;
                case HorizontalAlignment.Right:
                    x = image.Width - text.Width - MarginX;
                    break;
            }
            return x;
        }

        private float GetTextPositionY(Size image, SizeF text) {
            float y;
            switch (VerticalAlignment) {
                case VerticalAlignment.Top:
                    y = MarginY;
                    break;
                case VerticalAlignment.Center:
                    y = (image.Height - text.Height) / 2.0f;
                    break;
                case VerticalAlignment.Bottom:
                default:
                    y = image.Height - text.Height - MarginY;
                    break;
            }
            return y;
        }
    }
}
