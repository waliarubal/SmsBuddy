using NullVoidCreations.WpfHelpers.Base;
using SmsBuddy.Models;
using System.Collections.Generic;

namespace SmsBuddy.ViewModels
{
    class MessageViewModel: ViewModelBase
    {
        IEnumerable<TemplateModel> _templates;
        MessageModel _message;

        #region properties

        public IEnumerable<TemplateModel> Templates
        {
            get { return _templates; }
            private set { Set(nameof(Templates), ref _templates, value); }
        }

        public MessageModel Message
        {
            get { return _message; }
            private set { Set(nameof(Message), ref _message, value); }
        }

        #endregion
    }
}
