using NullVoidCreations.WpfHelpers.Base;
using System.Collections.Generic;

namespace SmsBuddy.Models
{
    public abstract class ModelBase: NotificationBase
    {
        long _id;

        #region properties

        public long Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        #endregion

        public abstract void Save();

        public abstract void Delete();

        public abstract IEnumerable<ModelBase> Get(string searchKeywoard);
    }
}
