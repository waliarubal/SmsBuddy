using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Gateway;
using SmsBuddy.Models;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class SmsViewModel: ViewModelBase
    {
        ICommand _send, _clear;
        SmsModel _sms;

        public SmsViewModel()
        {
            Clear();
        }

        #region properties

        public GatewayBase Gateway
        {
            get { return Shared.Instance.Gateway; }
        }

        public SmsModel Sms
        {
            get { return _sms; }
            set { Set(nameof(Sms), ref _sms, value); }
        }

        #endregion

        #region commands

        public ICommand SendCommand
        {
            get
            {
                if (_send == null)
                    _send = new RelayCommand<SmsModel>(Send);

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

        void Send(SmsModel sms)
        {
            if (Gateway == null)
                return;

            Gateway.SendSms(sms.Text, sms.GetMobileNumbers());
        }

        void Clear()
        {
            Sms = new SmsModel();
        }
    }
}
