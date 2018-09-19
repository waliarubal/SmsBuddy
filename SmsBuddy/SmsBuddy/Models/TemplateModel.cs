using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmsBuddy.Models
{
    class TemplateModel : DataModelBase
    {
        string _name, _template;
        HashSet<string> _fields;

        public TemplateModel()
        {
            _fields = new HashSet<string>();
        }

        #region properties

        public IEnumerable<string> Fields
        {
            get { return _fields; }
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

        #endregion

        public void AddField(string name)
        {
            if (_fields.Add(name))
                RaisePropertyChanged(nameof(Fields));
        }

        public void RemoveField(string name)
        {
            if (_fields.Remove(name))
                RaisePropertyChanged(nameof(Fields));
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<DataModelBase> Get(string searchKeywoards)
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
