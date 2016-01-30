namespace ffxigamma {
    partial class VolumeIndicator {
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.speakerLevel = new ffxigamma.SpeakerLevel();
            this.speakerIcon = new ffxigamma.SpeakerIcon();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // speakerLevel
            // 
            this.speakerLevel.Location = new System.Drawing.Point(37, 18);
            this.speakerLevel.Name = "speakerLevel";
            this.speakerLevel.Size = new System.Drawing.Size(200, 12);
            this.speakerLevel.TabIndex = 1;
            // 
            // speakerIcon
            // 
            this.speakerIcon.Location = new System.Drawing.Point(12, 12);
            this.speakerIcon.Name = "speakerIcon";
            this.speakerIcon.Size = new System.Drawing.Size(22, 22);
            this.speakerIcon.TabIndex = 0;
            // 
            // VolumeIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(254, 47);
            this.Controls.Add(this.speakerLevel);
            this.Controls.Add(this.speakerIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VolumeIndicator";
            this.ShowInTaskbar = false;
            this.Text = "VolumeIndicator";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.ResumeLayout(false);

        }

        #endregion

        private SpeakerIcon speakerIcon;
        private SpeakerLevel speakerLevel;
        private System.Windows.Forms.Timer timer;
    }
}