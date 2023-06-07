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
			this.txtPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkStatsUploadEnabled = new System.Windows.Forms.CheckBox();
			this.chkLiveTrackingEnabled = new System.Windows.Forms.CheckBox();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.buttonSelectFile = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.txtPath, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.chkLiveTrackingEnabled, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.chkStatsUploadEnabled, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtFile, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.buttonSelectFile, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(476, 141);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(142, 7);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(331, 20);
			this.txtPath.TabIndex = 2;
			this.txtPath.UseSystemPasswordChar = true;
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(133, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Upload Key";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// chkStatsUploadEnabled
			// 
			this.chkStatsUploadEnabled.AutoSize = true;
			this.chkStatsUploadEnabled.Checked = true;
			this.chkStatsUploadEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatsUploadEnabled.Location = new System.Drawing.Point(3, 66);
			this.chkStatsUploadEnabled.Name = "chkStatsUploadEnabled";
			this.chkStatsUploadEnabled.Size = new System.Drawing.Size(123, 17);
			this.chkStatsUploadEnabled.TabIndex = 3;
			this.chkStatsUploadEnabled.Text = "Enable Stats Upload";
			this.chkStatsUploadEnabled.UseVisualStyleBackColor = true;
			// 
			// chkLiveTrackingEnabled
			// 
			this.chkLiveTrackingEnabled.AutoSize = true;
			this.chkLiveTrackingEnabled.Checked = true;
			this.chkLiveTrackingEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLiveTrackingEnabled.Location = new System.Drawing.Point(3, 99);
			this.chkLiveTrackingEnabled.Name = "chkLiveTrackingEnabled";
			this.chkLiveTrackingEnabled.Size = new System.Drawing.Size(127, 17);
			this.chkLiveTrackingEnabled.TabIndex = 4;
			this.chkLiveTrackingEnabled.Text = "Enable Live Tracking";
			this.chkLiveTrackingEnabled.UseVisualStyleBackColor = true;
			// 
			// txtFile
			// 
			this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFile.Location = new System.Drawing.Point(142, 38);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(331, 20);
			this.txtFile.TabIndex = 6;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSelectFile.Location = new System.Drawing.Point(3, 37);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(133, 23);
			this.buttonSelectFile.TabIndex = 7;
			this.buttonSelectFile.Text = "OR Read Key from File";
			this.buttonSelectFile.UseVisualStyleBackColor = true;
			this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
			// 
			// CollectorSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "CollectorSettings";
			this.Padding = new System.Windows.Forms.Padding(7);
			this.Size = new System.Drawing.Size(476, 141);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.CheckBox chkLiveTrackingEnabled;
		private System.Windows.Forms.CheckBox chkStatsUploadEnabled;
		public System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.Button buttonSelectFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}
