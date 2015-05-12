using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;   //mersi Mark Zuckerberg ~!
using System.IO;
using SSI.Properties; 
namespace SSI
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }
        public static string TokenKey = " ";
        public static bool loginOk = false;
        public string GetProfileImageUrl(string facebookId)
        {
            return "http://graph.facebook.com/" + facebookId + "/picture?type=large";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.login = true;
            Form1 f1 = new Form1();            
            f1.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.login = false;
            Form1 f1 = new Form1();
            f1.Show();
            button1.Visible = true;
            button3.Visible = false;
            label1.Visible = false;
            pictureBox1.Visible = false;
        }              

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            //Settings.Default.defkey = "notlogged";
            button1.Visible = false;
            button3.Visible = true;
            label1.Visible = true;
            LoadData();      
        }
        
        public void LoadData()
        {
            if (Settings.Default.defkey == "notlogged")
            {
                button1.Visible = true;
                button3.Visible = false;
                label1.Visible = false;
            }
            else
            {
                button1.Visible = false;
                button3.Visible = true;
                label1.Visible = true;
                TokenKey = Properties.Settings.Default.defkey;
                try
                {
                    FacebookClient fb = new FacebookClient(TokenKey);
                    dynamic data = fb.Get("/me");
                    label1.Text = data.name;
                    string uid = data.id;
                    
                    pictureBox1.Load(GetProfileImageUrl(uid));
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Visible = true;
                }
                catch(FacebookOAuthException ex)
                {
                    if (ex.ErrorCode == 190)
                        MessageBox.Show("Boss, it looks like your access time expired. Would you be so kind as to log in again ?");
                    else
                        MessageBox.Show(ex.Message);
                    Settings.Default.defkey = "notlogged";
                    Settings.Default.Save();
                    LoadData();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(loginOk)
            {
                LoadData();
                loginOk = false;               
            }
        }
    }
}
