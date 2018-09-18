using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using SmsBuddy.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    abstract class DataViewModelBase: ChildViewModelBase
    {
        string _searchKeywoard;
        DataModelBase _record;
        IEnumerable<DataModelBase> _records;
        FrameworkElement _recordEditor;
        ICommand _new, _save, _delete, _get;

        protected DataViewModelBase(string title): base(title) { }

        #region properties

        public string SearchKeywoard
        {
            get { return _searchKeywoard; }
            set { Set(nameof(SearchKeywoard), ref _searchKeywoard, value); }
        }

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

        public IEnumerable<DataModelBase> Records
        {
            get { return _records; }
            private set { Set(nameof(Records), ref _records, value); }
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

        public ICommand GetCommand
        {
            get
            {
                if (_get == null)
                    _get = new RelayCommand<string>(Get);

                return _get;
            }
        }

        #endregion

        protected abstract DataModelBase New(object argument);

        void Get(string searchKeywoards)
        {
            if (Record == null)
                return;

            try
            {
                Records = Record.Get(searchKeywoards);
            }
            catch(Exception ex)
            {
                Records = null;
            }
        }

        void Save()
        {
            if (Record == null)
                return;

            try
            {
                Record.Save();
            }
            catch(Exception ex)
            {

            }
        }

        void Delete()
        {
            if (Record == null)
                return;

            try
            {
                Record.Delete();
            }
            catch(Exception ex)
            {

            }
            
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
