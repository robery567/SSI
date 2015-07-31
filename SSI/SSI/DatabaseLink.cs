using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace SSI
{
    class DatabaseLink
    {
		//clasa contine metode ce apeleaza api-ul prin care trimite datele in baza de date ( mysql , mongodb in functie de date)
        public string error=null;
        string defaultUri = "http://" + Properties.Settings.Default.apiaddress + "ssi_api.php?action=";
        string emailArg = "&email=";
        string dateArg = "&date=";
        string imageArg = "&image=";
		//in momentul instantierii clasei se verifica conexiunea la api
        public DatabaseLink()
        {
            LoadApiAddress();
            WebClient wb = new WebClient();
            try
            {
                string result = wb.DownloadString(defaultUri + "status_check");
            }
            catch (System.Net.WebException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }
		//fiecare metoda apeleaza ori GetRequest ori PostRequest (in functie de tipul requestului http) , ele primesc parametrii necesari
       //unele metode sunt "overloaded" deoarece cativa parametrii sunt optionali
        public string GetUserEvent(string email, string date)
        {
            return GetRequest(defaultUri+"get_user_event"+emailArg+email+dateArg+date);                       
        }
        public string GetInfo(string email)
        {
            return GetRequest(defaultUri + "get_info" + emailArg + email);            
        }
        public string GetEvents(string email)
        {
          return GetRequest(defaultUri + emailArg + email);            
        }
        public string InsertEvent(int evnum,string email,string date,string data)
        {
            string address = defaultUri+"insert_event";
            string parameters = "email=" + email + "&date=" + date + "&data=" + data+"&num="+evnum;
            return PostRequest(address, parameters);
        }
        public string UserLogin(string email, string password)
        {
            string address = defaultUri+"login";
            string parameters = "email=" + email + "&password=" + password;
            return PostRequest(address, parameters);
        }        
        public string InsertUser(string email , string password,string name)
        {
            string address = defaultUri + "insert";
            string parameters = "email=" + email + "&password=" + password + "&name=" + name;
            return PostRequest(address, parameters);
        }
        public string InsertFacebookUser(string fbid, string email,string name,string image)
        {
            string address = defaultUri + "insert";
            string parameters = "fbid=" + fbid + "&email=" + email+ "&name=" + name + "&image=" + image;
            return PostRequest(address, parameters);
        }
        public string InsertUser(string email, string password, string name, string image)
        {
            string address = defaultUri + "insert";
            string parameters = "email=" + email + "&password=" + password + "&name=" + name + "&image=" + image;
            return PostRequest(address, parameters);
        }        
        public string InsertUser(string fbid, string email, string password, string name, string image)
        {
            string address = defaultUri + "insert";
            string parameters = "fbid=" + fbid + "&email=" + email + "&password=" + password + "&name=" + name + "&image=" + image;
            return PostRequest(address, parameters);
        }
		//metoda ce verifica stringul returnat de api , in cazul in care acesta contine o eroare , este trimis in metoda ErrorParser
        public string ResultParser(string result)
        {
            error = null;
            if(result.Length>4)
            error=result.Substring(0, 4);
            if (error=="ERR_")
            {
                error = result;
                return ErrorParser(error);
            }
            else return result;
        }
		//metoda de transformare a unui string base64 (ce contine datele unei imagini) intr-un obiect Image
        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
		//metoda de transformare a unei imagini intr-un string base64
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
		//"traduce" codul erorii, corespunzand desigur cu cele din api
        public string ErrorParser(string error)
        {
            switch (error)
            {                
                case "ERR_101":
                    return "ERROR: INVALID ACTION";
                case "ERR_102":
                    return "ERROR: USER EXISTS";
                case "ERR_103":
                    return "ERROR: INVALID EMAIL";
                case "ERR_104":
                    return "ERROR: INCORRECT_CREDENTIALS";
                case "ERR_105":
                    return "ERROR: INVALID FACEBOOK ID";
                case "ERR_106":
                    return "ERROR: INVALID NAME";
                case "ERR_404":
                    return "ERROR: NOT FOUND";
                default:
                    return "ERROR: INTERNAL SERVER ERROR";
            }
        }
		//Trimiterea unui request GET si parsarea stringului returnat de api
        public string GetRequest(string address)
        {
            WebClient wb = new WebClient();
            try
            {
                string result = wb.DownloadString(address);
                return ResultParser(result);
            }
            catch(System.Net.WebException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return "ERROR: CAN'T CONNECT TO THE REMOTE SERVER";
        }
		//Trimiterea unui request POST si parsarea stringului returnat de api
        public string PostRequest(string address,string parameters)
        {
            WebClient wb = new WebClient();
            wb.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            try
            {
                string result = wb.UploadString(address,parameters);
                return ResultParser(result);
            }
            catch(System.Net.WebException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return "ERROR: CAN'T CONNECT TO THE REMOTE SERVER";
        }
        void LoadApiAddress()
        {
            try
            {
                StreamReader sr = new StreamReader("apilocation");
                Properties.Settings.Default.apiaddress = sr.ReadLine();
                Properties.Settings.Default.Save();
                defaultUri = "http://" + Properties.Settings.Default.apiaddress + "ssi_api.php?action=";
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
