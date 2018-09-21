using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmsBuddy.Models
{
    class TemplateModel: NotificationBase, IModel
    {
        string _name, _message;
        long _id;
        ObservableCollection<string> _fields;

        public TemplateModel()
        {
            _fields = new ObservableCollection<string>();
        }

        #region properties

        [BsonId(true)]
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

        public bool Delete()
        {
            using (var db = Shared.Instance.Database)
            {
                var collection = db.GetCollection<TemplateModel>();
                return collection.Delete(Id);
            }
        }

        public IEnumerable<IModel> Get()
        {
            IEnumerable<TemplateModel> templates;
            using (var db = Shared.Instance.Database)
            {
               templates = db.GetCollection<TemplateModel>().FindAll();
            }
            return templates;
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(Name))
                throw new Exception("Name not specified.");
            else if (string.IsNullOrEmpty(Message))
                throw new Exception("Message not specified.");

            using (var db = Shared.Instance.Database)
            {
                var collection = db.GetCollection<TemplateModel>();
                var isSaved = collection.Update(this);
                if (isSaved)
                    return isSaved;

                Id = collection.Insert(this);
                return true;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
