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
        }
        public string GetUserEvent(string email, string date)
        {
            WebClient wb = new WebClient();
            string result = wb.DownloadString(defaultUri+"get_user_event"+emailArg+email+dateArg+date);
            return ResultParser(result);            
        }
        public string GetInfo(string email)
        {
            WebClient wb = new WebClient();
            string result = wb.DownloadString(defaultUri + "get_info" + emailArg + email);
            return ResultParser(result);
        }
        public string GetEvents(string email)
        {
            WebClient wb = new WebClient();
            string result = wb.DownloadString(defaultUri + emailArg + email);
            return ResultParser(result);
        }
        public string InsertEvent(string email,string date,string data)
        {
            string URI = defaultUri+"insert_event";
            string parameters = "email=" + email + "&date=" + date + "&data=" + data;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wc.UploadString(URI, parameters);
                return ResultParser(result);
            }
        }
        public string UserLogin(string email, string password)
        {
            string URI = defaultUri+"login";
            string parameters = "email=" + email + "&password=" + password;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wc.UploadString(URI, parameters);
                return ResultParser(result);
            }
        }
        
        public string InsertUser(string email , string password,string name)
        {
            string URI = defaultUri + "insert";
            string parameters = "email=" + email + "&password=" + password + "&name=" + name;
            using (WebClient wb = new WebClient())
            {
                wb.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wb.UploadString(URI, parameters);
                return ResultParser(result);
            }
        }
        public string InsertUser(string email, string password, string name, string image)
        {
            string URI = defaultUri + "insert";
            string parameters = "email=" + email + "&password=" + password + "&name=" + name + "&image=" + image;
            using (WebClient wb = new WebClient())
            {
                wb.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wb.UploadString(URI, parameters);
                return ResultParser(result);
            }
        }
        
        public string InsertUser(string fbid, string email, string password, string name, string image)
        {
            string URI = defaultUri + "insert";
            string parameters = "fbid=" + fbid + "&email=" + email + "&password=" + password + "&name=" + name + "&image=" + image;
            using (WebClient wb = new WebClient())
            {
                wb.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wb.UploadString(URI, parameters);
                return ResultParser(result);
            }
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
    }
}
