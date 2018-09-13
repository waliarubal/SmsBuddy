using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Gateway;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class GatewayViewModel : ViewModelBase
    {
        IEnumerable<GatewayBase> _gateways;
        ICommand _iniatialize;
        bool _isInitialized;

        public GatewayViewModel()
        {
            Shared.Instance.PropertyChanged += (object sender, PropertyChangedEventArgs e) => RaisePropertyChanged(e.PropertyName);
        }

        #region properties

        public GatewayBase Gateway
        {
            get { return Shared.Instance.Gateway; }
            set { Shared.Instance.Gateway = value; }
        }

        public IEnumerable<GatewayBase> Gateways
        {
            get { return _gateways; }
            private set { Set(nameof(Gateways), ref _gateways, value); }
        }

        #endregion

        #region commands

        public ICommand IniatilizeCommand
        {
            get
            {
                if (_iniatialize == null)
                    _iniatialize = new RelayCommand(Initialize);

                return _iniatialize;
            }
        }

        #endregion

        void Initialize()
        {
            if (_isInitialized)
                return;

            var gatewayBase = typeof(GatewayBase);
            var gateways = new List<GatewayBase>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                if (type.IsClass && 
                    type.IsSubclassOf(gatewayBase) && 
                    Activator.CreateInstance(type) is GatewayBase gateway)
                    gateways.Add(gateway);

            Gateways = gateways;

            _isInitialized = true;
        }
    }
}
