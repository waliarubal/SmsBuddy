using NullVoidCreations.WpfHelpers.Base;
using SmsBuddy.Models;

namespace SmsBuddy.ViewModels
{
    class RecordsViewModel: ViewModelBase
    {
        ModelBase _record;
        string _searchKeywoard;

        #region properties

        public ModelBase Record
        {
            get { return _record; }
            set { Set(nameof(Record), ref _record, value); }
        }

        public string SearchKeywoard
        {
            get { return _searchKeywoard; }
            set { Set(nameof(SearchKeywoard), ref _searchKeywoard, value); }
        }

        #endregion
    }
}
