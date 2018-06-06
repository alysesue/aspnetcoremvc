using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GrandeTravelMVC.Services
{
    public static class SmsService
    {
        public static string SendSMSGet()
        {
            String result;
            string apiKey = "2jLVmAQhiDo-GJq2zseRHrnmQXO3UtAYp5qQeCB7Hu";
            string numbers = "+61405838333"; // in a comma seperated list
            string message = "Thanks for creating an account with Grande Travel";
            string sender = "Grande Travel";

            String url = "https://api.txtlocal.com/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
            //refer to parameters to complete correct url string

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }

        public static string SendSMSPost()
        {
            //string UserId = 1;
            String message = HttpUtility.UrlEncode("Thanks for creating an account with Grande Travel");
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
                {
                {"apikey" , "2jLVmAQhiDo-GJq2zseRHrnmQXO3UtAYp5qQeCB7Hu"},
                {"numbers" , "+61405838333"},
                {"message" , message},
                {"sender" , "Grande Travel"}
                });
                string result = Encoding.UTF8.GetString(response);
                return result;
            }
        }
    }
}
