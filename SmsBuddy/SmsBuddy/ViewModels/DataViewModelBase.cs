using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using SmsBuddy.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    abstract class DataViewModelBase: ChildViewModelBase
    {
        DataModelBase _record;
        FrameworkElement _recordEditor;
        ICommand _new, _save, _delete;

        protected DataViewModelBase(string title): base(title) { }

        #region properties

        public DataModelBase Record
        {
            get { return _record; }
            set { Set(nameof(Record), ref _record, value); }
        }

        public FrameworkElement RecordEditor
        {
            get { return _recordEditor; }
            private set { Set(nameof(RecordEditor), ref _recordEditor, value); }
        }

        #endregion

        #region commands

        public ICommand NewCommand
        {
            get
            {
                if (_new == null)
                    _new = new RelayCommand<object, DataModelBase>(New, (record) => Record = record);

                return _new;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_save == null)
                    _save = new RelayCommand(Save);

                return _new;
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

        #endregion

        protected abstract DataModelBase New(object argument);

        void Save()
        {
            if (Record == null)
                return;

            Record.Save();
        }

        void Delete()
        {
            if (Record == null)
                return;

            Record.Delete();
        }

        public new Control GetView()
        {
            var view = new DataView();
            RecordEditor = base.GetView();
            view.DataContext = this;
            return view;
        }
    }
}
