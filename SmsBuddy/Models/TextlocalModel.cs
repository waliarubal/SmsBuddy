using NullVoidCreations.WpfHelpers.DataStructures;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace SmsBuddy.Models
{
    class TextlocalModel : SmsGatewayBase
    {
        public TextlocalModel() : base("Textlocal", "https://www.textlocal.in/")
        {
            var settings = new List<Doublet<string, string>>
            {
                new Doublet<string, string>("API Key", Debugger.IsAttached ? "0A/rlNBSEgc-JILXYKcCQ5B8iRWpIt5SaKmt2x0Zrp" : null),
                new Doublet<string, string>("Sender", "TXTLCL")
            };
            Settings = settings;
        }

        public override SentSmsModel Send(SmsModel message, IEnumerable<string> mobileNumbers)
        {
            var messageText = HttpUtility.UrlEncode(message.Message);
            using (var client = new WebClient())
            {
                var numbers = new StringBuilder();
                foreach (var number in mobileNumbers)
                    numbers.AppendFormat("{0},", number);
                if (numbers.Length > 0)
                    numbers.Remove(numbers.Length - 1, 1);

                var response = client.UploadValues("https://api.textlocal.in/send/",
                    new NameValueCollection
                    {
                        { "apikey" , GetSetting("API Key") },
                        { "numbers" , numbers.ToString() },
                        { "message" , messageText },
                        { "sender" , GetSetting("Sender") },
                        { "format", "xml" }
                    });
                var result = new XmlDocument();
                result.LoadXml(Encoding.UTF8.GetString(response));

                var sentSms = new SentSmsModel(message);
                sentSms.IsSent = result.DocumentElement.SelectSingleNode("/response/status").InnerText.Trim().Equals("success");
                sentSms.GatewayMessage = sentSms.IsSent ? null : result.DocumentElement.SelectSingleNode("/response/errors/error/message").InnerText.Trim();
                return sentSms;
            }
        }
    }
}
