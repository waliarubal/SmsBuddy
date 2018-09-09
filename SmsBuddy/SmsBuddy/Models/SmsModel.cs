using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.Models
{
    class SmsModel: NotificationBase
    {
        TemplateModel _template;

        #region properties

        public TemplateModel Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
        }

        #endregion
    }
}
