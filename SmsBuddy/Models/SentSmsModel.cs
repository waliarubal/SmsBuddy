using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using System;
using System.Collections.Generic;

namespace SmsBuddy.Models
{
    class SentSmsModel: NotificationBase, IModel
    {
        long _id;
        string _mobileNumber, _message, _gatewayMessage;
        DateTime _time;
        bool _isSent;


        public SentSmsModel()
        {

        }

        public SentSmsModel(SmsModel sms): this()
        {
            MobileNumber = sms.MobileNumber;
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

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { Set(nameof(MobileNumber), ref _mobileNumber, value); }
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
    }
}
