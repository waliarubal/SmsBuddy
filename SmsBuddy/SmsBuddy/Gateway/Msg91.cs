using System;

namespace SmsBuddy.Gateway
{
    class Msg91 : GatewayBase
    {
        const string API_BASE_URL = "http://api.msg91.com/api/";

        public Msg91(): base("MSG91")
        {
            Version = new Version(18, 9, 13, 21);
            ProviderUrl = new Uri("https://msg91.com/");
            SetParameterNames("Auth Key", "Country");
        }

        public override bool SendSms(string text, params string[] mobileNumbers)
        {
            throw new NotImplementedException();
        }
    }
}
