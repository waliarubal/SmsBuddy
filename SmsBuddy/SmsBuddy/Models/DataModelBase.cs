using NullVoidCreations.WpfHelpers.Base;
using System.Collections.Generic;
using System.Data;

namespace SmsBuddy.Models
{
    abstract class DataModelBase: NotificationBase
    {
        IDbConnection _connection;

        #region properties

        protected IDbConnection Connection
        {
            get { return _connection; }
        }

        #endregion

        public abstract bool Save();

        public abstract bool Delete();

        public abstract IEnumerable<DataModelBase> Get();
    }
}
