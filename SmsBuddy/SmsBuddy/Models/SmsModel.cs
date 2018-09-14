using NullVoidCreations.WpfHelpers.Base;
using System;

namespace SmsBuddy.Models
{
    class SmsModel: NotificationBase
    {
        string _text, _mobiles;

        #region properties

        public string Mobiles
        {
            get { return _mobiles; }
            set { Set(nameof(Mobiles), ref _mobiles, value); }
        }

        public string Text
        {
            get { return _text; }
            set { Set(nameof(Text), ref _text, value); }
        }

        #endregion

        public string[] GetMobileNumbers()
        {
            var mobiles = Mobiles;
            if (string.IsNullOrWhiteSpace(mobiles))
                return new string[0];

            mobiles = mobiles.Replace("+", string.Empty);
            mobiles = mobiles.Replace(" ", string.Empty);
            return mobiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
