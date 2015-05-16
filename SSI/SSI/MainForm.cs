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
        private bool isCreated = false;
        TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        Label[] lb = new Label[45];   
        private Label[] dow = new Label[8];
        DateTime dt;
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
           
            groupBox1.Visible = false;
            tableLayoutPanel1.Visible = false;
            timer1.Start();            
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
                tableLayoutPanel1.Visible = false;
                comboBox1.Visible = false;
            }
            else
            {
                button1.Visible = false;
                button3.Visible = true;
                label1.Visible = true;
                comboBox1.Visible = true;
                DateTime currentdt = DateTime.Now;
                comboBox1.SelectedItem=currentdt.ToString("MMMM");
                if (!isCreated)
                TableLayoutCreator();
                
                tableLayoutPanel1.Visible = true;
                //monthCalendar1.Visible = true;
                //groupBox1.Visible = true;
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
                        Settings.Default.defkey = "notlogged";
                        Settings.Default.Save();
                        LoadData();
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
        
       private void TableLayoutCreator()
        {
            tableLayoutPanel1.Dispose();
           tableLayoutPanel1 = new TableLayoutPanel();
           tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
           tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
           tableLayoutPanel1.ColumnCount = 7;
           tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
           tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.Location = new System.Drawing.Point(178, 44);
           tableLayoutPanel1.Name = "tableLayoutPanel1";
           tableLayoutPanel1.RowCount = 7;
           tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           tableLayoutPanel1.Size = new System.Drawing.Size(686, 461);
           tableLayoutPanel1.TabIndex = 5;
           tableLayoutPanel1.Visible = true;
           int count = 1;
           dt = new DateTime(Decimal.ToInt32(numericUpDown1.Value), GetDateInt(comboBox1.Text), 1);
           for (int i = 0; i < 7; i++)
               dow[i] = new Label();
           dow[0].Text = "Sun";
           dow[1].Text = "Mon";
           dow[2].Text = "Tue";
           dow[3].Text = "Wed";
           dow[4].Text = "Thu";
           dow[5].Text = "Fri";
           dow[6].Text = "Sat";        
           for (int j = 0; j < 7; j++)
              tableLayoutPanel1.Controls.Add(dow[j], j, 0);
               for (int j = GetDayInt(dt.DayOfWeek.ToString()); j < tableLayoutPanel1.ColumnCount; j++)
               {
                   lb[count] = new Label();
                   lb[count].Click += new System.EventHandler(LabelClickEvent);
                   lb[count].Text = (count).ToString();
                   lb[count].AutoSize = true;
                   lb[count].Anchor = AnchorStyles.Top;
                   lb[count].Dock = DockStyle.Fill;
                   tableLayoutPanel1.Controls.Add(lb[count], j, 1);
                   count++;
               }                       
            for (int i = 2; i < tableLayoutPanel1.RowCount; i++)
               for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)                
                {
                    lb[count] = new Label();
                    lb[count].Click += new System.EventHandler(LabelClickEvent);
                    lb[count].Text = (count).ToString();
                    lb[count].AutoSize = true;
                    lb[count].Anchor = AnchorStyles.Top;
                    lb[count].Dock = DockStyle.Fill;
                    tableLayoutPanel1.Controls.Add(lb[count],j,i);
                    count++; 
                }
            tableLayoutPanel1.BackColor = Color.ForestGreen;
            this.Controls.Add(tableLayoutPanel1);
            isCreated = true;
        }

       private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
       {
           TableLayoutCreator();
       }
       private void LabelClickEvent(object sender , EventArgs e)
       {
           MessageBox.Show(((Label)sender).Text);
       }
       private int GetDateInt(string date)
       {
           switch(date)
           {
               case "January":
                   return 1;                   
               case "February":
                   return 2;
               case "March":
                   return 3;
               case "April":
                   return 4;
               case "May":
                   return 5;
               case "June":
                   return 6;
               case "July":
                   return 7;
               case "August":
                   return 8;
               case "September":
                   return 9;
               case "October":
                   return 10;
               case "November":
                   return 11;
               case "December":
                   return 12;
           }
           return 1;
       }
       private int GetDayInt(string dayWeek)
       {
           switch (dayWeek)
           {
               case "Monday":
                   return 1;
               case "Tuesday":
                   return 2;
               case "Wednesday":
                   return 3;
               case "Thursday":
                   return 4;
               case "Friday":
                   return 5;
               case "Saturday":
                   return 6;
               case "Sunday":
                   return 0;
           }
           return 1;
       }

       private void numericUpDown1_ValueChanged(object sender, EventArgs e)
       {
           TableLayoutCreator();
       }
  
    }
}
