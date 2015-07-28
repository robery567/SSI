using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
using Facebook.Reflection;
using System.Web;
using System.IO;

namespace SSI
{
    public partial class LoginForm : Form
    {
        public static bool login=true;
        public static bool status = false;
        
        public int ticker = 0;
        public LoginForm()
        {
            status = true;
            InitializeComponent();
        }        
        private void Form1_Load(object sender, EventArgs e)
        {            
            string loginURL = @"https://www.facebook.com/dialog/oauth?client_id=847697765303140&redirect_uri=https://www.facebook.com/connect/login_success.html&response_type=token&scope=email,user_photos";
            if (login)
            {
                try
                {                    
                    LoginBrowser.Navigate(loginURL);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if(login==false)
            {
                try
                {
                    string logoutURL = @"https://www.facebook.com/logout.php?next=" + System.Web.HttpUtility.UrlEncode(loginURL, ASCIIEncoding.UTF8)+"&access_token=" + Properties.Settings.Default.defkey;
                    LoginBrowser.Navigate(logoutURL);
                    Properties.Settings.Default.defkey = "notlogged";
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        string access_token;
        
        private void LoginBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (login == true)
            {
                if (LoginBrowser.Url.AbsoluteUri.Contains("access_token"))
                {
                    string url1 = LoginBrowser.Url.AbsoluteUri;
                    string url2 = url1.Substring(url1.IndexOf("access_token") + 13);
                    access_token = url2.Substring(0, url2.IndexOf("&"));
                    MainForm.TokenKey = access_token;
                    if (MainForm.TokenKey != " ")
                    {
                        Properties.Settings.Default.defkey = MainForm.TokenKey;
                        Properties.Settings.Default.Save();
                    }
                    //timer1.Start();
                    MainForm.loginOk = 't';
                    timer1.Start();
                    this.Hide();               
                }
            }
               
            if (LoginBrowser.Url.AbsoluteUri.Contains("skip_api_login=1&api_key") && login == false)
                    this.Close();
               
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ticker++;
            if(ticker==5)
            {
                ticker = 0;
                MainForm.loginOk = 'f';
                this.Close();
            }
        }
              

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            status = false;
            //MainForm.loginOk = 'm';
        }
    }
}
