using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel : DataViewModelBase
    {
        string _field, _selectedField;
        ICommand _add, _remove, _move;

        public TemplateViewModel() : base("Template")
        {
            Icon = "/Assets/Images/template-32.png";
        }

        #region properties

        public string Field
        {
            get { return _field; }
            set { Set(nameof(Field), ref _field, value); }
        }

        public string SelectedField
        {
            get { return _selectedField; }
            set { Set(nameof(SelectedField), ref _selectedField, value); }
        }

        #endregion

        #region commands

        public ICommand AddCommand
        {
            get
            {
                if (_add == null)
                    _add = new RelayCommand(Add);

                return _add;
            }
        }

        #endregion

        void Add()
        {
            var record = Record as TemplateModel;
            if (record == null)
                return;

            record.AddField(Field);
            Field = null;
        }

        protected override DataModelBase New(object argument)
        {
            return new TemplateModel();
        }
    }
}
