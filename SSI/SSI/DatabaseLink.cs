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
        public DatabaseLink()
        {

        }
        public string InsertUser(string email, string password, string name)
        {
            WebClient wb = new WebClient();
            string text = wb.DownloadString("http://localhost/ssi/ssi_api.php?action=insert&fbid=' '" + "&email=" + email + "&password=" + password + "&name=" + name);
            return Error(text);
            
        }
        public string InsertUser(string fbid ,string email , string password,string name)
        {
            WebClient wb = new WebClient();
            string text = wb.DownloadString("http://localhost/ssi/ssi_api.php?action=insert&"+"&fbid="+fbid+"&email="+email+"&password="+password+"&name="+name);
            return Error(text);
        }
        public string InsertUser(string fbid, string email, string password, string name, string image)
        {
            string URI = "http://localhost/ssi/ssi_api.php";
            string myParameters = "?action=insert&fbid=' '&email=" + email + "&password=" + password + "&name=" + name + "image=" + image;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wc.UploadString(URI, myParameters);
                return result;
            }
           // WebClient wbd = new WebClient();
            //string text = wbd.DownloadString("http://localhost/ssi/ssi_api.php?action=insert&" + "&fbid=" + fbid + "&email=" + email + "&password=" + password + "&name=" + name + "&image=" + image);
            //return Error(text);
        }
        public string InsertUser(string fbid, string email, string password, string name,string image, string settings)
        {
            WebClient wb = new WebClient();
            string text = wb.DownloadString("http://localhost/ssi/ssi_api.php?action=insert&" + "&fbid=" + fbid + "&email=" + email + "&password=" + password + "&name=" + name + "&image=" + image + "&settings=" + settings);
            return Error(text);
        }
        public string Error(string result)
        {
            if (result.Contains("ERR_"))
            {
                error = result;
                return "Something went wrong";
            }
            else return "Success";
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
