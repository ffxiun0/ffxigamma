namespace ffxigamma {
    partial class WindowSettingsEditor {
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
            this.label1 = new System.Windows.Forms.Label();
            this.uiName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uiPositionX = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.uiPositionY = new System.Windows.Forms.NumericUpDown();
            this.uiOk = new System.Windows.Forms.Button();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiHeight = new System.Windows.Forms.NumericUpDown();
            this.uiWidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.uiDisplay = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.uiChangeSize = new System.Windows.Forms.CheckBox();
            this.uiAlwaysGamma = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ウィンドウ名(&N):";
            // 
            // uiName
            // 
            this.uiName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiName.Location = new System.Drawing.Point(12, 24);
            this.uiName.Name = "uiName";
            this.uiName.Size = new System.Drawing.Size(359, 19);
            this.uiName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "位置&X:";
            // 
            // uiPositionX
            // 
            this.uiPositionX.Location = new System.Drawing.Point(65, 49);
            this.uiPositionX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.uiPositionX.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.uiPositionX.Name = "uiPositionX";
            this.uiPositionX.Size = new System.Drawing.Size(79, 19);
            this.uiPositionX.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "位置&Y:";
            // 
            // uiPositionY
            // 
            this.uiPositionY.Location = new System.Drawing.Point(65, 74);
            this.uiPositionY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.uiPositionY.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.uiPositionY.Name = "uiPositionY";
            this.uiPositionY.Size = new System.Drawing.Size(79, 19);
            this.uiPositionY.TabIndex = 5;
            // 
            // uiOk
            // 
            this.uiOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiOk.Location = new System.Drawing.Point(215, 159);
            this.uiOk.Name = "uiOk";
            this.uiOk.Size = new System.Drawing.Size(75, 23);
            this.uiOk.TabIndex = 14;
            this.uiOk.Text = "OK";
            this.uiOk.UseVisualStyleBackColor = true;
            this.uiOk.Click += new System.EventHandler(this.uiOk_Click);
            // 
            // uiCancel
            // 
            this.uiCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(296, 159);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 15;
            this.uiCancel.Text = "キャンセル";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // uiHeight
            // 
            this.uiHeight.Location = new System.Drawing.Point(65, 124);
            this.uiHeight.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.uiHeight.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.uiHeight.Name = "uiHeight";
            this.uiHeight.Size = new System.Drawing.Size(79, 19);
            this.uiHeight.TabIndex = 9;
            // 
            // uiWidth
            // 
            this.uiWidth.Location = new System.Drawing.Point(65, 99);
            this.uiWidth.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.uiWidth.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.uiWidth.Name = "uiWidth";
            this.uiWidth.Size = new System.Drawing.Size(79, 19);
            this.uiWidth.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "高さ(&H):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "幅(&W):";
            // 
            // uiDisplay
            // 
            this.uiDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiDisplay.FormattingEnabled = true;
            this.uiDisplay.Location = new System.Drawing.Point(160, 66);
            this.uiDisplay.Name = "uiDisplay";
            this.uiDisplay.Size = new System.Drawing.Size(121, 20);
            this.uiDisplay.TabIndex = 11;
            this.uiDisplay.SelectionChangeCommitted += new System.EventHandler(this.uiDisplay_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(158, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "ディスプレイの位置を設定(&D):";
            // 
            // uiChangeSize
            // 
            this.uiChangeSize.AutoSize = true;
            this.uiChangeSize.Location = new System.Drawing.Point(160, 100);
            this.uiChangeSize.Name = "uiChangeSize";
            this.uiChangeSize.Size = new System.Drawing.Size(131, 16);
            this.uiChangeSize.TabIndex = 12;
            this.uiChangeSize.Text = "幅と高さを変更する(&S)";
            this.uiChangeSize.UseVisualStyleBackColor = true;
            // 
            // uiAlwaysGamma
            // 
            this.uiAlwaysGamma.AutoSize = true;
            this.uiAlwaysGamma.Location = new System.Drawing.Point(160, 125);
            this.uiAlwaysGamma.Name = "uiAlwaysGamma";
            this.uiAlwaysGamma.Size = new System.Drawing.Size(204, 16);
            this.uiAlwaysGamma.TabIndex = 13;
            this.uiAlwaysGamma.Text = "非アクティブ時にもガンマ補正をする(&G)";
            this.uiAlwaysGamma.UseVisualStyleBackColor = true;
            // 
            // WindowSettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(383, 194);
            this.Controls.Add(this.uiAlwaysGamma);
            this.Controls.Add(this.uiChangeSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uiDisplay);
            this.Controls.Add(this.uiHeight);
            this.Controls.Add(this.uiWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uiCancel);
            this.Controls.Add(this.uiOk);
            this.Controls.Add(this.uiPositionY);
            this.Controls.Add(this.uiPositionX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uiName);
            this.Controls.Add(this.label1);
            this.Name = "WindowSettingsEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ウィンドウ設定";
            this.Load += new System.EventHandler(this.WindowSettingsEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown uiPositionX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown uiPositionY;
        private System.Windows.Forms.Button uiOk;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.NumericUpDown uiHeight;
        private System.Windows.Forms.NumericUpDown uiWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox uiDisplay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox uiChangeSize;
        private System.Windows.Forms.CheckBox uiAlwaysGamma;
    }
}