/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class ImageTextEditor : Form {
        private ImageText config;

        public ImageTextEditor() {
            InitializeComponent();
            Icon = Properties.Resources.Icon;
            InitializeDorpDownList();
            config = new ImageText();
        }

        public ImageText Config {
            get {
                return config;
            }
            set {
                config = value;
                SetConfigToUI(config);
            }
        }

        private void InitializeDorpDownList() {
            uiHAlign.Items.Clear();
            uiHAlign.Items.Add(HorizontalAlignment.Left);
            uiHAlign.Items.Add(HorizontalAlignment.Center);
            uiHAlign.Items.Add(HorizontalAlignment.Right);

            uiVAlign.Items.Clear();
            uiVAlign.Items.Add(VerticalAlignment.Top);
            uiVAlign.Items.Add(VerticalAlignment.Center);
            uiVAlign.Items.Add(VerticalAlignment.Bottom);
        }

        private void SetConfigToUI(ImageText config) {
            uiText.Text = config.Text;
            uiText.Font = config.Font;
            uiFontName.Text = config.FontName;
            uiForeColor.BackColor = config.ForeColor;
            uiBackColor.BackColor = config.BackColor;
            uiEdge.Checked = config.Edge;
            uiHAlign.SelectedItem = config.HorizontalAlignment;
            uiVAlign.SelectedItem = config.VerticalAlignment;
            uiMarginX.Text = config.MarginX.ToString();
            uiMarginY.Text = config.MarginY.ToString();
        }

        private ImageText GetConfigFromUI() {
            var config = new ImageText();

            config.Text = uiText.Text;
            config.Font = uiText.Font;
            config.ForeColor = uiForeColor.BackColor;
            config.BackColor = uiBackColor.BackColor;
            config.Edge = uiEdge.Checked;
            config.HorizontalAlignment = (HorizontalAlignment)uiHAlign.SelectedItem;
            config.VerticalAlignment = (VerticalAlignment)uiVAlign.SelectedItem;
            config.MarginX = float.Parse(uiMarginX.Text);
            config.MarginY = float.Parse(uiMarginY.Text);

            return config;
        }

        private void ShowWarning(string s) {
            MessageBox.Show(this, s, Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void uiEditFont_Click(object sender, EventArgs e) {
            uiFontDialog.Font = uiText.Font;
            if (uiFontDialog.ShowDialog(this) == DialogResult.OK) {
                uiText.Font = uiFontDialog.Font;
                var fc = new FontConverter();
                uiFontName.Text = fc.ConvertToString(uiFontDialog.Font);
            }
        }

        private void uiEditForeColor_Click(object sender, EventArgs e) {
            uiColorDialog.Color = uiForeColor.BackColor;
            if (uiColorDialog.ShowDialog(this) == DialogResult.OK)
                uiForeColor.BackColor = uiColorDialog.Color;
        }

        private void uiEditBackColor_Click(object sender, EventArgs e) {
            uiColorDialog.Color = uiBackColor.BackColor;
            if (uiColorDialog.ShowDialog(this) == DialogResult.OK)
                uiBackColor.BackColor = uiColorDialog.Color;
        }

        private void uiOk_Click(object sender, EventArgs e) {
            try {
                config = GetConfigFromUI();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (FormatException ex) {
                ShowWarning(ex.Message);
            }
            catch (OverflowException ex) {
                ShowWarning(ex.Message);
            }
        }

        private void uiCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
