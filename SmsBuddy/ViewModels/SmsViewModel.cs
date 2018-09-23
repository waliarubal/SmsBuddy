using NullVoidCreations.WpfHelpers.Commands;
using NullVoidCreations.WpfHelpers.DataStructures;
using SmsBuddy.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class SmsViewModel: ChildViewModelBase
    {
        SmsModel _sms;
        IEnumerable<Doublet<string, string>> _fields;
        IEnumerable<TemplateModel> _templates;
        ICommand _refresh;

        public SmsViewModel() : base("Messages", "sms-32.png")
        {
            PropertyChanged += SmsViewModel_PropertyChanged;
        }

        ~SmsViewModel()
        {
            PropertyChanged -= SmsViewModel_PropertyChanged;
        }

        #region properties

        public SmsModel Sms
        {
            get { return _sms; }
            set { Set(nameof(Sms), ref _sms, value); }
        }

        public IEnumerable<Doublet<string, string>> Fields
        {
            get { return _fields; }
            private set { Set(nameof(Fields), ref _fields, value); }
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

        #endregion

        void SmsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {

            }
        }

        void Refresh()
        {
            Templates = new TemplateModel().Get() as IEnumerable<TemplateModel>;
        }
    }
}
