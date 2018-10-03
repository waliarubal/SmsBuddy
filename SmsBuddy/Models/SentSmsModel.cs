using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmsBuddy.Models
{
    class SentSmsModel: NotificationBase, IModel
    {
        long _id;
        string _message, _gatewayMessage;
        ObservableCollection<string> _mobileNumbers, _mobileNumbersScheduled;
        DateTime _time;
        bool _isSent;

        public SentSmsModel()
        {
            _mobileNumbers = new ObservableCollection<string>();
            _mobileNumbersScheduled = new ObservableCollection<string>();
        }

        public SentSmsModel(SmsModel sms): this()
        {
            MobileNumbers = sms.MobileNumbers;
            MobileNumbersScheduled = sms.MobileNumbersScheduled;
            Message = sms.Message;
            Time = DateTime.Now;
        }

        #region properties

        [BsonId(true)]
        public long Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public ObservableCollection<string> MobileNumbers
        {
            get { return _mobileNumbers; }
            set { Set(nameof(MobileNumbers), ref _mobileNumbers, value); }
        }

        public ObservableCollection<string> MobileNumbersScheduled
        {
            get { return _mobileNumbersScheduled; }
            set { Set(nameof(MobileNumbersScheduled), ref _mobileNumbersScheduled, value); }
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
