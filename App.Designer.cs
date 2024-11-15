﻿namespace ffxigamma {
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
            this.uiNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.uiContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uiContextStartFFXI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.uiContextSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextOption = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.uiContextRestartAdminMode = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextRestartUserMode = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.uiContextMute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.uiContextCaptureCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextCaptureSaveFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.uiContextCaptureSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.uiSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.globalKeyReader = new ffxigamma.GlobalKeyReader(this.components);
            this.windowMonitor = new ffxigamma.WindowMonitor(this.components);
            this.remoteControl = new ffxigamma.RemoteControl(this.components);
            this.uiContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiNotifyIcon
            // 
            this.uiNotifyIcon.ContextMenuStrip = this.uiContextMenu;
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
            this.uiContextSettings,
            this.toolStripSeparator3,
            this.uiContextMute,
            this.toolStripSeparator4,
            this.uiContextCaptureCopy,
            this.uiContextCaptureSaveFolder,
            this.uiContextCaptureSaveAs});
            this.uiContextMenu.Name = "uiContextMenu";
            this.uiContextMenu.Size = new System.Drawing.Size(231, 154);
            this.uiContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.uiContextMenu_Opening);
            // 
            // uiContextStartFFXI
            // 
            this.uiContextStartFFXI.Name = "uiContextStartFFXI";
            this.uiContextStartFFXI.Size = new System.Drawing.Size(230, 22);
            this.uiContextStartFFXI.Text = "FAINAL FANTASY XI を起動(&F)";
            this.uiContextStartFFXI.Click += new System.EventHandler(this.uiContextStartFFXI_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(227, 6);
            // 
            // uiContextSettings
            // 
            this.uiContextSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiContextOption,
            this.toolStripSeparator1,
            this.uiContextRestartAdminMode,
            this.uiContextRestartUserMode,
            this.uiContextExit});
            this.uiContextSettings.Name = "uiContextSettings";
            this.uiContextSettings.Size = new System.Drawing.Size(230, 22);
            this.uiContextSettings.Text = "設定(&O)";
            // 
            // uiContextOption
            // 
            this.uiContextOption.Name = "uiContextOption";
            this.uiContextOption.Size = new System.Drawing.Size(197, 22);
            this.uiContextOption.Text = "オプション(&O)...";
            this.uiContextOption.Click += new System.EventHandler(this.uiContextOption_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // uiContextRestartAdminMode
            // 
            this.uiContextRestartAdminMode.Name = "uiContextRestartAdminMode";
            this.uiContextRestartAdminMode.Size = new System.Drawing.Size(197, 22);
            this.uiContextRestartAdminMode.Text = "管理者モードで再起動(&R)";
            this.uiContextRestartAdminMode.Click += new System.EventHandler(this.uiContextRestartAdminMode_Click);
            // 
            // uiContextRestartUserMode
            // 
            this.uiContextRestartUserMode.Name = "uiContextRestartUserMode";
            this.uiContextRestartUserMode.Size = new System.Drawing.Size(197, 22);
            this.uiContextRestartUserMode.Text = "ユーザーモードで再起動(&U)";
            this.uiContextRestartUserMode.Click += new System.EventHandler(this.uiContextRestartUserMode_Click);
            // 
            // uiContextExit
            // 
            this.uiContextExit.Name = "uiContextExit";
            this.uiContextExit.Size = new System.Drawing.Size(197, 22);
            this.uiContextExit.Text = "終了(&X)";
            this.uiContextExit.Click += new System.EventHandler(this.uiContextExit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(227, 6);
            // 
            // uiContextMute
            // 
            this.uiContextMute.Name = "uiContextMute";
            this.uiContextMute.Size = new System.Drawing.Size(230, 22);
            this.uiContextMute.Text = "ミュート(&M)";
            this.uiContextMute.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uiContextMute_DropDownItemClicked);
            this.uiContextMute.Click += new System.EventHandler(this.uiContextMute_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(227, 6);
            // 
            // uiContextCaptureCopy
            // 
            this.uiContextCaptureCopy.Name = "uiContextCaptureCopy";
            this.uiContextCaptureCopy.Size = new System.Drawing.Size(230, 22);
            this.uiContextCaptureCopy.Text = "画像をコピー(&C)";
            this.uiContextCaptureCopy.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uiContextCaptureCopy_DropDownItemClicked);
            this.uiContextCaptureCopy.Click += new System.EventHandler(this.uiContextCaptureCopy_Click);
            // 
            // uiContextCaptureSaveFolder
            // 
            this.uiContextCaptureSaveFolder.Name = "uiContextCaptureSaveFolder";
            this.uiContextCaptureSaveFolder.Size = new System.Drawing.Size(230, 22);
            this.uiContextCaptureSaveFolder.Text = "画像をフォルダーに保存(&S)";
            this.uiContextCaptureSaveFolder.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uiContextCaptureSaveFolder_DropDownItemClicked);
            this.uiContextCaptureSaveFolder.Click += new System.EventHandler(this.uiContextCaptureSaveFolder_Click);
            // 
            // uiContextCaptureSaveAs
            // 
            this.uiContextCaptureSaveAs.Name = "uiContextCaptureSaveAs";
            this.uiContextCaptureSaveAs.Size = new System.Drawing.Size(230, 22);
            this.uiContextCaptureSaveAs.Text = "名前を付けて画像を保存(&A)...";
            this.uiContextCaptureSaveAs.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uiContextCaptureSaveAs_DropDownItemClicked);
            this.uiContextCaptureSaveAs.Click += new System.EventHandler(this.uiContextCaptureSaveAs_Click);
            // 
            // uiSaveAs
            // 
            this.uiSaveAs.Filter = "PNG イメージ|*.png|JPEG イメージ|*.jpg|すべてのファイル|*";
            this.uiSaveAs.Title = "画像を保存 - FFXI Gamma";
            // 
            // globalKeyReader
            // 
            this.globalKeyReader.GlobalKeyDown += new ffxigamma.GlobalKeyEventHandler(this.globalKeyReader_GlobalKeyDown);
            // 
            // windowMonitor
            // 
            this.windowMonitor.WindowOpened += new ffxigamma.WindowMonitorEventHandler(this.windowMonitor_WindowOpend);
            this.windowMonitor.WindowClosed += new ffxigamma.WindowMonitorEventHandler(this.windowMonitor_WindowClosed);
            this.windowMonitor.WindowUpdate += new ffxigamma.WindowMonitorEventHandler(this.windowMonitor_WindowUpdate);
            // 
            // remoteControl
            // 
            this.remoteControl.CommandReceived += new ffxigamma.RemoteControlEventHandler(this.RemoteControl_CommandReceived);
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
        private System.Windows.Forms.ToolStripMenuItem uiContextCaptureCopy;
        private System.Windows.Forms.ToolStripMenuItem uiContextCaptureSaveAs;
        private System.Windows.Forms.SaveFileDialog uiSaveAs;
        private System.Windows.Forms.ToolStripMenuItem uiContextCaptureSaveFolder;
        private System.Windows.Forms.ToolStripMenuItem uiContextStartFFXI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem uiContextSettings;
        private System.Windows.Forms.ToolStripMenuItem uiContextOption;
        private System.Windows.Forms.ToolStripMenuItem uiContextRestartAdminMode;
        private System.Windows.Forms.ToolStripMenuItem uiContextExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem uiContextRestartUserMode;
        private GlobalKeyReader globalKeyReader;
        private WindowMonitor windowMonitor;
        private System.Windows.Forms.ToolStripMenuItem uiContextMute;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private RemoteControl remoteControl;
    }
}

