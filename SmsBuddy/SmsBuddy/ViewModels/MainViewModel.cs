using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        Dictionary<ChildViewModelBase, FrameworkElement> _children;
        FrameworkElement _child;
        ICommand _initialize, _showChild;

        #region properties

        public IEnumerable<ChildViewModelBase> Children
        {
            get { return _children.Keys; }
        }

        public FrameworkElement Child
        {
            get { return _child; }
            private set { Set(nameof(Child), ref _child, value); }
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

        public ICommand ShowChildCommand
        {
            get
            {
                if (_showChild == null)
                    _showChild = new RelayCommand<ChildViewModelBase>(ShowChild) { IsSynchronous = true };

                return _showChild;
            }
        }

        #endregion

        void ShowChild(ChildViewModelBase childVm)
        {
            var view = _children[childVm];
            if (view == null)
            {
                view = childVm.GetView();
                _children[childVm] = view;
            }
            Child = view;
        }

        void Initialize()
        {
            var children = new Dictionary<ChildViewModelBase, FrameworkElement>();
            var vmBaseType = typeof(ChildViewModelBase);
            foreach(Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(vmBaseType))
                {
                    var childVm = Activator.CreateInstance(type) as ChildViewModelBase;
                    if (childVm == null)
                        continue;

                    children.Add(childVm, null);
                }
            }
            _children = children;
            RaisePropertyChanged(nameof(Children));
        }
    }
}
