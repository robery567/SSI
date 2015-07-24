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
        public string error;
        string defaultUri = "http://localhost/ssi/ssi_api.php?action=";
        string emailArg = "&email=";
        string dateArg = "&date=";
        string image = "&image=";
        public DatabaseLink()
        {            
        }
        public string GetEvent(string email, string date)
        {
            WebClient wb = new WebClient();
            string text = wb.DownloadString(defaultUri+"get_user_event"+emailArg+email+dateArg+date);
            return text;
            
        }
        public string InsertEvent(string email,string date,string text,string image)
        {
            string URI = defaultUri+"insert_event";
            string myParameters = "email=" + email +"&date="+date+ "&text=" + text + "&image=" + image;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wc.UploadString(URI, myParameters);
                return result;
            }
        }
        public string UserLogin(string email, string password)
        {
            string URI = defaultUri+"login";
            string myParameters = "email=" + email + "&password=" + password;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wc.UploadString(URI, myParameters);
                return result;
            }
        }
        public string InsertUser(string email, string password, string name)
        {
            WebClient wb = new WebClient();
            string text = wb.DownloadString(defaultUri+"insert&fbid=' '" + "&email=" + email + "&password=" + password + "&name=" + name);
            return Error(text);
            
        }
        public string InsertUser(string fbid ,string email , string password,string name)
        {
            WebClient wb = new WebClient();
            string text = wb.DownloadString(defaultUri+"insert&"+"&fbid="+fbid+"&email="+email+"&password="+password+"&name="+name);
            return Error(text);
        }
        public string InsertUser(string fbid, string email, string password, string name, string image)
        {
            string URI = defaultUri+"insert";
            string myParameters = "fbid="+fbid+"&email=" + email + "&password=" + password + "&name=" + name + "&image=" + image;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string result = wc.UploadString(URI, myParameters);
                    return result;
                }
            }
            catch (System.Net.WebException)
            {
                return "Error";
            }
        }
        
        public string Error(string result)
        {
            if (result.Contains("ERR_"))
            {
                error = result;
                return "Something went wrong";
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
    }
}
