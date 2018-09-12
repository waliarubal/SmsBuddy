using System;

namespace SmsBuddy.Gateway
{
    class Textlocal : GatewayBase
    {
        const string API_KEY = "API Key";
        const string API_BASE_URL = "https://api.textlocal.in/";

        public Textlocal(): base("Textlocal")
        {
            SetParameterNames(API_KEY);
        }

        public override long GetBalance()
        {
            throw new NotImplementedException();
        }

        public override bool SendSms(string text)
        {
            throw new NotImplementedException();
        }
    }
}
