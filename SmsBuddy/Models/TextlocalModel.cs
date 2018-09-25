using System;

namespace SmsBuddy.Models
{
    class TextlocalModel : SmsGatewayBase
    {
        public TextlocalModel(): base("TextLocal", "https://www.textlocal.in/") { }

        public override bool Send(SmsModel message)
        {
            throw new NotImplementedException();
        }
    }
}
