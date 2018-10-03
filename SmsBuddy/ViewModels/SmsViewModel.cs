using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class SmsViewModel: ChildViewModelBase
    {
        SmsModel _sms;
        IEnumerable<TemplateModel> _templates;
        IEnumerable<SmsGatewayBase> _gateways;
        IEnumerable<SmsModel> _messages;
        IEnumerable<int> _hours, _minutes;
        ICommand _refresh, _new, _send, _save, _delete;

        public SmsViewModel() : base("Messages", "sms-32.png") { }

        #region properties

        public SmsModel Sms
        {
            get { return _sms; }
            set { Set(nameof(Sms), ref _sms, value); }
        }

        public IEnumerable<TemplateModel> Templates
        {
            get { return _templates; }
            private set { Set(nameof(Templates), ref _templates, value); }
        }

        public IEnumerable<SmsModel> Messages
        {
            get { return _messages; }
            private set { Set(nameof(Messages), ref _messages, value); }
        }

        public IEnumerable<SmsGatewayBase> Gateways
        {
            get { return _gateways; }
            private set { Set(nameof(Gateways), ref _gateways, value); }
        }

        public IEnumerable<int> Hours
        {
            get
            {
                if (_hours == null)
                {
                    var hours = new List<int>();
                    for (var hour = 0; hour < 24; hour++)
                        hours.Add(hour);
                    _hours = hours;
                }

                return _hours;
            }
        }

        public IEnumerable<int> Minutes
        {
            get
            {
                if (_minutes == null)
                {
                    var minutes = new List<int>();
                    for (var minute = 0; minute < 60; minute++)
                        minutes.Add(minute);
                    _minutes = minutes;
                }

                return _minutes;
            }
        }

        #endregion

        #region commands

        public ICommand RefreshCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand(Refresh);

                return _refresh;
            }
        }

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
                    _delete = new RelayCommand(Send);

                return _delete;
            }
        }

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

        void Delete()
        {
            ErrorMessage = null;

            if (Sms == null)
                ErrorMessage = "Select or create a new message first.";
            else
            {
                Sms.Delete();
                Refresh();
                New();
            }
        }

        void New()
        {
            ErrorMessage = null;
            Sms = new SmsModel();
        }

        void Save()
        {
            ErrorMessage = null;

            if (Sms == null)
                ErrorMessage = "Select or create a new message.";
            else if (Sms.MobileNumbers.Count == 0)
                ErrorMessage = "Mobile number not specified.";
            else if (Sms.Gateway == null)
                ErrorMessage = "SMS gateway not selected.";
            else if (Sms.Template == null)
                ErrorMessage = "Template not selected.";
            else if (string.IsNullOrEmpty(Sms.Message))
                ErrorMessage = "Message not specified.";
            else
            {
                Sms.Save();
                Refresh();
                New();
            }
        }

        void Send()
        {
            ErrorMessage = null;

            if (Sms == null)
                ErrorMessage = "Select or create a new message.";
            else if (Sms.MobileNumbers.Count == 0)
                ErrorMessage = "Mobile number not specified.";
            else if (Sms.Gateway == null)
                ErrorMessage = "SMS gateway not selected.";
            else if (Sms.Template == null)
                ErrorMessage = "Template not selected.";
            else if (string.IsNullOrEmpty(Sms.Message))
                ErrorMessage = "Message not specified.";
            else
            {
                var sentMessage = Sms.Gateway.Send(Sms, Sms.MobileNumbers);
                ErrorMessage = sentMessage.IsSent ? null : sentMessage.GatewayMessage;
                sentMessage.Save();
            }
        }

        void Refresh()
        {
            ErrorMessage = null;
            Gateways = Shared.Instance.Database.GetCollection<SmsGatewayBase>().FindAll();
            Templates = new TemplateModel().Get() as IEnumerable<TemplateModel>;
            Messages = new SmsModel().Get() as IEnumerable<SmsModel>;
        }
    }
}
