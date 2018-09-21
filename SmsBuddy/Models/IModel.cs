using System.Collections.Generic;

namespace SmsBuddy.Models
{
    interface IModel
    {
        bool Save();

        bool Delete();

        IEnumerable<IModel> Get();
    }
}
