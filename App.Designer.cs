namespace ffxigamma {
    partial class App {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.uiNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.uiContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uiContextStartFFXI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.uiContextCaptureCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextCaptureSaveFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextCaptureSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.uiContextSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.uiContextExit = new System.Windows.Forms.ToolStripMenuItem();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.uiSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.uiContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiNotifyIcon
            // 
            this.uiNotifyIcon.ContextMenuStrip = this.uiContextMenu;
            this.uiNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("uiNotifyIcon.Icon")));
            this.uiNotifyIcon.Text = "FFXI Gamma";
            this.uiNotifyIcon.Visible = true;
            this.uiNotifyIcon.BalloonTipClicked += new System.EventHandler(this.uiNotifyIcon_BalloonTipClicked);
            this.uiNotifyIcon.Click += new System.EventHandler(this.uiNotifyIcon_Click);
            // 
            // uiContextMenu
            // 
            this.uiContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiContextStartFFXI,
            this.toolStripSeparator2,
            this.uiContextCaptureCopy,
            this.uiContextCaptureSaveFolder,
            this.uiContextCaptureSaveAs,
            this.toolStripSeparator3,
            this.uiContextSettings,
            this.toolStripSeparator1,
            this.uiContextExit});
            this.uiContextMenu.Name = "uiContextMenu";
            this.uiContextMenu.Size = new System.Drawing.Size(230, 154);
            this.uiContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.uiContextMenu_Opening);
            // 
            // uiContextStartFFXI
            // 
            this.uiContextStartFFXI.Name = "uiContextStartFFXI";
            this.uiContextStartFFXI.Size = new System.Drawing.Size(229, 22);
            this.uiContextStartFFXI.Text = "FAINAL FANTASY XI を起動";
            this.uiContextStartFFXI.Click += new System.EventHandler(this.uiContextStartFFXI_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // uiContextCaptureCopy
            // 
            this.uiContextCaptureCopy.Name = "uiContextCaptureCopy";
            this.uiContextCaptureCopy.Size = new System.Drawing.Size(229, 22);
            this.uiContextCaptureCopy.Text = "画像をコピー(&C)";
            this.uiContextCaptureCopy.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uiContextCaptureCopy_DropDownItemClicked);
            this.uiContextCaptureCopy.Click += new System.EventHandler(this.uiContextCaptureCopy_Click);
            // 
            // uiContextCaptureSaveFolder
            // 
            this.uiContextCaptureSaveFolder.Name = "uiContextCaptureSaveFolder";
            this.uiContextCaptureSaveFolder.Size = new System.Drawing.Size(229, 22);
            this.uiContextCaptureSaveFolder.Text = "画像をフォルダーに保存(&S)";
            this.uiContextCaptureSaveFolder.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uiContextCaptureSaveFolder_DropDownItemClicked);
            this.uiContextCaptureSaveFolder.Click += new System.EventHandler(this.uiContextCaptureSaveFolder_Click);
            // 
            // uiContextCaptureSaveAs
            // 
            this.uiContextCaptureSaveAs.Name = "uiContextCaptureSaveAs";
            this.uiContextCaptureSaveAs.Size = new System.Drawing.Size(229, 22);
            this.uiContextCaptureSaveAs.Text = "名前を付けて画像を保存(&A)...";
            this.uiContextCaptureSaveAs.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uiContextCaptureSaveAs_DropDownItemClicked);
            this.uiContextCaptureSaveAs.Click += new System.EventHandler(this.uiContextCaptureSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(226, 6);
            // 
            // uiContextSettings
            // 
            this.uiContextSettings.Name = "uiContextSettings";
            this.uiContextSettings.Size = new System.Drawing.Size(229, 22);
            this.uiContextSettings.Text = "設定(&O)...";
            this.uiContextSettings.Click += new System.EventHandler(this.uiSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(226, 6);
            // 
            // uiContextExit
            // 
            this.uiContextExit.Name = "uiContextExit";
            this.uiContextExit.Size = new System.Drawing.Size(229, 22);
            this.uiContextExit.Text = "終了(&X)";
            this.uiContextExit.Click += new System.EventHandler(this.uiExit_Click);
            // 
            // uiTimer
            // 
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // uiSaveAs
            // 
            this.uiSaveAs.Filter = "PNG イメージ|*.png|JPEG イメージ|*.jpg|すべてのファイル|*";
            this.uiSaveAs.Title = "画像を保存 - FFXI Gamma";
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 272);
            this.Name = "App";
            this.ShowInTaskbar = false;
            this.Text = "FFXI Gamma";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.Load += new System.EventHandler(this.App_Load);
            this.uiContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon uiNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip uiContextMenu;
        private System.Windows.Forms.ToolStripMenuItem uiContextSettings;
        private System.Windows.Forms.ToolStripMenuItem uiContextExit;
        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.ToolStripMenuItem uiContextCaptureCopy;
        private System.Windows.Forms.ToolStripMenuItem uiContextCaptureSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SaveFileDialog uiSaveAs;
        private System.Windows.Forms.ToolStripMenuItem uiContextCaptureSaveFolder;
        private System.Windows.Forms.ToolStripMenuItem uiContextStartFFXI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

