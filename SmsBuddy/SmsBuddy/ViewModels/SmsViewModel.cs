using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Gateway;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class SmsViewModel: ViewModelBase
    {
        ICommand _send, _clear;
        GatewayBase _gateway;

        #region properties

        public GatewayBase Gateway
        {
            get { return _gateway; }
        }

        #endregion

        #region commands

        public ICommand SendCommand
        {
            get
            {
                if (_send == null)
                    _send = new RelayCommand(Send);

                return _send;
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                if (_clear == null)
                    _clear = new RelayCommand(Clear);

                return _clear;
            }
        }

        #endregion

        void Send()
        {

        }

        void Clear()
        {

        }
    }
}
