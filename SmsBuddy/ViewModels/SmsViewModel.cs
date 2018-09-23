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
        ICommand _refresh, _new;

        public SmsViewModel() : base("Messages", "sms-32.png")
        {
            
        }

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

        #endregion

        void New()
        {
            Sms = new SmsModel();
        }

        void Refresh()
        {
            Templates = new TemplateModel().Get() as IEnumerable<TemplateModel>;
        }
    }
}
