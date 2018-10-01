using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using System.Collections.Generic;

namespace SmsBuddy.Models
{
    class ContactModel : NotificationBase, IModel
    {
        long _id;
        string _mobileNumber, _firstName, _lastName, _company;

        #region properties

        [BsonId(true)]
        public long Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public string Name
        {
            get { return string.Format("{0} {1}", FirstName, LastName).Trim(); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (Set(nameof(LastName), ref _firstName, value))
                    RaisePropertyChanged(nameof(Name));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (Set(nameof(LastName), ref _lastName, value))
                    RaisePropertyChanged(nameof(Name));
            }
        }

        public string Company
        {
            get { return _company; }
            set { Set(nameof(Company), ref _company, value); }
        }

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { Set(nameof(MobileNumber), ref _mobileNumber, value); }
        }

        #endregion

        public bool Delete()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<ContactModel>();
            return collection.Delete(Id);
        }

        public IEnumerable<IModel> Get()
        {
            var db = Shared.Instance.Database;
            return db.GetCollection<ContactModel>().FindAll();
        }

        public bool Save()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<ContactModel>();
            var isSaved = collection.Update(this);
            if (isSaved)
                return isSaved;

            Id = collection.Insert(this);
            return true;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ContactModel;
            return other != null && other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode();
        }
    }
}
