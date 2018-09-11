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
        ICommand _addField, _removeField, _moveField;

        public TemplateViewModel()
        {
            Template = new TemplateModel();
        }

        public TemplateViewModel(TemplateModel template): this()
        {
            Template = template;
        }

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
        public ICommand MoveFieldCommand
        {
            get
            {
                if (_moveField == null)
                    _moveField = new RelayCommand(MoveField);

                return _moveField;
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

        void MoveField()
        {
            if (string.IsNullOrEmpty(SelectedField))
                return;

            Template.Template = string.Format("{0}<<{1}>>", Template.Template, SelectedField);
        }
    }
}
