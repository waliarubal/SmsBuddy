using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel: ChildViewModelBase
    {
        TemplateModel _template;
        IEnumerable<TemplateModel> _templates;
        string _newField, _selectedField;
        ICommand _addField, _removeField, _addFieldToMessage, _new, _save, _delete, _refresh;

        public TemplateViewModel(): base("Templates", "template-32.png") { }

        #region properties

        public TemplateModel Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
        }

        public IEnumerable<TemplateModel> Templates
        {
            get { return _templates; }
            private set { Set(nameof(Templates), ref _templates, value); }
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

        public ICommand NewCommand
        {
            get
            {
                if (_new == null)
                    _new = new RelayCommand(New);

                return _new;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_save == null)
                    _save = new RelayCommand(Save);

                return _save;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_delete == null)
                    _delete = new RelayCommand(Delete);

                return _delete;
            }
        }

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
            Templates = new TemplateModel().Get() as IEnumerable<TemplateModel>;
        }

        void New()
        {
            ErrorMessage = null;
            Template = new TemplateModel();
        }

        void Save()
        {
            ErrorMessage = null;

            if (Template == null)
                ErrorMessage = "Select or create a new template first.";
            else if (string.IsNullOrEmpty(Template.Name))
                ErrorMessage = "Name not specified.";
            else if (string.IsNullOrEmpty(Template.Message))
                ErrorMessage = "Message not specified.";
            else
            {
                Template.Save();
                New();
                Refresh();
            }
        }

        void Delete()
        {
            ErrorMessage = null;

            if (Template == null)
                ErrorMessage = "Select or create a new template first.";
            else
            {
                Template.Delete();
                New();
                Refresh();
            }
        }

        void AddField()
        {
            ErrorMessage = null;

            if (Template == null)
                ErrorMessage = "Select or create a new template first.";
            else if (string.IsNullOrEmpty(NewField))
                ErrorMessage = "New field can't be empty.";
            else if (Template.Fields.Contains(NewField))
                ErrorMessage = "Field with given name already exists.";
            else
            {
                Template.Fields.Add(NewField);
                NewField = null;
            }
        }

        void RemoveField()
        {
            ErrorMessage = null;

            if (Template == null)
                ErrorMessage = "Select or create a new template first.";
            else if (!Template.Fields.Contains(SelectedField))
                ErrorMessage = "Selected fiedl does not exist.";
            else
            {
                Template.Fields.Remove(SelectedField);
                SelectedField = null;
            }
        }

        void AddFieldToMessage()
        {
            ErrorMessage = null;

            if (Template == null)
                ErrorMessage = "Select or create a new template first.";
            else
                Template.Message = string.Format("{0}<<{1}>>", Template.Message, SelectedField);
        }
    }
}
