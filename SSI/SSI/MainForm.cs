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
            Brushes.CadetBlue,
            Brushes.Brown,
            Brushes.LimeGreen,
            Brushes.Yellow,
            Brushes.SkyBlue,
            Brushes.Teal,
            Brushes.MidnightBlue,
            Brushes.Firebrick,
            Brushes.LightSeaGreen,
            Brushes.Chocolate
        };
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
                timer1.Start();
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
            monthCalendar1.Visible = false;
            groupBox1.Visible = false;
            
            
        }              

        private void MainForm_Load(object sender, EventArgs e)
        {            
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
               // TableLayoutCreator();
                monthCalendar1.Visible = true;
                groupBox1.Visible = true;
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
                    {
                        MessageBox.Show("Boss, it looks like your access time expired. Would you be so kind as to log in again ?");    
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
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
                timer1.Stop();
                LoadData();
                loginOk = false;
                
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox2.ImageLocation = openFileDialog1.FileName;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        
       /* private void TableLayoutCreator()
        {
                       
            Panel[] lb = new Panel[36];
           // tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddRows;            
            int count = 1;
            for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
               for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)                
                {
                    lb[count] = new Panel();
                    lb[count].Text = (count).ToString();
                    lb[count].AutoSize = true;                                     
                    tableLayoutPanel1.Controls.Add(lb[count],j,i);
                    count++; 
                }
                        
        }*/


    }
}
