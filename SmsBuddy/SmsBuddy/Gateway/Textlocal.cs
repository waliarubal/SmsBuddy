using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace SmsBuddy.Gateway
{
    class Textlocal : GatewayBase
    {
        const string API_BASE_URL = "https://api.textlocal.in/";

        public Textlocal() : base("Textlocal")
        {
            Version = new Version(18, 9, 13, 21);
            ProviderUrl = new Uri("https://www.textlocal.in/");
            SetParameterNames("API Key", "Sender");
        }

        public override bool SendSms(string text, params string[] mobileNumbers)
        {
            var temp = this["Sender"];

            var mobile = new StringBuilder();
            foreach (var number in mobileNumbers)
                mobile.AppendFormat("{0},", number);
            mobile.Remove(mobile.Length - 1, 1);

            var apiKey = this["API Key"].ToString();
            var message = HttpUtility.UrlEncode(text);
            var sender = this["Sender"].ToString();
            using (var client = new WebClient())
            {
                var response = client.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                    {"apikey" , apiKey},
                    {"numbers" , mobile.ToString()},
                    {"message" , message},
                    {"sender" , sender}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result.Contains("\"status\":\"success\"");
            }
        }
    }
}
