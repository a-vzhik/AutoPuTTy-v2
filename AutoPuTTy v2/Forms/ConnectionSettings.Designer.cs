namespace AutoPuTTY.Forms
{
    partial class connectionSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbConnectionName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbRDP = new System.Windows.Forms.GroupBox();
            this.cbRDPSpan = new System.Windows.Forms.CheckBox();
            this.cbRDPDrivers = new System.Windows.Forms.CheckBox();
            this.cbRDPAdmin = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbRDPSize = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabSettings.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbRDP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabPage1);
            this.tabSettings.Controls.Add(this.tabPage2);
            this.tabSettings.Location = new System.Drawing.Point(12, 12);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(400, 213);
            this.tabSettings.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(496, 280);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Putty";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbConnectionName);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 198);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Putty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Connection name:";
            // 
            // tbConnectionName
            // 
            this.tbConnectionName.Enabled = false;
            this.tbConnectionName.Location = new System.Drawing.Point(134, 15);
            this.tbConnectionName.Name = "tbConnectionName";
            this.tbConnectionName.Size = new System.Drawing.Size(228, 22);
            this.tbConnectionName.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbRDP);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(392, 184);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Rdp";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbRDP
            // 
            this.gbRDP.Controls.Add(this.cbRDPSpan);
            this.gbRDP.Controls.Add(this.cbRDPDrivers);
            this.gbRDP.Controls.Add(this.cbRDPAdmin);
            this.gbRDP.Controls.Add(this.label8);
            this.gbRDP.Controls.Add(this.cbRDPSize);
            this.gbRDP.Location = new System.Drawing.Point(6, 6);
            this.gbRDP.Name = "gbRDP";
            this.gbRDP.Size = new System.Drawing.Size(384, 170);
            this.gbRDP.TabIndex = 4;
            this.gbRDP.TabStop = false;
            this.gbRDP.Text = "Rdp";
            // 
            // cbRDPSpan
            // 
            this.cbRDPSpan.AutoSize = true;
            this.cbRDPSpan.Location = new System.Drawing.Point(5, 135);
            this.cbRDPSpan.Name = "cbRDPSpan";
            this.cbRDPSpan.Size = new System.Drawing.Size(136, 21);
            this.cbRDPSpan.TabIndex = 14;
            this.cbRDPSpan.Text = "Multiple monitors";
            this.cbRDPSpan.UseVisualStyleBackColor = true;
            this.cbRDPSpan.CheckedChanged += new System.EventHandler(this.cbRDPSpan_CheckedChanged);
            // 
            // cbRDPDrivers
            // 
            this.cbRDPDrivers.AutoSize = true;
            this.cbRDPDrivers.Location = new System.Drawing.Point(5, 107);
            this.cbRDPDrivers.Name = "cbRDPDrivers";
            this.cbRDPDrivers.Size = new System.Drawing.Size(144, 21);
            this.cbRDPDrivers.TabIndex = 13;
            this.cbRDPDrivers.Text = "Mount local drives";
            this.cbRDPDrivers.UseVisualStyleBackColor = true;
            this.cbRDPDrivers.CheckedChanged += new System.EventHandler(this.cbRDPDrivers_CheckedChanged);
            // 
            // cbRDPAdmin
            // 
            this.cbRDPAdmin.AutoSize = true;
            this.cbRDPAdmin.Location = new System.Drawing.Point(6, 80);
            this.cbRDPAdmin.Name = "cbRDPAdmin";
            this.cbRDPAdmin.Size = new System.Drawing.Size(69, 21);
            this.cbRDPAdmin.TabIndex = 12;
            this.cbRDPAdmin.Text = "Admin";
            this.cbRDPAdmin.UseVisualStyleBackColor = true;
            this.cbRDPAdmin.CheckedChanged += new System.EventHandler(this.cbRDPAdmin_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "Screen SIze:";
            // 
            // cbRDPSize
            // 
            this.cbRDPSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRDPSize.FormattingEnabled = true;
            this.cbRDPSize.Items.AddRange(new object[] {
            "",
            "640x480",
            "800x600",
            "1024x768",
            "1152x864",
            "1280x720",
            "1280x960",
            "1280x1024",
            "1600x1200",
            "1680x1050",
            "1920x1080",
            "1920x1200",
            "Full screen"});
            this.cbRDPSize.Location = new System.Drawing.Point(5, 48);
            this.cbRDPSize.Margin = new System.Windows.Forms.Padding(4);
            this.cbRDPSize.Name = "cbRDPSize";
            this.cbRDPSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbRDPSize.Size = new System.Drawing.Size(372, 24);
            this.cbRDPSize.TabIndex = 10;
            this.cbRDPSize.SelectedIndexChanged += new System.EventHandler(this.cbRDPSize_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // connectionSettings
            // 
            this.ClientSize = new System.Drawing.Size(420, 287);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabSettings);
            this.Name = "connectionSettings";
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.connectionSettings_Load);
            this.tabSettings.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gbRDP.ResumeLayout(false);
            this.gbRDP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pOPuTTY;
        public System.Windows.Forms.Button bPuTTYExecute;
        private System.Windows.Forms.Label lSep8;
        public System.Windows.Forms.CheckBox cbPuTTYExecute;
        public System.Windows.Forms.TextBox tbPuTTYExecute;
        private System.Windows.Forms.Label lSep9;
        public System.Windows.Forms.CheckBox cbPuTTYKey;
        private System.Windows.Forms.Label lSep7;
        private System.Windows.Forms.Button bPuTTYKey;
        private System.Windows.Forms.Label lPuTTYPath;
        public System.Windows.Forms.TextBox tbPuTTYKey;
        private System.Windows.Forms.Button bPuTTYPath;
        public System.Windows.Forms.TextBox tbPuTTYPath;
        private System.Windows.Forms.Panel pORD;
        private System.Windows.Forms.Label lSep13;
        private System.Windows.Forms.Label lRDOther;
        private System.Windows.Forms.Label lSep12;
        private System.Windows.Forms.Label lRDSize;
        public System.Windows.Forms.ComboBox cbRDSize;
        private System.Windows.Forms.Label lRDKeep;
        public System.Windows.Forms.TextBox tbRDKeep;
        private System.Windows.Forms.Button bRDKeep;
        private System.Windows.Forms.Label lSep11;
        private System.Windows.Forms.Label lSep10;
        private System.Windows.Forms.TextBox tbRDPath;
        public System.Windows.Forms.CheckBox cbRDDrives;
        private System.Windows.Forms.Label lRDPath;
        private System.Windows.Forms.Button bRDPath;
        private System.Windows.Forms.Panel pOVNC;
        private System.Windows.Forms.Label lVNCOther;
        private System.Windows.Forms.Label lSep16;
        private System.Windows.Forms.Label lVNCFiles;
        public System.Windows.Forms.TextBox tbVNCKeep;
        private System.Windows.Forms.Button bVNCKeep;
        private System.Windows.Forms.Label lSep15;
        public System.Windows.Forms.CheckBox cbVNCViewonly;
        public System.Windows.Forms.CheckBox cbVNCFullscreen;
        private System.Windows.Forms.Label lSep14;
        private System.Windows.Forms.TextBox tbVNCPath;
        private System.Windows.Forms.Label lVNCPath;
        private System.Windows.Forms.Button bVNCPath;
        private System.Windows.Forms.Panel pOWinSCP;
        private System.Windows.Forms.Label lSep18;
        public System.Windows.Forms.CheckBox cbWSCPKey;
        private System.Windows.Forms.Label lSep17;
        private System.Windows.Forms.Button bWSCPKey;
        private System.Windows.Forms.Label lWSCPPath;
        public System.Windows.Forms.TextBox tbWSCPKey;
        private System.Windows.Forms.Button bWSCPPath;
        private System.Windows.Forms.TextBox tbWSCPPath;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPuTTY;
        private System.Windows.Forms.TabPage tabRD;
        private System.Windows.Forms.TabPage tabVNC;
        private System.Windows.Forms.TabPage tabWSCP;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.Panel pOGeneral;
        private System.Windows.Forms.LinkLabel liGImport;
        public System.Windows.Forms.CheckBox cbGSkip;
        public System.Windows.Forms.CheckBox cbGReplace;
        private System.Windows.Forms.Label lGImport;
        private System.Windows.Forms.Button bGImport;
        private System.Windows.Forms.CheckBox cbGPassword;
        private System.Windows.Forms.TableLayoutPanel tpGPass;
        private System.Windows.Forms.Panel pGPassword;
        private System.Windows.Forms.Label lSep1;
        private System.Windows.Forms.TextBox tbGPassword;
        private System.Windows.Forms.Panel pGConfirm;
        private System.Windows.Forms.TextBox tbGConfirm;
        private System.Windows.Forms.Label lSep2;
        private System.Windows.Forms.Label lGConfirm;
        private System.Windows.Forms.Panel pGApply;
        private System.Windows.Forms.Button bGPassword;
        private System.Windows.Forms.Label lSep3;
        private System.Windows.Forms.Label lSep6;
        private System.Windows.Forms.Label lGOther;
        public System.Windows.Forms.CheckBox cbGMinimize;
        public System.ComponentModel.BackgroundWorker bwProgress;
        private System.Windows.Forms.Button bOK;
        public System.Windows.Forms.CheckBox cbRDAdmin;
        public System.Windows.Forms.CheckBox cbPuTTYForward;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox cbRDSpan;
        private System.Windows.Forms.Label lSep19;
        private System.Windows.Forms.Label lWSCPOther;
        public System.Windows.Forms.CheckBox cbWSCPPassive;
        private System.Windows.Forms.Label lSep5;
        private System.Windows.Forms.TabPage tabNetCat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bNCPath;
        private System.Windows.Forms.TextBox tbNCPath;
        private System.Windows.Forms.Label lNCPath;
        public System.Windows.Forms.Button bNCCommand;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox cbNCExecuteCommand;
        public System.Windows.Forms.TextBox tbNCCommand;
        private System.Windows.Forms.Label lPlinkPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bPlinkPath;
        public System.Windows.Forms.TextBox tbPlinkPath;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbConnectionName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbRDP;
        private System.Windows.Forms.ComboBox cbRDPSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbRDPAdmin;
        private System.Windows.Forms.CheckBox cbRDPDrivers;
        private System.Windows.Forms.CheckBox cbRDPSpan;
        private System.Windows.Forms.Button button1;
    }
}

