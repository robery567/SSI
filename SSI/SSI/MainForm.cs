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
using Newtonsoft.Json.Linq;
namespace SSI
{//460-1100
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }
        public static string TokenKey = " ";
        public static bool loginOk = false;
        public static bool registOk = false;
        private bool isCreated = false;
        private int timeCount = 0;
        bool firstPictureClick = true;
        TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        Label[] lb = new Label[45];
        Panel[] pan = new Panel[45];
        PictureBox[] pb = new PictureBox[45];
        Label[] dow = new Label[8];
        DateTime dt;
        Random rand = new Random();        
        DateConverter dtConv = new DateConverter();
        EventColor evColor = new EventColor();
        EventArgs evAr = null;
        object last, lastPic;
        bool first = true;
        
       
        public string GetProfileImageUrl(string facebookId)
        {
            return "http://graph.facebook.com/" + facebookId + "/picture?width=150&height=150";
        }
        private void loginBtn_Click(object sender, EventArgs e)
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
              
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            LoginForm.login = false;
            LoginForm f1 = new LoginForm();
            f1.WindowState = FormWindowState.Minimized;
            f1.Show();
            /*loginBtn.Visible = true;
            loginSSIBtn.Visible = true;
            logoutBtn.Visible = false;
            registerBtn.Visible = true;
            label1.Visible = false;
            userPhoto.Visible = false;
            monthBox.Visible = false;
            yearBox.Visible = false;
            groupBox1.Visible = false;
            tableLayoutPanel1.Visible = false;*/
            Settings.Default.defkey = "notlogged";
            Settings.Default.Save();
            LoadData();
            timer1.Start();            
        }              
        private void CheckDatabase(DateTime dateCheck)
        {
            /*string sCheck = dateCheck.GetDateTimeFormats()[5];
            string command = "select * from ssipdb.events where date='" + sCheck + "';";
            using (MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Database=ssipdb;Uid=root;Pwd= ;"))
            {                
                using(MySqlCommand comm = new MySqlCommand(command,conn))
                {
                    conn.Open();
                    using(MySqlDataReader read = comm.ExecuteReader())
                    {
                        while(read.Read())
                        {
                            string image = read.GetString("image");
                            string text = read.GetString("text");
                            //entryImage.Image = (Base64ToImage(image));
                            entryBox.Clear();
                            entryBox.Text +=text;
                        }
                    }
                }
            }*/
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            loginBtn.Visible = false;
            logoutBtn.Visible = true;
            label1.Visible = true; 
            LoadData();
        }
        
        public void LoadData()
        {
            if (Settings.Default.defkey == "notlogged")
            {
                loginBtn.Visible = true;
                logoutBtn.Visible = false;
                label1.Visible = false;
                tableLayoutPanel1.Visible = false;
                monthBox.Visible = false;
                groupBox1.Visible = false;
                userPhoto.Visible = false;
                yearBox.Visible = false;
                loginSSIBtn.Visible = true;
                registerBtn.Visible = true;
                objectiveControls.Visible = false;
            }
            else
            {
                loginBtn.Visible = false;
                logoutBtn.Visible = true;
                registerBtn.Visible = false;
                label1.Visible = true;
                monthBox.Visible = true;
                yearBox.Visible = true;
                groupBox1.Visible = true;
                loginSSIBtn.Visible = false;
                objectiveControls.Visible = true;
                DateTime currentdt = DateTime.Now;
                monthBox.SelectedItem=currentdt.ToString("MMMM");
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
                    userPhoto.Load(GetProfileImageUrl(uid));
                    System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddEllipse(1, 1, userPhoto.Width, userPhoto.Height);
                    Region rg = new Region(gp);
                    userPhoto.Region = rg;
                    userPhoto.SizeMode = PictureBoxSizeMode.Zoom;
                    userPhoto.Visible = true;
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
                    Settings.Default.defkey = "notlogged";
                    Settings.Default.Save();
                    LoadData();
                    
                }
                catch(FacebookApiException ex)
                {
                    MessageBox.Show(ex.Message);
                    Settings.Default.defkey = "notlogged";
                    Settings.Default.Save();
                    LoadData();
                }
                catch(WebExceptionWrapper)
                {
                    MessageBox.Show("Please check your internet connection !");
                    this.Close();
                }               
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(loginOk)
            {
                LoadData();
                timer1.Stop();
                loginOk = false;
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (firstPictureClick)
            {
                openFileDialog1.ShowDialog();
                firstPictureClick = false;
            }
            
        }

       private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            entryImage.ImageLocation = openFileDialog1.FileName;
            entryImage.SizeMode = PictureBoxSizeMode.Zoom;
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
           tableLayoutPanel1.RowCount = 6;
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
           tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
           int count = 1;
           dt = new DateTime(Decimal.ToInt32(yearBox.Value), dtConv.GetDateInt(monthBox.Text), 1);
           int daysInMonth = DateTime.DaysInMonth(Decimal.ToInt32(yearBox.Value), dtConv.GetDateInt(monthBox.Text));
           if(daysInMonth+dtConv.GetDayInt(dt.DayOfWeek.ToString())>35)
           {
               tableLayoutPanel1.RowCount = 7;
               tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
           }
           if (daysInMonth + dtConv.GetDayInt(dt.DayOfWeek.ToString()) <29)
           {
               tableLayoutPanel1.RowCount = 5;
           }
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
           for (int j = dtConv.GetDayInt(dt.DayOfWeek.ToString()); j < tableLayoutPanel1.ColumnCount; j++)
           {
                   pan[count] = new Panel();
                   lb[count] = new Label();
                   pb[count] = new PictureBox();
                   
                   pb[count].Click += new System.EventHandler(PanelClickEvent);
                   pb[count].Name = count.ToString();
                   pb[count].Image = Resources.plus;
                   pb[count].SizeMode = PictureBoxSizeMode.Zoom;
                   pb[count].Dock = DockStyle.Fill;
                   lb[count].Click += new System.EventHandler(LabelClickEvent);
                   lb[count].Text = (count).ToString();
                   lb[count].AutoSize = true;
                   lb[count].Anchor = AnchorStyles.Top;
                   lb[count].Dock = DockStyle.Top;
                   tableLayoutPanel1.Controls.Add(pan[count], j, 1);
                   pan[count].Controls.Add(lb[count]);
                   pan[count].Controls.Add(pb[count]);
                   count++;
           }                       
            for (int i = 2; i < tableLayoutPanel1.RowCount; i++)
               for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)                
                {
                    pan[count] = new Panel();
                    lb[count] = new Label();
                    pb[count] = new PictureBox();
                    pb[count].Click += new System.EventHandler(PanelClickEvent);
                    pb[count].Name = count.ToString();
                    pb[count].Image = Resources.plus;
                    pb[count].SizeMode = PictureBoxSizeMode.Zoom;
                    pb[count].Dock = DockStyle.Fill;
                    lb[count].Click += new System.EventHandler(LabelClickEvent);
                    lb[count].Text = (count).ToString();
                    lb[count].AutoSize = true;
                    lb[count].Anchor = AnchorStyles.Top;
                    lb[count].Dock = DockStyle.Top;
                    tableLayoutPanel1.Controls.Add(pan[count], j, i);
                    pan[count].Controls.Add(lb[count]);
                    pan[count].Controls.Add(pb[count]);
                    count++;
                    if (count > daysInMonth)
                    {
                        i = 100;
                        j = 100;
                    }      
               }
            tableLayoutPanel1.BackColor = Color.ForestGreen;
            this.Controls.Add(tableLayoutPanel1);
            isCreated = true;
        }

       private void monthBox_SelectedIndexChanged(object sender, EventArgs e)
       {
           TableLayoutCreator();
       }
       private void LabelClickEvent(object sender , EventArgs e)
       {
           dateLabel.Visible = true;
           entryBox.Clear();
           entryImage.Image = Resources.clickheretoselect;
           dateLabel.Text = "Date: " + dtConv.GetDateInt(monthBox.Text) + "/" + ((Label)sender).Text + "/" + yearBox.Value.ToString();
           entryBox.Enabled = true;
           DateTime dateSend;
           
           dateSend = new DateTime(Decimal.ToInt32(yearBox.Value), dtConv.GetDateInt(monthBox.Text), Convert.ToInt32(((Label)sender).Text));
           
           ((Label)sender).BackColor = Color.GhostWhite;
           if (first)
           {
               first = false;
           }
           else 
           {
               ((PictureBox)lastPic).BackColor = Color.ForestGreen;
               ((Label)last).BackColor = Color.ForestGreen;
               if(last==sender)
               {
                   ((Label)sender).BackColor = Color.ForestGreen;
                   pb[Convert.ToInt32(((Label)sender).Text)].BackColor = Color.ForestGreen;
               }
           }
           entryImage.Image= Resources.clickheretoselect;
          last  = ((Label)sender);
          lastPic = pb[Convert.ToInt32(((Label)sender).Text)];
          
          CheckDatabase(dateSend);
       }
       private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           TableLayoutCreator();
        }

       private void saveToDb_Click(object sender, EventArgs e)
       {
           /*DateTime dateDb = new DateTime(Decimal.ToInt32(yearBox.Value), dtConv.GetDateInt(monthBox.Text), Convert.ToInt32(((Label)last).Text));
           string sCheck = dateDb.GetDateTimeFormats()[5];
           string command = "replace into ssipdb.events(date,text,image) values('" + sCheck + "','" + entryBox.Text + "','" + ImageToBase64(entryImage.Image, System.Drawing.Imaging.ImageFormat.Jpeg) + "');";
           using (MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Database=ssipdb;Uid=root;Pwd= ;"))
           {
               using (MySqlCommand comm = new MySqlCommand(command, conn))
               {
                   conn.Open();
                   using(MySqlDataReader mread = comm.ExecuteReader())
                   {
                       MessageBox.Show("Saved");
                       while(mread.Read())
                       {

                       }
                   }

               }
           }
           entryBox.Clear();
           entryBox.Enabled = false;
           entryImage.Image = Resources.clickheretoselect;
           dateLabel.Visible = false;*/
       }
        
       private void PanelClickEvent(object sender, EventArgs e)
        {
            LabelClickEvent(lb[Convert.ToInt32(((PictureBox)sender).Name)],evAr);
            ((PictureBox)sender).BackColor = Color.AliceBlue;
        }

       private void registTimer_Tick(object sender, EventArgs e)
        {
            if(registOk)
            {
                registOk = false;
                registTimer.Stop();
                loginBtn.Visible = true;
                loginSSIBtn.Visible = true;
                registerBtn.Visible = true;
            }
        }

       private void registerBtn_Click(object sender, EventArgs e)
        {
            registerWindow1.Visible = true;
            loginBtn.Visible = false;
            loginSSIBtn.Visible = false;
            registerBtn.Visible = false;
            registTimer.Start();
        }

       private void button1_Click(object sender, EventArgs e)
       {
           MessageBox.Show(this.Size.Height.ToString() +" "+ this.Size.Width.ToString());
       }

       private void colorBtn_Click(object sender, EventArgs e)
       {
           
       }

    }
}
