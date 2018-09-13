using System;

namespace SmsBuddy.Gateway
{
    class Textlocal : GatewayBase
    {
        const string API_BASE_URL = "https://api.textlocal.in/";

        public Textlocal(): base("Textlocal")
        {
            SetParameterNames("Base URL", "API Key");
            SetParameterValue("Base URL", API_BASE_URL);
        }

        public override long GetBalance()
        {
            throw new NotImplementedException();
        }

        public override bool SendSms(string text, params string[] mobileNumbers)
        {
            throw new NotImplementedException();
        }
    }
}
