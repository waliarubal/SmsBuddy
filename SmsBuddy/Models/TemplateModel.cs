using System.Collections.ObjectModel;

namespace SmsBuddy.Models
{
    class TemplateModel: ModelBase
    {
        string _name, _message;
        ObservableCollection<string> _fields;

        public TemplateModel()
        {
            _fields = new ObservableCollection<string>();
        }

        #region properties

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        public ObservableCollection<string> Fields
        {
            get { return _fields; }
            set { Set(nameof(Fields), ref _fields, value); }
        }

        public string Message
        {
            get { return _message; }
            set { Set(nameof(Message), ref _message, value); }
        }

        #endregion
    }
}
