using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel: ViewModelBase
    {
        string _field, _selectedField;
        TemplateModel _template;
        ICommand _addField, _removeField;

        #region properties

        public TemplateModel Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
        }

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

        public ICommand AddFieldCommand
        {
            get
            {
                if (_addField == null)
                    _addField = new RelayCommand(AddField);

                return _addField;
            }
        }

        public ICommand RemoveFieldCommand
        {
            get
            {
                if (_removeField == null)
                    _removeField = new RelayCommand(RemoveField);

                return _removeField;
            }
        }

        #endregion

        void AddField()
        {
            if(Template.AddField(Field))
                Field = null;
        }

        void RemoveField()
        {
            if (string.IsNullOrEmpty(SelectedField))
                return;

            if (Template.RemoveField(SelectedField))
                SelectedField = null;
        }
    }
}
