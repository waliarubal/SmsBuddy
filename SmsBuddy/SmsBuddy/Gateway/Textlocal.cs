using System;

namespace SmsBuddy.Gateway
{
    class Textlocal : GatewayBase
    {
        const string API_BASE_URL = "https://api.textlocal.in/";

        public Textlocal(): base("Textlocal")
        {
            Version = new Version(18, 9, 13, 21);
            ProviderUrl = new Uri("https://www.textlocal.in/");
            SetParameterNames("API Key");
        }

        public override bool SendSms(string text, params string[] mobileNumbers)
        {
            throw new NotImplementedException();
        }
    }
}
