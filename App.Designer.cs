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
            this.uiContextSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextExit = new System.Windows.Forms.ToolStripMenuItem();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.uiContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiNotifyIcon
            // 
            this.uiNotifyIcon.ContextMenuStrip = this.uiContextMenu;
            this.uiNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("uiNotifyIcon.Icon")));
            this.uiNotifyIcon.Text = "FFXI Gamma";
            this.uiNotifyIcon.Visible = true;
            this.uiNotifyIcon.DoubleClick += new System.EventHandler(this.uiNotifyIcon_DoubleClick);
            // 
            // uiContextMenu
            // 
            this.uiContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiContextSettings,
            this.uiContextExit});
            this.uiContextMenu.Name = "uiContextMenu";
            this.uiContextMenu.Size = new System.Drawing.Size(117, 48);
            // 
            // uiContextSettings
            // 
            this.uiContextSettings.Name = "uiContextSettings";
            this.uiContextSettings.Size = new System.Drawing.Size(116, 22);
            this.uiContextSettings.Text = "設定(&S)";
            this.uiContextSettings.Click += new System.EventHandler(this.uiSettings_Click);
            // 
            // uiContextExit
            // 
            this.uiContextExit.Name = "uiContextExit";
            this.uiContextExit.Size = new System.Drawing.Size(116, 22);
            this.uiContextExit.Text = "終了(&X)";
            this.uiContextExit.Click += new System.EventHandler(this.uiExit_Click);
            // 
            // uiTimer
            // 
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
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
    }
}

