using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class TemplateViewModel: ViewModelBase
    {
        TemplateModel _template;
        string _newField, _selectedField;
        ICommand _new, _add, _remove, _move;

        #region properties

        public TemplateModel Template
        {
            get { return _template; }
            set { Set(nameof(Template), ref _template, value); }
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

        public ICommand NewCommand
        {
            get
            {
                if (_new == null)
                    _new = new RelayCommand(New);

                return _new;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_add == null)
                    _add = new RelayCommand(Add) { IsSynchronous = true };

                return _add;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (_remove == null)
                    _remove = new RelayCommand(Remove) { IsSynchronous = true };

                return _remove;
            }
        }

        public ICommand MoveCommand
        {
            get
            {
                if (_move == null)
                    _move = new RelayCommand(Move);

                return _move;
            }
        }

        #endregion

        void Add()
        {
            if (string.IsNullOrEmpty(NewField) || Template.Fields.Contains(NewField))
                return;

            Template.Fields.Add(NewField);
            NewField = null;
        }

        void Remove()
        {
            if (string.IsNullOrEmpty(SelectedField))
                return;

            Template.Fields.Remove(SelectedField);
            Template.Template = Template.Template.Replace(string.Format("<<{0}>>", SelectedField), string.Empty);
            SelectedField = null;
        }

        void Move()
        {
            if (string.IsNullOrEmpty(SelectedField))
                return;

            Template.Template = string.Format("{0}<<{1}>>", Template.Template, SelectedField);
        }

        void New()
        {
            Template = new TemplateModel();
        }
    }
}
