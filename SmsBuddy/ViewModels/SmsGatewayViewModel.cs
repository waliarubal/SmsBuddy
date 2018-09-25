using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class SmsGatewayViewModel: ChildViewModelBase
    {
        IEnumerable<SmsGatewayBase> _providers, _gateways;
        SmsGatewayBase _gateway, _provider;
        ICommand _new, _save, _delete, _refresh;

        public SmsGatewayViewModel(): base("SMS Gateway", "server-32.png") {  }

        #region properties

        public SmsGatewayBase Gateway
        {
            get { return _gateway; }
            set { Set(nameof(Gateway), ref _gateway, value); }
        }

        public IEnumerable<SmsGatewayBase> Gateways
        {
            get { return _gateways; }
            private set { Set(nameof(Gateways), ref _gateways, value); }
        }

        public SmsGatewayBase Provider
        {
            get { return _provider; }
            set
            {
                if(Set(nameof(Provider), ref _provider, value) && value != null)
                {
                    Gateway = Activator.CreateInstance(value.GetType()) as SmsGatewayBase;
                }
            }
        }

        public IEnumerable<SmsGatewayBase> Providers
        {
            get
            {
                if (_providers == null)
                {
                    var gatewayBaseType = typeof(SmsGatewayBase);
                    var gateways = new List<SmsGatewayBase>();
                    foreach(var type in Assembly.GetExecutingAssembly().GetTypes())
                    {
                        if (type.IsClass && type.IsSubclassOf(gatewayBaseType))
                        {
                            var gateway = Activator.CreateInstance(type) as SmsGatewayBase;
                            if (gateway == null)
                                continue;

                            gateways.Add(gateway);
                        }
                    }
                    _providers = gateways;
                }

                return _providers;
            }
        }

        #endregion

        #region commands

        public ICommand NewCommand
        {
            get
            {
                if (_new == null)
                    _new = new RelayCommand(New);

                return _new;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_save == null)
                    _save = new RelayCommand(Save);

                return _save;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_delete == null)
                    _delete = new RelayCommand(Delete);

                return _delete;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand(Refresh);

                return _refresh;
            }
        }

        #endregion

        void Refresh()
        {
            ErrorMessage = null;
            Gateways = Shared.Instance.Database.GetCollection<SmsGatewayBase>().FindAll();
        }

        void New()
        {
            ErrorMessage = null;
            Provider = null;
            Gateway = null;
        }

        void Save()
        {
            ErrorMessage = null;

            if (Gateway == null)
                ErrorMessage = "Select a provider.";
            else if (string.IsNullOrEmpty(Gateway.AccountName))
                ErrorMessage = "Account name not specified.";
            else if (Gateway.Settings != null)
            {
                foreach (var setting in Gateway.Settings)
                {
                    if (string.IsNullOrEmpty(setting.Second))
                    {
                        ErrorMessage = "All provider settings not specified.";
                        return;
                    }
                }
            }
            
            Gateway.Save();
            New();
            Refresh();
        }

        void Delete()
        {
            ErrorMessage = null;

            if (Gateway == null)
                ErrorMessage = "Gateway not selected.";
            else
            {
                Gateway.Delete();
                New();
                Refresh();
            }
        }
    }
}
