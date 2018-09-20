using NullVoidCreations.WpfHelpers.Base;
using SmsBuddy.Models;
using System.Collections.ObjectModel;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel: ViewModelBase
    {
        TemplateModel _template;
        ObservableCollection<TemplateModel> _templates;

        #region properties

        public TemplateModel Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
        }

        public ObservableCollection<TemplateModel> Templates
        {
            get { return _templates; }
            set { Set(nameof(Templates), ref _templates, value); }
        }

        #endregion
    }
}
