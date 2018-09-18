using SmsBuddy.Models;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel : DataViewModelBase
    {
        public TemplateViewModel() : base("Template")
        {
            Icon = "/Assets/Images/template-32.png";
        }

        protected override DataModelBase New(object argument)
        {
            return new TemplateModel();
        }
    }
}
