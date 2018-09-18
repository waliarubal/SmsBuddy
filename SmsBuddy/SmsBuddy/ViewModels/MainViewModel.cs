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
        readonly Dictionary<ChildViewModelBase, FrameworkElement> _childrenCache;
        IEnumerable<ChildViewModelBase> _children;
        FrameworkElement _child;
        ICommand _initialize, _showChild;

        public MainViewModel()
        {
            _childrenCache = new Dictionary<ChildViewModelBase, FrameworkElement>();
        }

        #region properties

        public IEnumerable<ChildViewModelBase> Children
        {
            get { return _children; }
            private set { Set(nameof(Children), ref _children, value); }
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
            var view = _childrenCache[childVm];
            if (view == null)
            {
                view = childVm.GetView();
                _childrenCache[childVm] = view;
            }
            Child = view;
        }

        void Initialize()
        {
            var vmBaseType = typeof(ChildViewModelBase);

            _childrenCache.Clear();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsClass && type.IsSubclassOf(vmBaseType))
                {
                    var childVm = Activator.CreateInstance(type) as ChildViewModelBase;
                    if (childVm == null)
                        continue;

                    _childrenCache.Add(childVm, null);
                }
            }
            Children = _childrenCache.Keys;
        }
    }
}
