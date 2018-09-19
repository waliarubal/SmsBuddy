using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class RecordsViewModel: ViewModelBase
    {
        ModelBase _record;
        IEnumerable<ModelBase> _records;
        string _searchKeywoard;
        ICommand _search, _new, _save, _delete;

        #region properties

        public ModelBase Record
        {
            get { return _record; }
            set { Set(nameof(Record), ref _record, value); }
        }

        public IEnumerable<ModelBase> Records
        {
            get { return _records; }
            private set { Set(nameof(Records), ref _records, value); }
        }

        public string SearchKeywoard
        {
            get { return _searchKeywoard; }
            set { Set(nameof(SearchKeywoard), ref _searchKeywoard, value); }
        }

        #endregion

        #region commands

        public ICommand SearchCommand
        {
            get
            {
                if (_search == null)
                    _search = new RelayCommand<string>(Search);

                return _search;
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

        #endregion

        void New()
        {
            var type = Record.GetType();
            Record = Activator.CreateInstance(type) as ModelBase;
        }

        void Search(string keywoard)
        {
            Records = Record.Get(keywoard);
        }

        void Delete()
        {
            Record.Delete();
        }

        void Save()
        {
            Record.Save();
        }
    }
}
