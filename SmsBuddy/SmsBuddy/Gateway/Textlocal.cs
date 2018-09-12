using System;
using System.Collections.Generic;

namespace SmsBuddy.Gateway
{
    class Textlocal : GatewayBase
    {
        public Textlocal(): base("Textlocal")
        {
            var parameterNames = new List<string> { "Dummy" };
            SetParameterNames(parameterNames);
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
