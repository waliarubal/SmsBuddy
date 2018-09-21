using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        ICommand _initialize, _showChildView;
        Dictionary<string, FrameworkElement> _childViews;
        FrameworkElement _child;
        IEnumerable<ChildViewModelBase> _children;

        #region properties

        public IEnumerable<ChildViewModelBase> ChildViewModels
        {
            get { return _children; }
            private set { Set(nameof(ChildViewModels), ref _children, value); }
        }

        public FrameworkElement ChildView
        {
            get { return _child; }
            private set { Set(nameof(ChildView), ref _child, value); }
        }

        #endregion

        #region commands

        public ICommand InitializeCommand
        {
            get
            {
                if (_initialize == null)
                    _initialize = new RelayCommand(Initialize);

                return _initialize;
            }
        }

        public ICommand ShowChildViewCommand
        {
            get
            {
                if (_showChildView == null)
                    _showChildView = new RelayCommand<ChildViewModelBase>(ShowChildView) { IsSynchronous = true };

                return _showChildView;
            }
        }

        #endregion

        void ShowChildView(ChildViewModelBase viewModel)
        {
            var view = _childViews[viewModel.Name];
            if (view == null)
            {
                view = viewModel.GetView();
                _childViews[viewModel.Name] = view;
            }
            ChildView = view;
        }

        void Initialize()
        {
            _childViews = new Dictionary<string, FrameworkElement>();
            
            var childVmType = typeof(ChildViewModelBase);
            var childViewModels = new List<ChildViewModelBase>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsClass && type.IsSubclassOf(childVmType))
                {
                    var viewModel = Activator.CreateInstance(type) as ChildViewModelBase;
                    if (viewModel == null)
                        return;

                    _childViews.Add(viewModel.Name, null);
                    childViewModels.Add(viewModel);
                }
            }
            ChildViewModels = childViewModels;
        }
    }
}
