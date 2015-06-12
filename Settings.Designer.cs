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
            this.uiWindowSettingsAdd = new System.Windows.Forms.Button();
            this.uiWindowSettingsList = new System.Windows.Forms.ListBox();
            this.uiWindowSettingsDelete = new System.Windows.Forms.Button();
            this.uiOk = new System.Windows.Forms.Button();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiReset = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.uiImageTextDelete = new System.Windows.Forms.Button();
            this.uiImageTextEdit = new System.Windows.Forms.Button();
            this.uiImageTextAdd = new System.Windows.Forms.Button();
            this.uiImageTextList = new System.Windows.Forms.ListBox();
            this.uiEnableImageText = new System.Windows.Forms.CheckBox();
            this.uiImageFormatName = new System.Windows.Forms.ComboBox();
            this.uiEditHotKey = new System.Windows.Forms.Button();
            this.uiEnableHotKey = new System.Windows.Forms.CheckBox();
            this.uiHotKey = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.uiEditImageFolder = new System.Windows.Forms.Button();
            this.uiImageFolder = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.uiEnableImageGamma = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uiSaveWindowPosition = new System.Windows.Forms.CheckBox();
            this.uiWindowSettingsEdit = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uiAdminMode = new System.Windows.Forms.CheckBox();
            this.uiStartUpFFXI = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiFontDialog = new System.Windows.Forms.FontDialog();
            this.uiColorDialog = new System.Windows.Forms.ColorDialog();
            this.uiFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "FFXI のガンマ値(&F):";
            // 
            // uiAppGamma
            // 
            this.uiAppGamma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiAppGamma.Location = new System.Drawing.Point(142, 6);
            this.uiAppGamma.Name = "uiAppGamma";
            this.uiAppGamma.Size = new System.Drawing.Size(214, 19);
            this.uiAppGamma.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Windows のガンマ値(&W):";
            // 
            // uiSystemGamma
            // 
            this.uiSystemGamma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiSystemGamma.Location = new System.Drawing.Point(142, 31);
            this.uiSystemGamma.Name = "uiSystemGamma";
            this.uiSystemGamma.Size = new System.Drawing.Size(214, 19);
            this.uiSystemGamma.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "ガンマを変更するウィンドウの名前(&N):";
            // 
            // uiWindowSettingsAdd
            // 
            this.uiWindowSettingsAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiWindowSettingsAdd.Location = new System.Drawing.Point(281, 48);
            this.uiWindowSettingsAdd.Name = "uiWindowSettingsAdd";
            this.uiWindowSettingsAdd.Size = new System.Drawing.Size(75, 23);
            this.uiWindowSettingsAdd.TabIndex = 3;
            this.uiWindowSettingsAdd.Text = "追加...";
            this.uiWindowSettingsAdd.UseVisualStyleBackColor = true;
            this.uiWindowSettingsAdd.Click += new System.EventHandler(this.uiWindowSettingsAdd_Click);
            // 
            // uiWindowSettingsList
            // 
            this.uiWindowSettingsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiWindowSettingsList.FormattingEnabled = true;
            this.uiWindowSettingsList.ItemHeight = 12;
            this.uiWindowSettingsList.Location = new System.Drawing.Point(8, 48);
            this.uiWindowSettingsList.Name = "uiWindowSettingsList";
            this.uiWindowSettingsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.uiWindowSettingsList.Size = new System.Drawing.Size(267, 244);
            this.uiWindowSettingsList.TabIndex = 2;
            this.uiWindowSettingsList.DoubleClick += new System.EventHandler(this.uiWindowSettingsEdit_Click);
            // 
            // uiWindowSettingsDelete
            // 
            this.uiWindowSettingsDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiWindowSettingsDelete.Location = new System.Drawing.Point(281, 106);
            this.uiWindowSettingsDelete.Name = "uiWindowSettingsDelete";
            this.uiWindowSettingsDelete.Size = new System.Drawing.Size(75, 23);
            this.uiWindowSettingsDelete.TabIndex = 5;
            this.uiWindowSettingsDelete.Text = "削除";
            this.uiWindowSettingsDelete.UseVisualStyleBackColor = true;
            this.uiWindowSettingsDelete.Click += new System.EventHandler(this.uiWindowSettingsDelete_Click);
            // 
            // uiOk
            // 
            this.uiOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiOk.Location = new System.Drawing.Point(213, 4);
            this.uiOk.Name = "uiOk";
            this.uiOk.Size = new System.Drawing.Size(75, 23);
            this.uiOk.TabIndex = 1;
            this.uiOk.Text = "OK";
            this.uiOk.UseVisualStyleBackColor = true;
            this.uiOk.Click += new System.EventHandler(this.uiOk_Click);
            // 
            // uiCancel
            // 
            this.uiCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(294, 4);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 2;
            this.uiCancel.Text = "キャンセル";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // uiReset
            // 
            this.uiReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiReset.Location = new System.Drawing.Point(132, 4);
            this.uiReset.Name = "uiReset";
            this.uiReset.Size = new System.Drawing.Size(75, 23);
            this.uiReset.TabIndex = 0;
            this.uiReset.Text = "全て初期化";
            this.uiReset.UseVisualStyleBackColor = true;
            this.uiReset.Click += new System.EventHandler(this.uiReset_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(372, 327);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.uiImageTextDelete);
            this.tabPage3.Controls.Add(this.uiImageTextEdit);
            this.tabPage3.Controls.Add(this.uiImageTextAdd);
            this.tabPage3.Controls.Add(this.uiImageTextList);
            this.tabPage3.Controls.Add(this.uiEnableImageText);
            this.tabPage3.Controls.Add(this.uiImageFormatName);
            this.tabPage3.Controls.Add(this.uiEditHotKey);
            this.tabPage3.Controls.Add(this.uiEnableHotKey);
            this.tabPage3.Controls.Add(this.uiHotKey);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.uiEditImageFolder);
            this.tabPage3.Controls.Add(this.uiImageFolder);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.uiEnableImageGamma);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(364, 301);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "画像保存";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // uiImageTextDelete
            // 
            this.uiImageTextDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiImageTextDelete.Location = new System.Drawing.Point(281, 236);
            this.uiImageTextDelete.Name = "uiImageTextDelete";
            this.uiImageTextDelete.Size = new System.Drawing.Size(75, 23);
            this.uiImageTextDelete.TabIndex = 14;
            this.uiImageTextDelete.Text = "削除";
            this.uiImageTextDelete.UseVisualStyleBackColor = true;
            this.uiImageTextDelete.Click += new System.EventHandler(this.uiImageTextDelete_Click);
            // 
            // uiImageTextEdit
            // 
            this.uiImageTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiImageTextEdit.Location = new System.Drawing.Point(281, 207);
            this.uiImageTextEdit.Name = "uiImageTextEdit";
            this.uiImageTextEdit.Size = new System.Drawing.Size(75, 23);
            this.uiImageTextEdit.TabIndex = 13;
            this.uiImageTextEdit.Text = "編集...";
            this.uiImageTextEdit.UseVisualStyleBackColor = true;
            this.uiImageTextEdit.Click += new System.EventHandler(this.uiImageTextEdit_Click);
            // 
            // uiImageTextAdd
            // 
            this.uiImageTextAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiImageTextAdd.Location = new System.Drawing.Point(281, 178);
            this.uiImageTextAdd.Name = "uiImageTextAdd";
            this.uiImageTextAdd.Size = new System.Drawing.Size(75, 23);
            this.uiImageTextAdd.TabIndex = 12;
            this.uiImageTextAdd.Text = "追加...";
            this.uiImageTextAdd.UseVisualStyleBackColor = true;
            this.uiImageTextAdd.Click += new System.EventHandler(this.uiImageTextAdd_Click);
            // 
            // uiImageTextList
            // 
            this.uiImageTextList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiImageTextList.FormattingEnabled = true;
            this.uiImageTextList.ItemHeight = 12;
            this.uiImageTextList.Location = new System.Drawing.Point(8, 178);
            this.uiImageTextList.Name = "uiImageTextList";
            this.uiImageTextList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.uiImageTextList.Size = new System.Drawing.Size(267, 112);
            this.uiImageTextList.TabIndex = 11;
            this.uiImageTextList.DoubleClick += new System.EventHandler(this.uiImageTextEdit_Click);
            // 
            // uiEnableImageText
            // 
            this.uiEnableImageText.AutoSize = true;
            this.uiEnableImageText.Location = new System.Drawing.Point(8, 156);
            this.uiEnableImageText.Name = "uiEnableImageText";
            this.uiEnableImageText.Size = new System.Drawing.Size(149, 16);
            this.uiEnableImageText.TabIndex = 10;
            this.uiEnableImageText.Text = "画像に文字を追記する(&A)";
            this.uiEnableImageText.UseVisualStyleBackColor = true;
            // 
            // uiImageFormatName
            // 
            this.uiImageFormatName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiImageFormatName.FormattingEnabled = true;
            this.uiImageFormatName.Items.AddRange(new object[] {
            "png",
            "jpg"});
            this.uiImageFormatName.Location = new System.Drawing.Point(84, 74);
            this.uiImageFormatName.Name = "uiImageFormatName";
            this.uiImageFormatName.Size = new System.Drawing.Size(86, 20);
            this.uiImageFormatName.TabIndex = 5;
            // 
            // uiEditHotKey
            // 
            this.uiEditHotKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiEditHotKey.Location = new System.Drawing.Point(281, 126);
            this.uiEditHotKey.Name = "uiEditHotKey";
            this.uiEditHotKey.Size = new System.Drawing.Size(75, 23);
            this.uiEditHotKey.TabIndex = 9;
            this.uiEditHotKey.Text = "設定...";
            this.uiEditHotKey.UseVisualStyleBackColor = true;
            this.uiEditHotKey.Click += new System.EventHandler(this.uiEditHotKey_Click);
            // 
            // uiEnableHotKey
            // 
            this.uiEnableHotKey.AutoSize = true;
            this.uiEnableHotKey.Location = new System.Drawing.Point(8, 106);
            this.uiEnableHotKey.Name = "uiEnableHotKey";
            this.uiEnableHotKey.Size = new System.Drawing.Size(146, 16);
            this.uiEnableHotKey.TabIndex = 6;
            this.uiEnableHotKey.Text = "ホットキーを有効にする(&H)";
            this.uiEnableHotKey.UseVisualStyleBackColor = true;
            this.uiEnableHotKey.Click += new System.EventHandler(this.uiEnableHotKey_Click);
            // 
            // uiHotKey
            // 
            this.uiHotKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiHotKey.Location = new System.Drawing.Point(84, 128);
            this.uiHotKey.Name = "uiHotKey";
            this.uiHotKey.ReadOnly = true;
            this.uiHotKey.Size = new System.Drawing.Size(191, 19);
            this.uiHotKey.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 12);
            this.label14.TabIndex = 7;
            this.label14.Text = "ホットキー(&K):";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "画像形式(&T):";
            // 
            // uiEditImageFolder
            // 
            this.uiEditImageFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiEditImageFolder.Location = new System.Drawing.Point(281, 47);
            this.uiEditImageFolder.Name = "uiEditImageFolder";
            this.uiEditImageFolder.Size = new System.Drawing.Size(75, 23);
            this.uiEditImageFolder.TabIndex = 3;
            this.uiEditImageFolder.Text = "参照...";
            this.uiEditImageFolder.UseVisualStyleBackColor = true;
            this.uiEditImageFolder.Click += new System.EventHandler(this.uiEditImageFolder_Click);
            // 
            // uiImageFolder
            // 
            this.uiImageFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiImageFolder.Location = new System.Drawing.Point(8, 49);
            this.uiImageFolder.Name = "uiImageFolder";
            this.uiImageFolder.Size = new System.Drawing.Size(267, 19);
            this.uiImageFolder.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "保存先フォルダー(&F):";
            // 
            // uiEnableImageGamma
            // 
            this.uiEnableImageGamma.AutoSize = true;
            this.uiEnableImageGamma.Location = new System.Drawing.Point(8, 7);
            this.uiEnableImageGamma.Name = "uiEnableImageGamma";
            this.uiEnableImageGamma.Size = new System.Drawing.Size(153, 16);
            this.uiEnableImageGamma.TabIndex = 0;
            this.uiEnableImageGamma.Text = "ガンマ補正を有効にする(&G)";
            this.uiEnableImageGamma.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.uiSaveWindowPosition);
            this.tabPage2.Controls.Add(this.uiWindowSettingsEdit);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.uiWindowSettingsList);
            this.tabPage2.Controls.Add(this.uiWindowSettingsAdd);
            this.tabPage2.Controls.Add(this.uiWindowSettingsDelete);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(364, 301);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "ウィンドウ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // uiSaveWindowPosition
            // 
            this.uiSaveWindowPosition.AutoSize = true;
            this.uiSaveWindowPosition.Location = new System.Drawing.Point(8, 7);
            this.uiSaveWindowPosition.Name = "uiSaveWindowPosition";
            this.uiSaveWindowPosition.Size = new System.Drawing.Size(170, 16);
            this.uiSaveWindowPosition.TabIndex = 0;
            this.uiSaveWindowPosition.Text = "ウィンドウの位置を記憶する(&W)";
            this.uiSaveWindowPosition.UseVisualStyleBackColor = true;
            this.uiSaveWindowPosition.Click += new System.EventHandler(this.uiSaveWindowPosition_Click);
            // 
            // uiWindowSettingsEdit
            // 
            this.uiWindowSettingsEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiWindowSettingsEdit.Location = new System.Drawing.Point(281, 77);
            this.uiWindowSettingsEdit.Name = "uiWindowSettingsEdit";
            this.uiWindowSettingsEdit.Size = new System.Drawing.Size(75, 23);
            this.uiWindowSettingsEdit.TabIndex = 4;
            this.uiWindowSettingsEdit.Text = "編集...";
            this.uiWindowSettingsEdit.UseVisualStyleBackColor = true;
            this.uiWindowSettingsEdit.Click += new System.EventHandler(this.uiWindowSettingsEdit_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uiAdminMode);
            this.tabPage1.Controls.Add(this.uiStartUpFFXI);
            this.tabPage1.Controls.Add(this.uiAppGamma);
            this.tabPage1.Controls.Add(this.uiSystemGamma);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(364, 301);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "その他";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // uiAdminMode
            // 
            this.uiAdminMode.AutoSize = true;
            this.uiAdminMode.Location = new System.Drawing.Point(10, 59);
            this.uiAdminMode.Name = "uiAdminMode";
            this.uiAdminMode.Size = new System.Drawing.Size(178, 16);
            this.uiAdminMode.TabIndex = 4;
            this.uiAdminMode.Text = "常に管理者モードで起動する(&A)";
            this.uiAdminMode.UseVisualStyleBackColor = true;
            // 
            // uiStartUpFFXI
            // 
            this.uiStartUpFFXI.AutoSize = true;
            this.uiStartUpFFXI.Location = new System.Drawing.Point(10, 84);
            this.uiStartUpFFXI.Name = "uiStartUpFFXI";
            this.uiStartUpFFXI.Size = new System.Drawing.Size(238, 16);
            this.uiStartUpFFXI.TabIndex = 5;
            this.uiStartUpFFXI.Text = "起動時に FINAL FANTSY XI を起動する(&F)";
            this.uiStartUpFFXI.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiCancel);
            this.panel1.Controls.Add(this.uiOk);
            this.panel1.Controls.Add(this.uiReset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 30);
            this.panel1.TabIndex = 1;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(372, 357);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(304, 372);
            this.Name = "Settings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFXI Gamma";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiAppGamma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uiSystemGamma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button uiWindowSettingsAdd;
        private System.Windows.Forms.ListBox uiWindowSettingsList;
        private System.Windows.Forms.Button uiWindowSettingsDelete;
        private System.Windows.Forms.Button uiOk;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.Button uiReset;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FontDialog uiFontDialog;
        private System.Windows.Forms.ColorDialog uiColorDialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox uiEnableImageGamma;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button uiEditImageFolder;
        private System.Windows.Forms.TextBox uiImageFolder;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button uiEditHotKey;
        private System.Windows.Forms.CheckBox uiEnableHotKey;
        private System.Windows.Forms.TextBox uiHotKey;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox uiImageFormatName;
        private System.Windows.Forms.FolderBrowserDialog uiFolderDialog;
        private System.Windows.Forms.CheckBox uiStartUpFFXI;
        private System.Windows.Forms.CheckBox uiAdminMode;
        private System.Windows.Forms.Button uiImageTextDelete;
        private System.Windows.Forms.Button uiImageTextEdit;
        private System.Windows.Forms.Button uiImageTextAdd;
        private System.Windows.Forms.ListBox uiImageTextList;
        private System.Windows.Forms.CheckBox uiEnableImageText;
        private System.Windows.Forms.Button uiWindowSettingsEdit;
        private System.Windows.Forms.CheckBox uiSaveWindowPosition;
    }
}