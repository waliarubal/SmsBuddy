using NullVoidCreations.WpfHelpers.Base;

namespace SmsBuddy.ViewModels
{
    abstract class ChildViewModelBase: ViewModelBase
    {
        protected ChildViewModelBase(string name, string iconFile)
        {
            Name = name;
            Icon = string.Format("pack://application:,,,/Assets/Images/{0}", iconFile);
        }

        #region properties

        public string Name { get; }

        public string Icon { get; }

        #endregion
    }
}
