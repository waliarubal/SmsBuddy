using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.ViewModels
{
    abstract class ChildViewModelBase: ViewModelBase
    {
        string _errorMessage;

        protected ChildViewModelBase(string name, string iconFile)
        {
            Name = name;
            Icon = string.Format("pack://application:,,,/Assets/Images/{0}", iconFile);
        }

        #region properties
        
        public string ErrorMessage
        {
            get { return _errorMessage; }
            protected set
            {
                if (Set(nameof(ErrorMessage), ref _errorMessage, value))
                    RaisePropertyChanged(nameof(IsHavingError));
            }
        }

        public bool IsHavingError
        {
            get { return !string.IsNullOrEmpty(ErrorMessage); }
        }

        public string Name { get; }

        public string Icon { get; }

        #endregion
    }
}
