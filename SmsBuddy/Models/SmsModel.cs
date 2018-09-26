using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using LiteDB;
using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.DataStructures;

namespace SmsBuddy.Models
{
    class SmsModel: NotificationBase, IModel
    {
        string _mobileNumber, _message;
        long _id;
        bool _repeatDaily;
        int _hour, _minute;
        TemplateModel _template;
        SmsGatewayBase _gateway;
        IEnumerable<Doublet<string, string>> _fields;

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
            set
            {
                if(Set(nameof(Template), ref _template, value) && 
                    value != null && 
                    value.Fields != null)
                {
                    var fields = new List<Doublet<string, string>>();
                    foreach (var field in value.Fields)
                    {
                        var messageField = new Doublet<string, string>(field, null);
                        messageField.PropertyChanged += (object sender, PropertyChangedEventArgs e) => Message = GetMessage();
                        fields.Add(messageField);
                    }
                        
                    Fields = fields;
                }
            }
        }

        public SmsGatewayBase Gateway
        {
            get { return _gateway; }
            set { Set(nameof(Gateway), ref _gateway, value); }
        }

        public string Message
        {
            get { return _message; }
            private set { Set(nameof(Message), ref _message, value); }
        }

        public bool RepeatDaily
        {
            get { return _repeatDaily; }
            set { Set(nameof(RepeatDaily), ref _repeatDaily, value); }
        }

        public int Hour
        {
            get { return _hour; }
            set { Set(nameof(Hour), ref _hour, value); }
        }

        public int Minute
        {
            get { return _minute; }
            set { Set(nameof(Minute), ref _minute, value); }
        }

        public IEnumerable<Doublet<string, string>> Fields
        {
            get { return _fields; }
            private set { Set(nameof(Fields), ref _fields, value); }
        }

        #endregion

        string GetMessage()
        {
            if (Template == null || Template.Message == null || Fields == null)
                return string.Empty;

            var messageBuilder = new StringBuilder(Template.Message);
            foreach (var field in Fields)
                messageBuilder.Replace(string.Format("<<{0}>>", field.First), field.Second);
            return messageBuilder.ToString();
        }

        public bool Delete()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<SmsModel>();
            return collection.Delete(Id);
        }

        public IEnumerable<IModel> Get()
        {
            var db = Shared.Instance.Database;
            return db.GetCollection<SmsModel>().FindAll();
        }

        public bool Save()
        {
            var db = Shared.Instance.Database;
            var collection = db.GetCollection<SmsModel>();
            var isSaved = collection.Update(this);
            if (isSaved)
                return isSaved;

            Id = collection.Insert(this);
            return true;
        }

        public override bool Equals(object obj)
        {
            var other = obj as SmsModel;
            return other != null && other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode();
        }
    }
}
