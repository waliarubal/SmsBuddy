using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmsBuddy.Models
{
    class TemplateModel: NotificationBase, IModel, IEquatable<TemplateModel>
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
            set
            {
                if (Set(nameof(Message), ref _message, value))
                    RaisePropertyChanged(nameof(RemainingLength));
            }
        }

        public int MaxLength
        {
            get { return 160; }
        }

        public int RemainingLength
        {
            get { return MaxLength - (string.IsNullOrWhiteSpace(Message) ? 0 : Message.Length); }
        }

        #endregion

        public bool Delete()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<TemplateModel>();
            return collection.Delete(Id);
        }

        public IEnumerable<IModel> Get()
        {
            var db = Shared.Instance.Database;
            return db.GetCollection<TemplateModel>().FindAll();
        }

        public bool Save()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<TemplateModel>();
            var isSaved = collection.Update(this);
            if (isSaved)
                return isSaved;

            Id = collection.Insert(this);
            return true;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(TemplateModel other)
        {
            return other != null && other.Id.Equals(Id);
        }
    }
}
