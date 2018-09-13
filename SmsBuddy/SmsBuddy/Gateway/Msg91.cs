using System;

namespace SmsBuddy.Gateway
{
    class Msg91 : GatewayBase
    {
        const string API_BASE_URL = "http://api.msg91.com/api/";

        public Msg91(): base("MSG91")
        {
            SetParameterNames("Base URL", "Auth Key");
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
