using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using Facebook;
using System.IO;
using SSI.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SSI
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }
        
        public static string TokenKey = " ";
        public static char loginOk = 'f';
        public static char registOk = 'f';
        public static string userMail;
        public int eventCount;
        public List<EventValues> eValues = new List<EventValues>();
        string userFullName,caller;
        private bool isCreated = false;
        private int timeCount = 0;
        bool jsonNull = false;
        TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        Label[] lb = new Label[45];
        Panel[] pan = new Panel[45];
        PictureBox[] pb = new PictureBox[45];
        Label[] dow = new Label[8];
        DateTime dt;
        Random rand = new Random();        
        DateConverter dtConv = new DateConverter();
        EventColor evColor = new EventColor();
        DatabaseLink dbLink = new DatabaseLink();
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
                facebookLoginTimer.Start();
                LoginForm.login = true;
                LoginForm f1 = new LoginForm();
                f1.Show();
            }

        }
              
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            switch(caller)
            {
                case "facebookuser":
                    {
                        LoginForm.login = false;
                        LoginForm f1 = new LoginForm();
                        f1.WindowState = FormWindowState.Minimized;
                        f1.Show();
                        Settings.Default.defkey = "notlogged";
                        Settings.Default.Save();
                        LoadData();
                        //facebookLoginTimer.Start();
                    }break;
                case "ssiuser":
                    {
                        hideElements();
                    }break;
           }
        }              
        
        private void CheckDatabase(DateTime dateCheck)
        {
            eventComboBox.Items.Clear();
            eventComboBox.Text = null;
            string sCheck = dateCheck.GetDateTimeFormats()[5];
            string jsonResult = dbLink.GetUserEvent(userMail, sCheck);
            if (jsonResult != "null")
            {
                jsonNull = false;
                string jsonCheck = jsonResult.Substring(0, 6);
                if (jsonCheck == "ERROR:")
                    MessageBox.Show(jsonResult);
                else
                {

                    JObject jsonArray = JObject.Parse(jsonResult);
                    string jsonData = jsonArray.GetValue("data").ToString();
                    eValues = JsonConvert.DeserializeObject<List<EventValues>>(jsonData);
                    eventCount = Convert.ToInt32(jsonArray.GetValue("num").ToString());                    
                    for (int i = 0; i <eventCount;i++)
                    {
                        eventComboBox.Items.Add("Event " + (i+1) +" : " +eValues[i].title);
                    }                    
                    eventComboBox.SelectedIndex = 0;
                    eventComboBox.Items.Add("New event");
                    entryBox.Text = eValues[0].text;
                    entryImage.Image = dbLink.Base64ToImage(eValues[0].image64);
                    titleTextBox.Text = eValues[0].title;
                }
            }
            else
            {
                jsonNull = true;
                eValues.Clear();
                eventCount = 0;
                eventComboBox.Items.Add("Event : 1 :");
                eventComboBox.SelectedIndex = 0;
            }
           
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
            switch(Settings.Default.defkey)
            {
                case "notlogged":
                {
                hideElements();
                }
                break;
                case "ssiuser":
                {                                        
                    string jsonResult = dbLink.GetInfo(userMail);
                    JObject jsonArray = JObject.Parse(jsonResult);
                    userFullName = jsonArray.GetValue("name").ToString();
                    showElements();
                    if(jsonArray.GetValue("image").ToString()!="")                    
                    userPhoto.Image = dbLink.Base64ToImage(jsonArray.GetValue("image").ToString());
                    logoutBtn.BackgroundImage = Resources.SSI_Logout_1;
                    caller = "ssiuser";
                }
                break;
                default:
                {                
                TokenKey = Properties.Settings.Default.defkey;
                try
                {
                    FacebookClient fb = new FacebookClient(TokenKey);
                    dynamic data = fb.Get("/me");                    
                    string uid = data.id;
                    userMail = data.email;
                    userPhoto.Load(GetProfileImageUrl(uid));
                    userFullName = data.name;
                    showElements();
                    if (dbLink.GetInfo(userMail) == "ERROR: NOT FOUND")
                    dbLink.InsertFacebookUser(uid, userMail, userFullName, HttpUtility.UrlEncode(dbLink.ImageToBase64(userPhoto.Image, userPhoto.Image.RawFormat)));
                    logoutBtn.BackgroundImage = Resources.logout21;
                    caller = "facebookuser";
                }
                catch(FacebookOAuthException ex)
                {
                    if (ex.ErrorCode == 190)
                    {
                        MessageBox.Show("It looks like your access time expired. Would you be so kind as to log in again ?");
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
                catch(WebExceptionWrapper ex)
                {
                    MessageBox.Show("Please check your internet connection !");
                    MessageBox.Show(ex.Message);
                    this.Close();
                }             
                
            }break;

            }
               
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(loginOk=='t')
            {
                LoadData();
                facebookLoginTimer.Stop();
                loginOk = 'f';
            }
            if (loginOk == 'm')
                facebookLoginTimer.Stop();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {            
               openFileDialog1.ShowDialog();     
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
           titleTextBox.Text = null;
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
           try
           {
               DateTime dateDb = new DateTime(Decimal.ToInt32(yearBox.Value), dtConv.GetDateInt(monthBox.Text), Convert.ToInt32(((Label)last).Text));
               string sCheck = dateDb.GetDateTimeFormats()[5];
               int evNum = eventComboBox.SelectedIndex;
               if(eValues.Count>=evNum && evNum==0 && !jsonNull)
               {
                   eValues[0].text = entryBox.Text;
                   eValues[0].image64 = dbLink.ImageToBase64(entryImage.Image, entryImage.Image.RawFormat);
                   eValues[0].title = titleTextBox.Text;
               }else if (eValues.Count > evNum && evNum > 0)
               {
                   eValues[evNum].text = entryBox.Text;
                   eValues[evNum].image64 = dbLink.ImageToBase64(entryImage.Image, entryImage.Image.RawFormat);
                   eValues[evNum].title = titleTextBox.Text;
               }
               else
               {
                   eValues.Add(new EventValues { text = entryBox.Text, image64 = dbLink.ImageToBase64(entryImage.Image, entryImage.Image.RawFormat), title = titleTextBox.Text });
                   eventCount++;
               }
               string jsonArray = JsonConvert.SerializeObject(eValues);
               if (dbLink.InsertEvent(eventCount, userMail, sCheck, HttpUtility.UrlEncode(jsonArray)).Contains("MySQL server has gone away in"))
                   MessageBox.Show("Image is too big. Maximum size allowed: 500KB");
           }
           catch(Exception)
           {
               MessageBox.Show("Please select a date");
           }
       }
        
       private void PanelClickEvent(object sender, EventArgs e)
        {
            LabelClickEvent(lb[Convert.ToInt32(((PictureBox)sender).Name)],evAr);
            ((PictureBox)sender).BackColor = Color.AliceBlue;
        }

       private void registTimer_Tick(object sender, EventArgs e)
        {
            if(registOk=='t')
            {
                LoadData();
                registOk = 'f';
                registTimer.Stop();
                this.AcceptButton = null;
            }
           if(registOk == 'm')
           {
               registOk = 'f';
               registTimer.Stop();
               loginBtn.Visible = true;
               loginSSIBtn.Visible = true;
               registerBtn.Visible = true;
           }

        }

       private void registerBtn_Click(object sender, EventArgs e)
        {
            registerWindow1.Visible = true;
            registerWindow1.Dock = DockStyle.Fill;
            loginBtn.Visible = false;
            loginSSIBtn.Visible = false;
            registerBtn.Visible = false;
            registTimer.Start();
            this.AcceptButton = registerWindow1.registerBtn;
        }

       private void loginSSIBtn_Click(object sender, EventArgs e)
       {
           loginWindow1.Visible = true;
           loginWindow1.Dock = DockStyle.Fill;
           loginBtn.Visible = false;
           loginSSIBtn.Visible = false;
           registerBtn.Visible = false;
           registTimer.Start();
           this.AcceptButton = loginWindow1.button1;
       }
       private void showElements()
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
           eventComboBox.Visible = true;
           DateTime currentdt = DateTime.Now;
           monthBox.SelectedItem = currentdt.ToString("MMMM");
           if (!isCreated)
               TableLayoutCreator();
           tableLayoutPanel1.Visible = true;
           System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
           gp.AddEllipse(1, 1, userPhoto.Width, userPhoto.Height);
           Region rg = new Region(gp);
           userPhoto.Region = rg;
           userPhoto.SizeMode = PictureBoxSizeMode.Zoom;
           userPhoto.Visible = true;
           label1.Text = userFullName;
       }
       private void hideElements()
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
           eventComboBox.Visible = true;
       }

       private void fullSizeImageButton_Click(object sender, EventArgs e)
       {
           imageFullSize1.BackgroundImage = entryImage.Image;
           imageFullSize1.Dock = DockStyle.Fill;
           imageFullSize1.Visible = true;
       }

       private void eventComboBox_SelectedIndexChanged(object sender, EventArgs e)
       {           
           int evNumber = eventComboBox.SelectedIndex;
           if (!jsonNull && eventComboBox.Text!="New event")
           {
               entryBox.Text = eValues[evNumber].text;
               entryImage.Image = dbLink.Base64ToImage(eValues[evNumber].image64);
               titleTextBox.Text = eValues[evNumber].title;
           }
           if(eventComboBox.Text == "New event")
           {
               entryBox.Clear();
               titleTextBox.Clear();
               entryImage.Image = Resources.clickheretoselect;
           }
       }
       
    }
    class DateConverter
    {
        public DateConverter()
        {
        }
        public int GetDateInt(string date)
        {
            switch (date)
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
        public int GetDayInt(string dayWeek)
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

    }
}
