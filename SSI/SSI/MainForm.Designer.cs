namespace SSI
{
    partial class MainForm
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.entryBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.monthBox = new System.Windows.Forms.ComboBox();
            this.yearBox = new System.Windows.Forms.NumericUpDown();
            this.notifIcn = new System.Windows.Forms.NotifyIcon(this.components);
            this.registTimer = new System.Windows.Forms.Timer(this.components);
            this.registerBtn = new System.Windows.Forms.Button();
            this.loginSSIBtn = new System.Windows.Forms.Button();
            this.userPhoto = new System.Windows.Forms.PictureBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.saveToDb = new System.Windows.Forms.Button();
            this.entryImage = new System.Windows.Forms.PictureBox();
            this.registerWindow1 = new SSI.RegisterWindow();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryImage)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 23);
            this.label1.TabIndex = 1;
            // 
            // entryBox
            // 
            this.entryBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.entryBox.Location = new System.Drawing.Point(3, 16);
            this.entryBox.Name = "entryBox";
            this.entryBox.Size = new System.Drawing.Size(408, 183);
            this.entryBox.TabIndex = 6;
            this.entryBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dateLabel);
            this.groupBox1.Controls.Add(this.saveToDb);
            this.groupBox1.Controls.Add(this.entryImage);
            this.groupBox1.Controls.Add(this.entryBox);
            this.groupBox1.Location = new System.Drawing.Point(254, 12);
            this.groupBox1.MinimumSize = new System.Drawing.Size(288, 296);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 342);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Day Review";
            this.groupBox1.Visible = false;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(170, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(35, 13);
            this.dateLabel.TabIndex = 12;
            this.dateLabel.Text = "label2";
            this.dateLabel.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // monthBox
            // 
            this.monthBox.FormattingEnabled = true;
            this.monthBox.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.monthBox.Location = new System.Drawing.Point(176, 18);
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(121, 21);
            this.monthBox.TabIndex = 9;
            this.monthBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // yearBox
            // 
            this.yearBox.Location = new System.Drawing.Point(304, 18);
            this.yearBox.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(52, 20);
            this.yearBox.TabIndex = 10;
            this.yearBox.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.yearBox.Visible = false;
            this.yearBox.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // notifIcn
            // 
            this.notifIcn.Text = "SSI Notification";
            this.notifIcn.Visible = true;
            // 
            // registTimer
            // 
            this.registTimer.Tick += new System.EventHandler(this.registTimer_Tick);
            // 
            // registerBtn
            // 
            this.registerBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.registerBtn.BackColor = System.Drawing.Color.Transparent;
            this.registerBtn.BackgroundImage = global::SSI.Properties.Resources.register;
            this.registerBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.registerBtn.FlatAppearance.BorderSize = 0;
            this.registerBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.registerBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.registerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerBtn.Location = new System.Drawing.Point(225, 249);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(216, 68);
            this.registerBtn.TabIndex = 14;
            this.registerBtn.UseVisualStyleBackColor = false;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // loginSSIBtn
            // 
            this.loginSSIBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.loginSSIBtn.BackColor = System.Drawing.Color.Transparent;
            this.loginSSIBtn.BackgroundImage = global::SSI.Properties.Resources.LoginSSI;
            this.loginSSIBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loginSSIBtn.FlatAppearance.BorderSize = 0;
            this.loginSSIBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.loginSSIBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.loginSSIBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginSSIBtn.Location = new System.Drawing.Point(216, 182);
            this.loginSSIBtn.Name = "loginSSIBtn";
            this.loginSSIBtn.Size = new System.Drawing.Size(216, 61);
            this.loginSSIBtn.TabIndex = 12;
            this.loginSSIBtn.UseVisualStyleBackColor = false;
            // 
            // userPhoto
            // 
            this.userPhoto.Location = new System.Drawing.Point(12, 44);
            this.userPhoto.Name = "userPhoto";
            this.userPhoto.Size = new System.Drawing.Size(156, 156);
            this.userPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userPhoto.TabIndex = 0;
            this.userPhoto.TabStop = false;
            this.userPhoto.Visible = false;
            // 
            // loginBtn
            // 
            this.loginBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.loginBtn.BackColor = System.Drawing.Color.Transparent;
            this.loginBtn.BackgroundImage = global::SSI.Properties.Resources.login;
            this.loginBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loginBtn.FlatAppearance.BorderSize = 0;
            this.loginBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.loginBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.loginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginBtn.Location = new System.Drawing.Point(191, 115);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(269, 61);
            this.loginBtn.TabIndex = 2;
            this.loginBtn.UseVisualStyleBackColor = false;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.logoutBtn.BackgroundImage = global::SSI.Properties.Resources.logout21;
            this.logoutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logoutBtn.FlatAppearance.BorderSize = 0;
            this.logoutBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.logoutBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.logoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutBtn.Location = new System.Drawing.Point(568, 360);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(100, 46);
            this.logoutBtn.TabIndex = 4;
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // saveToDb
            // 
            this.saveToDb.BackgroundImage = global::SSI.Properties.Resources.Forward_arrow_button_next_right_media_mail;
            this.saveToDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveToDb.Location = new System.Drawing.Point(372, 159);
            this.saveToDb.Name = "saveToDb";
            this.saveToDb.Size = new System.Drawing.Size(39, 40);
            this.saveToDb.TabIndex = 11;
            this.saveToDb.UseVisualStyleBackColor = true;
            this.saveToDb.Click += new System.EventHandler(this.saveToDb_Click);
            // 
            // entryImage
            // 
            this.entryImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.entryImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entryImage.Image = global::SSI.Properties.Resources.clickheretoselect;
            this.entryImage.Location = new System.Drawing.Point(3, 199);
            this.entryImage.Name = "entryImage";
            this.entryImage.Size = new System.Drawing.Size(408, 140);
            this.entryImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.entryImage.TabIndex = 7;
            this.entryImage.TabStop = false;
            this.entryImage.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // registerWindow1
            // 
            this.registerWindow1.BackColor = System.Drawing.Color.ForestGreen;
            this.registerWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registerWindow1.Location = new System.Drawing.Point(0, 0);
            this.registerWindow1.Name = "registerWindow1";
            this.registerWindow1.Size = new System.Drawing.Size(680, 418);
            this.registerWindow1.TabIndex = 13;
            this.registerWindow1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 418);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.loginSSIBtn);
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.monthBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userPhoto);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.registerWindow1);
            this.MinimumSize = new System.Drawing.Size(441, 278);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox userPhoto;
        private System.Windows.Forms.RichTextBox entryBox;
        private System.Windows.Forms.PictureBox entryImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox monthBox;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.Button saveToDb;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.NotifyIcon notifIcn;
        private System.Windows.Forms.Button loginSSIBtn;
        private RegisterWindow registerWindow1;
        private System.Windows.Forms.Timer registTimer;
        private System.Windows.Forms.Button registerBtn;
    }
}