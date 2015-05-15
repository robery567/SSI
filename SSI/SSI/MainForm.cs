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
using System.IO;
using SSI.Properties;
using System.Drawing.Drawing2D; 
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
        Random rand = new Random();
        Brush[] brushes = new Brush[] {
           Brushes.AliceBlue,
           Brushes.AntiqueWhite,
           Brushes.Aqua,
           Brushes.Indigo,
           Brushes.LightSkyBlue,
           Brushes.LightYellow,
           Brushes.Lime};
        public string GetProfileImageUrl(string facebookId)
        {
            return "http://graph.facebook.com/" + facebookId + "/picture?width=150&height=150";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (LoginForm.status)
                MessageBox.Show("Try again", "Sorry");
            else
            {
                LoginForm.login = true;
                LoginForm f1 = new LoginForm();
                f1.Show();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {            
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm.login = false;
            LoginForm f1 = new LoginForm();
            f1.WindowState = FormWindowState.Minimized;
            f1.Show();
            button1.Visible = true;
            button3.Visible = false;
            label1.Visible = false;
            pictureBox1.Visible = false;
            tableLayoutPanel1.Visible = false;
        }              

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

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
                tableLayoutPanel1.Visible = true;
                TokenKey = Properties.Settings.Default.defkey;
                try
                {
                    FacebookClient fb = new FacebookClient(TokenKey);
                    dynamic data = fb.Get("/me");
                    label1.Text = data.name;
                    string uid = data.id;
                    
                    pictureBox1.Load(GetProfileImageUrl(uid));
                    System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddEllipse(1, 1, pictureBox1.Width, pictureBox1.Height);
                    Region rg = new Region(gp);
                    pictureBox1.Region = rg;
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
                catch(FacebookApiException ex)
                {
                    MessageBox.Show(ex.Message);
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
        int randomValue;
        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
             /*randomValue=rand.Next(0,brushes.Length);
             Graphics g = e.Graphics;
             Rectangle r = e.CellBounds;
             g.FillRectangle(brushes[randomValue], r);*/
             
        }
    }
}
