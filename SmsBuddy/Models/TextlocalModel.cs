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

        public override bool Send(SmsModel message)
        {
            var messageText = HttpUtility.UrlEncode(message.Message);
            using (var client = new WebClient())
            {
                var response = client.UploadValues("https://api.textlocal.in/send/",
                    new NameValueCollection
                    {
                        { "apikey" , GetSetting("API Key") },
                        { "numbers" , message.MobileNumber },
                        { "message" , messageText },
                        { "sender" , GetSetting("Sender") },
                        { "format", "xml" }
                    });
                var result = new XmlDocument();
                result.LoadXml(Encoding.UTF8.GetString(response));
                return result.DocumentElement.SelectSingleNode("/response/status").InnerText.Trim().Equals("success");
            }
        }
    }
}
