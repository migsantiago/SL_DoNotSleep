namespace SL_DoNotSleep
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ntfIconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkEnableSleep = new System.Windows.Forms.CheckBox();
            this.tmrGetStatus = new System.Windows.Forms.Timer(this.components);
            this.pctCurrentStatus = new System.Windows.Forms.PictureBox();
            this.chkRunOnStartup = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // ntfIconTray
            // 
            this.ntfIconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfIconTray.Icon")));
            this.ntfIconTray.Text = "SL_DoNotSleep";
            this.ntfIconTray.Visible = true;
            this.ntfIconTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ntfIconTray_MouseDoubleClick);
            // 
            // chkEnableSleep
            // 
            this.chkEnableSleep.AutoSize = true;
            this.chkEnableSleep.Location = new System.Drawing.Point(69, 82);
            this.chkEnableSleep.Name = "chkEnableSleep";
            this.chkEnableSleep.Size = new System.Drawing.Size(87, 17);
            this.chkEnableSleep.TabIndex = 0;
            this.chkEnableSleep.Text = "Enable sleep";
            this.chkEnableSleep.UseVisualStyleBackColor = true;
            this.chkEnableSleep.CheckedChanged += new System.EventHandler(this.chkEnableSleep_CheckedChanged);
            // 
            // tmrGetStatus
            // 
            this.tmrGetStatus.Enabled = true;
            this.tmrGetStatus.Interval = 1000;
            this.tmrGetStatus.Tick += new System.EventHandler(this.tmrGetStatus_Tick);
            // 
            // pctCurrentStatus
            // 
            this.pctCurrentStatus.Location = new System.Drawing.Point(80, 12);
            this.pctCurrentStatus.Name = "pctCurrentStatus";
            this.pctCurrentStatus.Size = new System.Drawing.Size(64, 64);
            this.pctCurrentStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctCurrentStatus.TabIndex = 2;
            this.pctCurrentStatus.TabStop = false;
            // 
            // chkRunOnStartup
            // 
            this.chkRunOnStartup.AutoSize = true;
            this.chkRunOnStartup.Location = new System.Drawing.Point(69, 105);
            this.chkRunOnStartup.Name = "chkRunOnStartup";
            this.chkRunOnStartup.Size = new System.Drawing.Size(96, 17);
            this.chkRunOnStartup.TabIndex = 3;
            this.chkRunOnStartup.Text = "Run on startup";
            this.chkRunOnStartup.UseVisualStyleBackColor = true;
            this.chkRunOnStartup.CheckedChanged += new System.EventHandler(this.chkRunOnStartup_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 127);
            this.Controls.Add(this.chkRunOnStartup);
            this.Controls.Add(this.pctCurrentStatus);
            this.Controls.Add(this.chkEnableSleep);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize_1);
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon ntfIconTray;
        private System.Windows.Forms.CheckBox chkEnableSleep;
        private System.Windows.Forms.Timer tmrGetStatus;
        private System.Windows.Forms.PictureBox pctCurrentStatus;
        private System.Windows.Forms.CheckBox chkRunOnStartup;
    }
}

