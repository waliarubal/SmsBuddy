using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel: ViewModelBase
    {
        TemplateModel _template;
        ObservableCollection<TemplateModel> _templates;
        string _newField, _selectedField;
        ICommand _addField, _removeField, _addFieldToMessage;

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

        public string NewField
        {
            get { return _newField; }
            set { Set(nameof(NewField), ref _newField, value); }
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
                    _addField = new RelayCommand(AddField) { IsSynchronous = true };

                return _addField;
            }
        }

        public ICommand RemoveFieldCommand
        {
            get
            {
                if (_removeField == null)
                    _removeField = new RelayCommand(RemoveField) { IsSynchronous = true };

                return _removeField;
            }
        }

        public ICommand AddFieldToMessageCommand
        {
            get
            {
                if (_addFieldToMessage == null)
                    _addFieldToMessage = new RelayCommand(AddFieldToMessage);

                return _addFieldToMessage;
            }
        }

        #endregion

        void AddField()
        {
            if (Template == null || Template.Fields.Contains(NewField))
                return;

            Template.Fields.Contains(NewField);
            NewField = null;
        }

        void RemoveField()
        {
            if (Template == null || !Template.Fields.Contains(SelectedField))
                return;

            Template.Fields.Remove(SelectedField);
            SelectedField = null;
        }

        void AddFieldToMessage()
        {
            if (Template == null || string.IsNullOrWhiteSpace(SelectedField))
                return;

            Template.Message = string.Format("{0}<<{1}>>", Template.Message, SelectedField);
        }
    }
}
