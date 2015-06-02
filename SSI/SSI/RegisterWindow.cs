using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SSI
{
    public partial class RegisterWindow : UserControl
    {
        bool imageLoaded = false;
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void selectImage_FileOk(object sender, CancelEventArgs e)
        {
            imageLoaded = true;
            imageBox.Text = selectImage.FileName;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            selectImage.ShowDialog();
            
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            bool ok=true;
            
            if (nameBox.Text != null & emailBox.Text != null & pwdBox.Text!=null)
            {
                if (pwdBox.Text.Length<6)
                {
                    MessageBox.Show("Password should be at least 6 characters");
                    ok = false;
                }
                if (pwdBox.Text != pwdcheckBox.Text)
                {
                    MessageBox.Show("Passwords don't match");
                    ok = false;
                }
                if (nameBox.TextLength < 3)
                {
                    MessageBox.Show("Please enter a valid name");
                    ok = false;
                }
                if (!emailBox.Text.Contains('@') || !emailBox.Text.Contains('.'))
                {
                    MessageBox.Show("Please enter a valid email");
                    ok = false;
                }
                if (ok)
                {
                    DatabaseLink db = new DatabaseLink();
                    PasswordHash ph = new PasswordHash();
                    string hashedPw = ph.hashPassword(pwdBox.Text);
                    if (imageLoaded)
                    {
                        Image img = null;
                        img = Image.FromFile(imageBox.Text);
                        MessageBox.Show(db.InsertUser(" ", emailBox.Text, hashedPw, nameBox.Text, db.ImageToBase64(img, img.RawFormat)));
                            
                    }
                    else
                        if (db.InsertUser(" ", emailBox.Text, hashedPw, nameBox.Text) != "Success")
                            MessageBox.Show(db.error);
                        else
                            MessageBox.Show("Success");
                        
                }
            }
            else
                MessageBox.Show("The fields with * must be filled");
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm.registOk = true;
        }
    }
}
