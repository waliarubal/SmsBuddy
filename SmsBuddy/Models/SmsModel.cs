﻿using System;
using System.Collections.Generic;
using LiteDB;
using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.Models
{
    class SmsModel: NotificationBase, IModel
    {
        string _mobileNumber, _message;
        long _id;
        bool _repeatDaily;
        TemplateModel _template;

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

        public TemplateModel Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
        }

        public string Message
        {
            get { return _message; }
            set { Set(nameof(Message), ref _message, value); }
        }

        public bool RepeatDaily
        {
            get { return _repeatDaily; }
            set { Set(nameof(RepeatDaily), ref _repeatDaily, value); }
        }

        #endregion

        public bool Delete()
        {
            using (var db = Shared.Instance.Database)
            {
                var collection = db.GetCollection<SmsModel>();
                return collection.Delete(Id);
            }
        }

        public IEnumerable<IModel> Get()
        {
            IEnumerable<SmsModel> sms;
            using (var db = Shared.Instance.Database)
            {
                sms = db.GetCollection<SmsModel>().FindAll();
            }
            return sms;
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(MobileNumber))
                throw new Exception("Mobile number not specified.");
            else if (string.IsNullOrEmpty(Message))
                throw new Exception("Message not specified.");

            using (var db = Shared.Instance.Database)
            {
                var collection = db.GetCollection<SmsModel>();
                var isSaved = collection.Update(this);
                if (isSaved)
                    return isSaved;

                Id = collection.Insert(this);
                return true;
            }
        }
    }
}
