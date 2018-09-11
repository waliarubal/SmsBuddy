using NullVoidCreations.WpfHelpers.Base;
using System.Collections.Generic;

namespace SmsBuddy.Models
{
    abstract class ModelBase: NotificationBase
    {
        internal abstract IEnumerable<ModelBase> Get();

        internal abstract bool Save();

        internal abstract bool Delete();
    }
}
