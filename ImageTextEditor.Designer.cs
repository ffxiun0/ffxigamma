namespace ffxigamma {
    partial class ImageTextEditor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.uiEditBackColor = new System.Windows.Forms.Button();
            this.uiBackColor = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.uiEditForeColor = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.uiMarginY = new System.Windows.Forms.TextBox();
            this.uiMarginX = new System.Windows.Forms.TextBox();
            this.uiVAlign = new System.Windows.Forms.ComboBox();
            this.uiHAlign = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.uiForeColor = new System.Windows.Forms.Panel();
            this.uiFontName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.uiEditFont = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.uiText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uiOk = new System.Windows.Forms.Button();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiFontDialog = new System.Windows.Forms.FontDialog();
            this.uiColorDialog = new System.Windows.Forms.ColorDialog();
            this.uiEdge = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // uiEditBackColor
            // 
            this.uiEditBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiEditBackColor.Location = new System.Drawing.Point(453, 205);
            this.uiEditBackColor.Name = "uiEditBackColor";
            this.uiEditBackColor.Size = new System.Drawing.Size(75, 23);
            this.uiEditBackColor.TabIndex = 30;
            this.uiEditBackColor.Text = "編集...";
            this.uiEditBackColor.UseVisualStyleBackColor = true;
            this.uiEditBackColor.Click += new System.EventHandler(this.uiEditBackColor_Click);
            // 
            // uiBackColor
            // 
            this.uiBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiBackColor.Location = new System.Drawing.Point(109, 206);
            this.uiBackColor.Name = "uiBackColor";
            this.uiBackColor.Size = new System.Drawing.Size(86, 20);
            this.uiBackColor.TabIndex = 29;
            this.uiBackColor.Click += new System.EventHandler(this.uiEditBackColor_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 28;
            this.label11.Text = "背景色(&B):";
            // 
            // uiEditForeColor
            // 
            this.uiEditForeColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiEditForeColor.Location = new System.Drawing.Point(453, 179);
            this.uiEditForeColor.Name = "uiEditForeColor";
            this.uiEditForeColor.Size = new System.Drawing.Size(75, 23);
            this.uiEditForeColor.TabIndex = 27;
            this.uiEditForeColor.Text = "編集...";
            this.uiEditForeColor.UseVisualStyleBackColor = true;
            this.uiEditForeColor.Click += new System.EventHandler(this.uiEditForeColor_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 312);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 12);
            this.label10.TabIndex = 37;
            this.label10.Text = "上下の余白(&Y):";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 287);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "左右の余白(&X):";
            // 
            // uiMarginY
            // 
            this.uiMarginY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiMarginY.Location = new System.Drawing.Point(109, 309);
            this.uiMarginY.Name = "uiMarginY";
            this.uiMarginY.Size = new System.Drawing.Size(86, 19);
            this.uiMarginY.TabIndex = 38;
            // 
            // uiMarginX
            // 
            this.uiMarginX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiMarginX.Location = new System.Drawing.Point(109, 284);
            this.uiMarginX.Name = "uiMarginX";
            this.uiMarginX.Size = new System.Drawing.Size(86, 19);
            this.uiMarginX.TabIndex = 36;
            // 
            // uiVAlign
            // 
            this.uiVAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiVAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiVAlign.FormattingEnabled = true;
            this.uiVAlign.Location = new System.Drawing.Point(109, 258);
            this.uiVAlign.Name = "uiVAlign";
            this.uiVAlign.Size = new System.Drawing.Size(86, 20);
            this.uiVAlign.TabIndex = 34;
            // 
            // uiHAlign
            // 
            this.uiHAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiHAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiHAlign.FormattingEnabled = true;
            this.uiHAlign.Location = new System.Drawing.Point(109, 232);
            this.uiHAlign.Name = "uiHAlign";
            this.uiHAlign.Size = new System.Drawing.Size(86, 20);
            this.uiHAlign.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 261);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "垂直位置(&V):";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "水平位置(&H):";
            // 
            // uiForeColor
            // 
            this.uiForeColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiForeColor.Location = new System.Drawing.Point(109, 180);
            this.uiForeColor.Name = "uiForeColor";
            this.uiForeColor.Size = new System.Drawing.Size(86, 20);
            this.uiForeColor.TabIndex = 26;
            this.uiForeColor.Click += new System.EventHandler(this.uiEditForeColor_Click);
            // 
            // uiFontName
            // 
            this.uiFontName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiFontName.Location = new System.Drawing.Point(109, 155);
            this.uiFontName.Name = "uiFontName";
            this.uiFontName.ReadOnly = true;
            this.uiFontName.Size = new System.Drawing.Size(180, 19);
            this.uiFontName.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "文字色(&C):";
            // 
            // uiEditFont
            // 
            this.uiEditFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiEditFont.Location = new System.Drawing.Point(453, 153);
            this.uiEditFont.Name = "uiEditFont";
            this.uiEditFont.Size = new System.Drawing.Size(75, 23);
            this.uiEditFont.TabIndex = 24;
            this.uiEditFont.Text = "編集...";
            this.uiEditFont.UseVisualStyleBackColor = true;
            this.uiEditFont.Click += new System.EventHandler(this.uiEditFont_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "フォント(&F):";
            // 
            // uiText
            // 
            this.uiText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiText.Location = new System.Drawing.Point(12, 23);
            this.uiText.MaxLength = 1000000;
            this.uiText.Multiline = true;
            this.uiText.Name = "uiText";
            this.uiText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.uiText.Size = new System.Drawing.Size(516, 124);
            this.uiText.TabIndex = 21;
            this.uiText.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "文字(&T):";
            // 
            // uiOk
            // 
            this.uiOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiOk.Location = new System.Drawing.Point(372, 336);
            this.uiOk.Name = "uiOk";
            this.uiOk.Size = new System.Drawing.Size(75, 23);
            this.uiOk.TabIndex = 39;
            this.uiOk.Text = "OK";
            this.uiOk.UseVisualStyleBackColor = true;
            this.uiOk.Click += new System.EventHandler(this.uiOk_Click);
            // 
            // uiCancel
            // 
            this.uiCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(453, 336);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 40;
            this.uiCancel.Text = "キャンセル";
            this.uiCancel.UseVisualStyleBackColor = true;
            this.uiCancel.Click += new System.EventHandler(this.uiCancel_Click);
            // 
            // uiEdge
            // 
            this.uiEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiEdge.AutoSize = true;
            this.uiEdge.Location = new System.Drawing.Point(201, 209);
            this.uiEdge.Name = "uiEdge";
            this.uiEdge.Size = new System.Drawing.Size(93, 16);
            this.uiEdge.TabIndex = 41;
            this.uiEdge.Text = "縁取りをつける";
            this.uiEdge.UseVisualStyleBackColor = true;
            // 
            // ImageTextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 371);
            this.Controls.Add(this.uiEdge);
            this.Controls.Add(this.uiCancel);
            this.Controls.Add(this.uiOk);
            this.Controls.Add(this.uiEditBackColor);
            this.Controls.Add(this.uiBackColor);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.uiEditForeColor);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.uiMarginY);
            this.Controls.Add(this.uiMarginX);
            this.Controls.Add(this.uiVAlign);
            this.Controls.Add(this.uiHAlign);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.uiForeColor);
            this.Controls.Add(this.uiFontName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uiEditFont);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uiText);
            this.Controls.Add(this.label4);
            this.MinimumSize = new System.Drawing.Size(398, 331);
            this.Name = "ImageTextEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "編集";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uiEditBackColor;
        private System.Windows.Forms.Panel uiBackColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button uiEditForeColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox uiMarginY;
        private System.Windows.Forms.TextBox uiMarginX;
        private System.Windows.Forms.ComboBox uiVAlign;
        private System.Windows.Forms.ComboBox uiHAlign;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel uiForeColor;
        private System.Windows.Forms.TextBox uiFontName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button uiEditFont;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox uiText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button uiOk;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.FontDialog uiFontDialog;
        private System.Windows.Forms.ColorDialog uiColorDialog;
        private System.Windows.Forms.CheckBox uiEdge;
    }
}