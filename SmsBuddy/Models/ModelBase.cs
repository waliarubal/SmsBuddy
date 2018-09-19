using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.Models
{
    abstract class ModelBase: NotificationBase
    {
        long _id;

        #region properties

        public long Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        #endregion
    }
}
