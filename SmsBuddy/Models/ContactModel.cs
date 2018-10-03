using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace SmsBuddy.Models
{
    class ContactModel : NotificationBase, IModel
    {
        long _id;
        string _firstName, _lastName, _company, _mobileNumbersString;
        ObservableCollection<string> _mobileNumbers;

        public ContactModel()
        {
            _mobileNumbers = new ObservableCollection<string>();
            _mobileNumbers.CollectionChanged += OnMobileNumbers_CollectionChanged;
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

        public ObservableCollection<string> MobileNumbers
        {
            get { return _mobileNumbers; }
            set
            {
                var old = _mobileNumbers;
                if (old != null)
                    old.CollectionChanged -= OnMobileNumbers_CollectionChanged;

                if (Set(nameof(MobileNumbers), ref _mobileNumbers, value) && value != null)
                {
                    _mobileNumbers.CollectionChanged += OnMobileNumbers_CollectionChanged;
                    OnMobileNumbers_CollectionChanged(this, null);
                }
            }
        }

        public string MobileNumbersString
        {
            get { return _mobileNumbersString; }
            private set { Set(nameof(MobileNumbersString), ref _mobileNumbersString, value); }
        }

        #endregion

        void OnMobileNumbers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var mobilesString = new StringBuilder();
            foreach (var number in MobileNumbers)
                mobilesString.AppendFormat("{0}, ", number);

            MobileNumbersString = mobilesString.Length > 0 ? mobilesString.ToString(0, mobilesString.Length - 2) : null;
        }

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

        public override string ToString()
        {
            var mobiles = MobileNumbersString;
            return mobiles == null ?  Name : string.Format("{0} ({1})", Name, mobiles);
        }

        public override bool Equals(object obj)
        {
            var other = obj as ContactModel;
            var mobiles = MobileNumbersString;
            return mobiles != null && other != null && mobiles.Equals(other.MobileNumbersString);
        }

        public override int GetHashCode()
        {
            var mobiles = MobileNumbersString;
            return mobiles == null ? 0 : mobiles.GetHashCode();
        }
    }
}
