using LiteDB;
using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.Models
{
    abstract class ModelBase: NotificationBase
    {
        BsonValue _id;

        #region properties

        public BsonValue Id
        {
            get { return _id; }
            private set { Set(nameof(Id), ref _id, value); }
        }

        #endregion

        public bool Save()
        {
            using(var database = Shared.Instance.GetDatabase())
            {
                var collection = database.GetCollection<ModelBase>(GetType().Name);
                return Id.IsNull ? (Id = collection.Insert(this)).IsNull : collection.Update(Id, this);
            }
        }

        public bool Delete()
        {
            using (var database = Shared.Instance.GetDatabase())
            {
                var collection = database.GetCollection<ModelBase>(GetType().Name);
                return Id.IsNull ? false : collection.Delete(Id);
            }
        }
    }
}
