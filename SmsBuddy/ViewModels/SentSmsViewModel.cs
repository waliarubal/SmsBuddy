using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class SentSmsViewModel: ChildViewModelBase
    {
        IEnumerable<SentSmsModel> _messages;
        ICommand _refresh;

        public SentSmsViewModel(): base("Outbox", "outbox-32.png") { }

        #region properties

        public IEnumerable<SentSmsModel> Messages
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

        #endregion

        void Refresh()
        {
            ErrorMessage = null;
            Messages = new SentSmsModel().Get() as IEnumerable<SentSmsModel>;
        }
    }
}
