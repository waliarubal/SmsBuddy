﻿namespace SmsBuddy.Models
{
    class SmsModel: ModelBase
    {
        string _mobileNumber, _message;
        TemplateModel _template;

        #region properties

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { Set(nameof(MobileNumber), ref _mobileNumber, value); }
        }

        public TemplateModel Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
        }

        public string Message
        {
            get { return _message; }
            set { Set(nameof(Message), ref _message, value); }
        }

        #endregion
    }
}
