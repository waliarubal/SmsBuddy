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
        IEnumerable<SmsModel> _messages;
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
                New();
                Refresh();
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
            else if (string.IsNullOrEmpty(Sms.MobileNumber))
                ErrorMessage = "Mobile number not specified.";
            else if (Sms.Template == null)
                ErrorMessage = "Template not selected.";
            else if (string.IsNullOrEmpty(Sms.Message))
                ErrorMessage = "Message not specified.";
            else
            {
                Sms.Save();
                New();
                Refresh();
            }
        }

        void Send()
        {
            ErrorMessage = null;
        }

        void Refresh()
        {
            ErrorMessage = null;
            Templates = new TemplateModel().Get() as IEnumerable<TemplateModel>;
            Messages = new SmsModel().Get() as IEnumerable<SmsModel>;
        }
    }
}
