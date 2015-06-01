namespace SSI
{
    partial class RegisterWindow
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
            this.registerBtn = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.imageBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.browseBtn = new System.Windows.Forms.Button();
            this.selectImage = new System.Windows.Forms.OpenFileDialog();
            this.pwdBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pwdcheckBox = new System.Windows.Forms.TextBox();
            this.backBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // registerBtn
            // 
            this.registerBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.registerBtn.Location = new System.Drawing.Point(157, 243);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(75, 23);
            this.registerBtn.TabIndex = 0;
            this.registerBtn.Text = "Register";
            this.registerBtn.UseVisualStyleBackColor = true;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // nameBox
            // 
            this.nameBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nameBox.Location = new System.Drawing.Point(132, 103);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(100, 20);
            this.nameBox.TabIndex = 2;
            // 
            // emailBox
            // 
            this.emailBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.emailBox.Location = new System.Drawing.Point(132, 76);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(100, 20);
            this.emailBox.TabIndex = 3;
            // 
            // imageBox
            // 
            this.imageBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imageBox.Enabled = false;
            this.imageBox.Location = new System.Drawing.Point(132, 130);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(100, 20);
            this.imageBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name*";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Email*";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Image";
            // 
            // browseBtn
            // 
            this.browseBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.browseBtn.Location = new System.Drawing.Point(239, 130);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 9;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // selectImage
            // 
            this.selectImage.FileOk += new System.ComponentModel.CancelEventHandler(this.selectImage_FileOk);
            // 
            // pwdBox
            // 
            this.pwdBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pwdBox.Location = new System.Drawing.Point(132, 157);
            this.pwdBox.Name = "pwdBox";
            this.pwdBox.PasswordChar = '•';
            this.pwdBox.Size = new System.Drawing.Size(100, 20);
            this.pwdBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Password*";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password check*";
            // 
            // pwdcheckBox
            // 
            this.pwdcheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pwdcheckBox.Location = new System.Drawing.Point(132, 183);
            this.pwdcheckBox.Name = "pwdcheckBox";
            this.pwdcheckBox.PasswordChar = '•';
            this.pwdcheckBox.Size = new System.Drawing.Size(100, 20);
            this.pwdcheckBox.TabIndex = 13;
            // 
            // backBtn
            // 
            this.backBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.backBtn.Location = new System.Drawing.Point(310, 357);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(90, 23);
            this.backBtn.TabIndex = 14;
            this.backBtn.Text = "Back to login";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // RegisterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.pwdcheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pwdBox);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.registerBtn);
            this.Name = "RegisterWindow";
            this.Size = new System.Drawing.Size(412, 393);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.TextBox imageBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.OpenFileDialog selectImage;
        private System.Windows.Forms.TextBox pwdBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox pwdcheckBox;
        private System.Windows.Forms.Button backBtn;
    }
}
