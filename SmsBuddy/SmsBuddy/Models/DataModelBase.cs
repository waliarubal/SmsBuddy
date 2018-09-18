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

        public abstract void Save();

        public abstract void Delete();

        public abstract IEnumerable<DataModelBase> Get(string searchKeywoards);
    }
}
