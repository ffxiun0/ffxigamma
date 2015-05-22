namespace ffxigamma {
    partial class Settings {
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
            this.uiAppGamma = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uiSystemGamma = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.uiName = new System.Windows.Forms.TextBox();
            this.uiAdd = new System.Windows.Forms.Button();
            this.uiNameList = new System.Windows.Forms.ListBox();
            this.uiDelete = new System.Windows.Forms.Button();
            this.uiOk = new System.Windows.Forms.Button();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "FFXI のガンマ値(&F):";
            // 
            // uiAppGamma
            // 
            this.uiAppGamma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiAppGamma.Location = new System.Drawing.Point(139, 150);
            this.uiAppGamma.Name = "uiAppGamma";
            this.uiAppGamma.Size = new System.Drawing.Size(149, 19);
            this.uiAppGamma.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Windows のガンマ値(&W):";
            // 
            // uiSystemGamma
            // 
            this.uiSystemGamma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiSystemGamma.Location = new System.Drawing.Point(139, 178);
            this.uiSystemGamma.Name = "uiSystemGamma";
            this.uiSystemGamma.Size = new System.Drawing.Size(149, 19);
            this.uiSystemGamma.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "ガンマを変更するウィンドウの名前(&N):";
            // 
            // uiName
            // 
            this.uiName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiName.Location = new System.Drawing.Point(12, 24);
            this.uiName.Name = "uiName";
            this.uiName.Size = new System.Drawing.Size(195, 19);
            this.uiName.TabIndex = 1;
            // 
            // uiAdd
            // 
            this.uiAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiAdd.Location = new System.Drawing.Point(213, 22);
            this.uiAdd.Name = "uiAdd";
            this.uiAdd.Size = new System.Drawing.Size(75, 23);
            this.uiAdd.TabIndex = 2;
            this.uiAdd.Text = "追加(&A)";
            this.uiAdd.UseVisualStyleBackColor = true;
            this.uiAdd.Click += new System.EventHandler(this.uiAdd_Click);
            // 
            // uiNameList
            // 
            this.uiNameList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiNameList.FormattingEnabled = true;
            this.uiNameList.ItemHeight = 12;
            this.uiNameList.Location = new System.Drawing.Point(12, 49);
            this.uiNameList.Name = "uiNameList";
            this.uiNameList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.uiNameList.Size = new System.Drawing.Size(195, 88);
            this.uiNameList.TabIndex = 3;
            // 
            // uiDelete
            // 
            this.uiDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiDelete.Location = new System.Drawing.Point(213, 51);
            this.uiDelete.Name = "uiDelete";
            this.uiDelete.Size = new System.Drawing.Size(75, 23);
            this.uiDelete.TabIndex = 4;
            this.uiDelete.Text = "削除(&D)";
            this.uiDelete.UseVisualStyleBackColor = true;
            this.uiDelete.Click += new System.EventHandler(this.uiDelete_Click);
            // 
            // uiOk
            // 
            this.uiOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiOk.Location = new System.Drawing.Point(132, 210);
            this.uiOk.Name = "uiOk";
            this.uiOk.Size = new System.Drawing.Size(75, 23);
            this.uiOk.TabIndex = 10;
            this.uiOk.Text = "OK";
            this.uiOk.UseVisualStyleBackColor = true;
            this.uiOk.Click += new System.EventHandler(this.uiOk_Click);
            // 
            // uiCancel
            // 
            this.uiCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(213, 210);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 11;
            this.uiCancel.Text = "キャンセル";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // uiReset
            // 
            this.uiReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiReset.Location = new System.Drawing.Point(51, 210);
            this.uiReset.Name = "uiReset";
            this.uiReset.Size = new System.Drawing.Size(75, 23);
            this.uiReset.TabIndex = 9;
            this.uiReset.Text = "初期値(&I)";
            this.uiReset.UseVisualStyleBackColor = true;
            this.uiReset.Click += new System.EventHandler(this.uiReset_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(298, 243);
            this.Controls.Add(this.uiReset);
            this.Controls.Add(this.uiCancel);
            this.Controls.Add(this.uiOk);
            this.Controls.Add(this.uiDelete);
            this.Controls.Add(this.uiNameList);
            this.Controls.Add(this.uiAdd);
            this.Controls.Add(this.uiName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uiSystemGamma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uiAppGamma);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFXI Gamma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiAppGamma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uiSystemGamma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox uiName;
        private System.Windows.Forms.Button uiAdd;
        private System.Windows.Forms.ListBox uiNameList;
        private System.Windows.Forms.Button uiDelete;
        private System.Windows.Forms.Button uiOk;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.Button uiReset;
    }
}