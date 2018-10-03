using NullVoidCreations.WpfHelpers.Commands;
using SmsBuddy.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace SmsBuddy.ViewModels
{
    class ContactViewModel: ChildViewModelBase
    {
        ContactModel _contact;
        IEnumerable<ContactModel> _contacts;
        string _newMobile, _selectedMobile;
        ICommand _refresh, _new, _save, _delete, _addMobile, _removeMobile;

        public ContactViewModel() : base("Contacts", "contacts-32.png") { }

        #region properties

        public string NewMobileNumber
        {
            get { return _newMobile; }
            set { Set(nameof(NewMobileNumber), ref _newMobile, value); }
        }

        public string SelectedMobileNumber
        {
            get { return _selectedMobile; }
            set { Set(nameof(SelectedMobileNumber), ref _selectedMobile, value); }
        }

        public ContactModel Contact
        {
            get { return _contact; }
            set { Set(nameof(Contact), ref _contact, value); }
        }

        public IEnumerable<ContactModel> Contacts
        {
            get { return _contacts; }
            private set { Set(nameof(Contacts), ref _contacts, value); }
        }

        #endregion

        #region commands

        public ICommand RefreshCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand(Refresh);

                return _refresh;
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

        public ICommand AddMobileCommand
        {
            get
            {
                if (_addMobile == null)
                    _addMobile = new RelayCommand(AddMobile) { IsSynchronous = true };

                return _addMobile;
            }
        }

        public ICommand RemoveMobileCommand
        {
            get
            {
                if (_removeMobile == null)
                    _removeMobile = new RelayCommand(RemoveMobile) { IsSynchronous = true };

                return _removeMobile;
            }
        }

        #endregion

        void AddMobile()
        {
            ErrorMessage = null;

            if (Contact == null)
                ErrorMessage = "Select or create a new message first.";
            else if (string.IsNullOrEmpty(NewMobileNumber))
                ErrorMessage = "Enter mobile number.";
            else
            {
                Contact.MobileNumbers.Add(NewMobileNumber);
                NewMobileNumber = null;
            }
        }

        void RemoveMobile()
        {
            ErrorMessage = null;

            if (Contact == null)
                ErrorMessage = "Select or create a new message first.";
            else if (SelectedMobileNumber == null)
                ErrorMessage = "Select a mobile number to remove.";
            else
            {
                if (Contact.MobileNumbers.Remove(SelectedMobileNumber))
                    SelectedMobileNumber = null;
            }
        }

        void Delete()
        {
            ErrorMessage = null;

            if (Contact == null)
                ErrorMessage = "Select or create a new message first.";
            else
            {
                Contact.Delete();
                Refresh();
                New();
            }
        }

        void New()
        {
            ErrorMessage = null;
            Contact = new ContactModel();
        }

        void Save()
        {
            ErrorMessage = null;

            if (Contact == null)
                ErrorMessage = "Select or create a new contact.";
            else if (string.IsNullOrEmpty(Contact.Name))
                ErrorMessage = "Name not specified.";
            else if (Contact.MobileNumbers.Count == 0)
                ErrorMessage = "Mobile number not specified.";
            else
            {
                Contact.Save();
                Refresh();
                New();
            }
        }

        void Refresh()
        {
            ErrorMessage = null;
            Contacts = new ContactModel().Get() as IEnumerable<ContactModel>;
        }
    }
}
