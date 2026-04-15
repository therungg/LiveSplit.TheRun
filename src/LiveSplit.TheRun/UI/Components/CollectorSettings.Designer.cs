namespace LiveSplit.UI.Components
{
    partial class CollectorSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lnkUploadKey = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnTest = new System.Windows.Forms.Button();
			this.picUser = new System.Windows.Forms.PictureBox();
			this.lnkUsername = new System.Windows.Forms.LinkLabel();
			this.lnkLive = new System.Windows.Forms.LinkLabel();
			this.chkStatsUploadEnabled = new System.Windows.Forms.CheckBox();
			this.chkUploadOnReset = new System.Windows.Forms.CheckBox();
			this.chkLiveTrackingEnabled = new System.Windows.Forms.CheckBox();
			this.chkToastEnabled = new System.Windows.Forms.CheckBox();
			this.chkLayoutPathUpload = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip();
			((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			//
			// tableLayoutPanel1
			//
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.Controls.Add(this.lblDescription, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lnkUploadKey, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtPath, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnTest, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.picUser, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.lnkUsername, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.lnkLive, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.chkStatsUploadEnabled, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.chkUploadOnReset, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.chkLiveTrackingEnabled, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.chkToastEnabled, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.chkLayoutPathUpload, 0, 8);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 7);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 9;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(462, 291);
			this.tableLayoutPanel1.TabIndex = 0;
			//
			// lblDescription
			//
			this.tableLayoutPanel1.SetColumnSpan(this.lblDescription, 3);
			this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDescription.Location = new System.Drawing.Point(3, 0);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(456, 80);
			this.lblDescription.TabIndex = 9;
			this.lblDescription.Text = "therun.gg syncs your splits automatically and shows your live runs alongside " +
				"thousands of other runners. Get advanced stats on your profile, race against " +
				"others, participate in tournaments, and receive a yearly recap of your speedrunning journey.";
			//
			// lnkUploadKey
			//
			this.tableLayoutPanel1.SetColumnSpan(this.lnkUploadKey, 3);
			this.lnkUploadKey.AutoSize = true;
			this.lnkUploadKey.Location = new System.Drawing.Point(3, 80);
			this.lnkUploadKey.Name = "lnkUploadKey";
			this.lnkUploadKey.Size = new System.Drawing.Size(250, 13);
			this.lnkUploadKey.TabIndex = 10;
			this.lnkUploadKey.TabStop = true;
			this.lnkUploadKey.Text = "Get your upload key at therun.gg/livesplit";
			this.lnkUploadKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUploadKey_LinkClicked);
			//
			// label1
			//
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 110);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(133, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Upload Key";
			//
			// txtPath
			//
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(142, 107);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(271, 20);
			this.txtPath.TabIndex = 2;
			this.txtPath.UseSystemPasswordChar = true;
			this.txtPath.Leave += new System.EventHandler(this.txtPath_Leave);
			//
			// btnTest
			//
			this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTest.Location = new System.Drawing.Point(419, 105);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(54, 23);
			this.btnTest.TabIndex = 5;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			//
			// picUser
			//
			this.picUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.picUser.Location = new System.Drawing.Point(107, 134);
			this.picUser.Name = "picUser";
			this.picUser.Size = new System.Drawing.Size(29, 29);
			this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picUser.TabIndex = 6;
			this.picUser.TabStop = false;
			this.picUser.Visible = false;
			//
			// lnkUsername
			//
			this.lnkUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lnkUsername.AutoSize = true;
			this.lnkUsername.Location = new System.Drawing.Point(142, 142);
			this.lnkUsername.Name = "lnkUsername";
			this.lnkUsername.Size = new System.Drawing.Size(0, 13);
			this.lnkUsername.TabIndex = 7;
			this.lnkUsername.Visible = false;
			this.lnkUsername.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUsername_LinkClicked);
			//
			// lnkLive
			//
			this.lnkLive.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lnkLive.AutoSize = true;
			this.lnkLive.Location = new System.Drawing.Point(419, 142);
			this.lnkLive.Name = "lnkLive";
			this.lnkLive.Size = new System.Drawing.Size(28, 13);
			this.lnkLive.TabIndex = 12;
			this.lnkLive.TabStop = true;
			this.lnkLive.Text = "Live";
			this.lnkLive.Visible = false;
			this.lnkLive.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLive_LinkClicked);
			//
			// chkStatsUploadEnabled
			//
			this.tableLayoutPanel1.SetColumnSpan(this.chkStatsUploadEnabled, 3);
			this.chkStatsUploadEnabled.AutoSize = true;
			this.chkStatsUploadEnabled.Checked = true;
			this.chkStatsUploadEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatsUploadEnabled.Location = new System.Drawing.Point(3, 169);
			this.chkStatsUploadEnabled.Name = "chkStatsUploadEnabled";
			this.chkStatsUploadEnabled.Size = new System.Drawing.Size(123, 17);
			this.chkStatsUploadEnabled.TabIndex = 3;
			this.chkStatsUploadEnabled.Text = "Enable Stats Sync";
			this.chkStatsUploadEnabled.UseVisualStyleBackColor = true;
			//
			// chkUploadOnReset
			//
			this.tableLayoutPanel1.SetColumnSpan(this.chkUploadOnReset, 3);
			this.chkUploadOnReset.AutoSize = true;
			this.chkUploadOnReset.Checked = true;
			this.chkUploadOnReset.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUploadOnReset.Location = new System.Drawing.Point(3, 194);
			this.chkUploadOnReset.Name = "chkUploadOnReset";
			this.chkUploadOnReset.Size = new System.Drawing.Size(110, 17);
			this.chkUploadOnReset.TabIndex = 8;
			this.chkUploadOnReset.Text = "Sync on Reset";
			this.chkUploadOnReset.UseVisualStyleBackColor = true;
			//
			// chkLiveTrackingEnabled
			//
			this.tableLayoutPanel1.SetColumnSpan(this.chkLiveTrackingEnabled, 3);
			this.chkLiveTrackingEnabled.AutoSize = true;
			this.chkLiveTrackingEnabled.Checked = true;
			this.chkLiveTrackingEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLiveTrackingEnabled.Location = new System.Drawing.Point(3, 219);
			this.chkLiveTrackingEnabled.Name = "chkLiveTrackingEnabled";
			this.chkLiveTrackingEnabled.Size = new System.Drawing.Size(127, 17);
			this.chkLiveTrackingEnabled.TabIndex = 4;
			this.chkLiveTrackingEnabled.Text = "Enable Live Tracking";
			this.chkLiveTrackingEnabled.UseVisualStyleBackColor = true;
			//
			// chkToastEnabled
			//
			this.tableLayoutPanel1.SetColumnSpan(this.chkToastEnabled, 3);
			this.chkToastEnabled.AutoSize = true;
			this.chkToastEnabled.Checked = true;
			this.chkToastEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkToastEnabled.Location = new System.Drawing.Point(3, 244);
			this.chkToastEnabled.Name = "chkToastEnabled";
			this.chkToastEnabled.Size = new System.Drawing.Size(155, 17);
			this.chkToastEnabled.TabIndex = 13;
			this.chkToastEnabled.Text = "Show Toast Notifications";
			this.chkToastEnabled.UseVisualStyleBackColor = true;
			//
			// chkLayoutPathUpload
			//
			this.tableLayoutPanel1.SetColumnSpan(this.chkLayoutPathUpload, 3);
			this.chkLayoutPathUpload.AutoSize = true;
			this.chkLayoutPathUpload.Location = new System.Drawing.Point(3, 269);
			this.chkLayoutPathUpload.Name = "chkLayoutPathUpload";
			this.chkLayoutPathUpload.Size = new System.Drawing.Size(140, 17);
			this.chkLayoutPathUpload.TabIndex = 14;
			this.chkLayoutPathUpload.Text = "Upload Layout Path";
			this.chkLayoutPathUpload.UseVisualStyleBackColor = true;
			//
			// toolTip
			//
			this.toolTip.AutoPopDelay = 10000;
			this.toolTip.InitialDelay = 300;
			this.toolTip.ReshowDelay = 100;
			this.toolTip.SetToolTip(this.chkStatsUploadEnabled, "Syncs your splits file to therun.gg after each completed run, on reset, and when closing LiveSplit.\nThis powers your profile stats, personal bests, and history.");
			this.toolTip.SetToolTip(this.chkUploadOnReset, "When enabled, stats are synced every time you reset the timer.\nWhen disabled, stats are only synced after a completed run or when closing LiveSplit.");
			this.toolTip.SetToolTip(this.chkLiveTrackingEnabled, "Sends real-time split data to therun.gg so your current run appears on the live page.\nViewers and other runners can follow your progress as it happens.");
			this.toolTip.SetToolTip(this.chkToastEnabled, "Shows a small notification when stats are being synced or when sync completes.");
			this.toolTip.SetToolTip(this.chkLayoutPathUpload, "Includes the layout file path in the uploaded splits file.\nDisabled by default because the path may contain personal information such as your Windows username.\nIf disabled, you will need to reload your layout when re-downloading your splits from therun.gg.");
			//
			// CollectorSettings
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "CollectorSettings";
			this.Padding = new System.Windows.Forms.Padding(7);
			this.Size = new System.Drawing.Size(476, 305);
			((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.LinkLabel lnkUploadKey;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.LinkLabel lnkUsername;
        private System.Windows.Forms.LinkLabel lnkLive;
        private System.Windows.Forms.CheckBox chkStatsUploadEnabled;
        private System.Windows.Forms.CheckBox chkUploadOnReset;
        private System.Windows.Forms.CheckBox chkLiveTrackingEnabled;
        private System.Windows.Forms.CheckBox chkToastEnabled;
        private System.Windows.Forms.CheckBox chkLayoutPathUpload;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
