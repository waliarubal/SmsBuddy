using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel: ChildViewModelBase
    {
        TemplateModel _template;
        ObservableCollection<TemplateModel> _templates;
        string _newField, _selectedField;
        ICommand _addField, _removeField, _addFieldToMessage, _new, _save, _delete, _refresh;

        public TemplateViewModel(): base("Templates", "template-32.png") { }

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
                    _refresh = new RelayCommand<object, ObservableCollection<TemplateModel>>(Refresh, (templates) => Templates = templates) { IsCallbackSynchronous = true };

                return _refresh;
            }
        }

        #endregion

        ObservableCollection<TemplateModel> Refresh(object argument)
        {
            var templates = new ObservableCollection<TemplateModel>();

            foreach (var template in new TemplateModel().Get())
                templates.Add(template as TemplateModel);

            return templates;
        }

        void New()
        {
            Template = new TemplateModel();
        }

        void Save()
        {
            if (Template == null)
                return;

            try
            {
                Template.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void Delete()
        {
            if (Template == null)
                return;

            Template.Delete();
        }

        void AddField()
        {
            if (Template == null || Template.Fields.Contains(NewField))
                return;

            Template.Fields.Add(NewField);
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
