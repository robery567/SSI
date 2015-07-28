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
    public partial class LoginWindow : UserControl
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseLink dbLink = new DatabaseLink();
            PasswordHash pHash = new PasswordHash();
            string hashed = pHash.hashPassword(passwordBox.Text);
            string result = dbLink.UserLogin(emailBox.Text, hashed);

            MessageBox.Show(result);
            if (result.Contains("SUCCESS"))
            {
                MainForm.userMail = emailBox.Text;
                Properties.Settings.Default.defkey = "ssiuser";
                this.Hide();
                MainForm.registOk = 't';
            }
        }

        private void backToLogin_Click(object sender, EventArgs e)
        {
            MainForm.registOk = 'm';
            this.Hide();
        }
    }
}
