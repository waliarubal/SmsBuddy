using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.ViewModels
{
    abstract class ChildViewModelBase: ViewModelBase
    {
        string _title, _icon;
        bool _isInitialized;

        protected ChildViewModelBase(string title)
        {
            Title = title;
        }

        #region properties

        public string Title
        {
            get { return _title; }
            private set { Set(nameof(Title), ref _title, value); }
        }

        public string Icon
        {
            get { return _icon; }
            protected set { Set(nameof(Icon), ref _icon, value); }
        }

        #endregion
    }
}
