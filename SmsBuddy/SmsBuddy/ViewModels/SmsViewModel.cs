using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Gateway;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class SmsViewModel: ViewModelBase
    {
        ICommand _send;
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

        #endregion

        void Send()
        {

        }
    }
}
