using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.DataStructures;
using System;
using System.Collections.Generic;

namespace SmsBuddy.Models
{
    class SentSmsModel: NotificationBase, IModel
    {
        long _id;
        string _message, _gatewayMessage;
        ExtendedObservableCollection<string> _mobileNumbers;
        DateTime _time;
        bool _isSent;

        public SentSmsModel()
        {
            _mobileNumbers = new ExtendedObservableCollection<string>();
        }

        public SentSmsModel(SmsModel sms, IEnumerable<string> mobileNumbers): this()
        {
            Message = sms.Message;
            Time = DateTime.Now;
            MobileNumbers = new ExtendedObservableCollection<string>(mobileNumbers);
        }

        #region properties

        [BsonId(true)]
        public long Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public ExtendedObservableCollection<string> MobileNumbers
        {
            get { return _mobileNumbers; }
            set { Set(nameof(MobileNumbers), ref _mobileNumbers, value); }
        }

        public string Message
        {
            get { return _message; }
            set { Set(nameof(Message), ref _message, value); }
        }

        public DateTime Time
        {
            get { return _time; }
            set { Set(nameof(Time), ref _time, value); }
        }

        public bool IsSent
        {
            get { return _isSent; }
            set { Set(nameof(IsSent), ref _isSent, value); }
        }

        public string GatewayMessage
        {
            get { return _gatewayMessage; }
            set { Set(nameof(GatewayMessage), ref _gatewayMessage, value); }
        }

        #endregion

        public bool Delete()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<SentSmsModel>();
            return collection.Delete(Id);
        }

        public IEnumerable<IModel> Get()
        {
            var db = Shared.Instance.Database;
            return db.GetCollection<SentSmsModel>().FindAll();
        }

        public bool Save()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<SentSmsModel>();
            var isSaved = collection.Update(this);
            if (isSaved)
                return isSaved;

            Id = collection.Insert(this);
            return true;
        }

        public override bool Equals(object obj)
        {
            var other = obj as SentSmsModel;
            return other != null && other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode();
        }
    }
}
