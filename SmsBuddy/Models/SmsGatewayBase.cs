using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.DataStructures;
using System;
using System.Collections.Generic;

namespace SmsBuddy.Models
{
    abstract class SmsGatewayBase : NotificationBase, IModel
    {
        IEnumerable<Doublet<string, string>> _settings;
        string _accountName;
        long _id;

        protected SmsGatewayBase(string name, string providerWebsite)
        {
            Name = name;
            ProviderWebsite = new Uri(providerWebsite, UriKind.Absolute);
        }

        #region properties

        [BsonId(true)]
        public long Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public string Name { get; }

        public string AccountName
        {
            get { return _accountName; }
            set { Set(nameof(AccountName), ref _accountName, value); }
        }

        public Uri ProviderWebsite { get; }

        public IEnumerable<Doublet<string, string>> Settings
        {
            get { return _settings; }
            protected set { Set(nameof(Settings), ref _settings, value); }
        }

        #endregion

        protected string GetSetting(string key)
        {
            if (string.IsNullOrEmpty(key) || Settings == null)
                return default(string);

            foreach (var setting in Settings)
                if (setting.First.Equals(key))
                    return setting.Second;

            return default(string);
        }

        public bool Delete()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<SmsGatewayBase>();
            return collection.Delete(Id);
        }

        public IEnumerable<IModel> Get()
        {
            var db = Shared.Instance.Database;
            return db.GetCollection<SmsGatewayBase>().FindAll();
        }

        public bool Save()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<SmsGatewayBase>();
            var isSaved = collection.Update(this);
            if (isSaved)
                return isSaved;

            Id = collection.Insert(this);
            return true;
        }

        public abstract SentSmsModel Send(SmsModel message, IEnumerable<string> mobileNumbers);

        public override string ToString()
        {
            return string.IsNullOrEmpty(AccountName) ? Name : string.Format("{0} ({1})", Name, AccountName);
        }

        public override bool Equals(object obj)
        {
            var other = obj as SmsGatewayBase;
            return other != null && other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode();
        }
    }
}
