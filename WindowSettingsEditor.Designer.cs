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
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionY)).BeginInit();
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
            this.uiName.Size = new System.Drawing.Size(252, 19);
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
            this.uiOk.Location = new System.Drawing.Point(108, 104);
            this.uiOk.Name = "uiOk";
            this.uiOk.Size = new System.Drawing.Size(75, 23);
            this.uiOk.TabIndex = 6;
            this.uiOk.Text = "OK";
            this.uiOk.UseVisualStyleBackColor = true;
            this.uiOk.Click += new System.EventHandler(this.uiOk_Click);
            // 
            // uiCancel
            // 
            this.uiCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(189, 104);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 7;
            this.uiCancel.Text = "キャンセル";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // WindowSettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(276, 139);
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
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPositionY)).EndInit();
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
    }
}