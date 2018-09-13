using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        FrameworkElement _childView;
        Dictionary<Type, FrameworkElement> _cachedViews;
        ICommand _showView;

        public MainViewModel()
        {
            _cachedViews = new Dictionary<Type, FrameworkElement>();
        }

        #region properties

        public FrameworkElement ChildView
        {
            get { return _childView; }
            private set { Set(nameof(ChildView), ref _childView, value); }
        }

        #endregion

        #region commands

        public ICommand ShowViewCommand
        {
            get
            {
                if (_showView == null)
                    _showView = new RelayCommand<Type>(ShowView) { IsSynchronous = true };

                return _showView;
            }
        }

        #endregion

        void ShowView(Type viewModelType)
        {
            if (viewModelType == null)
                return;

            if (_cachedViews.ContainsKey(viewModelType))
                ChildView = _cachedViews[viewModelType];
            else
            {
                var viewModel = Activator.CreateInstance(viewModelType) as ViewModelBase;
                if (viewModel == null)
                    return;

                var view = viewModel.GetView();
                _cachedViews.Add(viewModelType, view);
                ChildView = view;
            }
        }
    }
}
