using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.Models
{
    class TemplateModel: NotificationBase
    {
        string _name;

        #region properties

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        #endregion
    }
}
