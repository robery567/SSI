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
        public string error=null;
        string defaultUri = "http://localhost/ssi/ssi_api.php?action=";
        string emailArg = "&email=";
        string dateArg = "&date=";
        string imageArg = "&image=";
        public DatabaseLink()
        {
            
            WebClient wb = new WebClient();
            try
            {
                string result = wb.DownloadString(defaultUri + "status_check");
            }
            catch(System.Net.WebException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }

        }
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
        public string InsertEvent(string email,string date,string data)
        {
            string address = defaultUri+"insert_event";
            string parameters = "email=" + email + "&date=" + date + "&data=" + data;
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
        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
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
    }
}
