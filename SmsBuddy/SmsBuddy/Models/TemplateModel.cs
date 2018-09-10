using NullVoidCreations.WpfHelpers.Base;
using PetaPoco;
using System.Collections.Generic;

namespace SmsBuddy.Models
{
    [TableName(nameof(TemplateModel))]
    [PrimaryKey(nameof(Id))]
    class TemplateModel: NotificationBase
    {
        long _id;
        string _name, _template;
        readonly HashSet<string> _fields;

        public TemplateModel()
        {
            _fields = new HashSet<string>();
        }

        #region properties

        public long Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        public string Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
        }

        public IEnumerable<string> Fields
        {
            get { return _fields; }
        }

        #endregion

        public bool AddField(string field)
        {
            var isAdded = _fields.Add(field);
            if (isAdded)
                RaisePropertyChanged(nameof(Fields));

            return isAdded;
        }

        public bool RemoveField(string field)
        {
            var isRemoved = _fields.Remove(field);
            if (isRemoved)
                RaisePropertyChanged(nameof(Fields));

            return isRemoved;
        }
    }
}
