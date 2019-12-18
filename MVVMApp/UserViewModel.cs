using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMApp
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _UsersList { get; set; }
        private User selecteduser;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)

            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }
        public UserViewModel()
        {
            _UsersList = new ObservableCollection<User>
            {
                new User{UserId = 1,FirstName="Raj",LastName="Beniwal",City="Delhi",State="DEL",Country="INDIA"},
                new User{UserId=2,FirstName="Mark",LastName="henry",City="New York", State="NY", Country="USA"},
                new User{UserId=3,FirstName="Mahesh",LastName="Chand",City="Philadelphia", State="PHL", Country="USA"},
                new User{UserId=4,FirstName="Vikash",LastName="Nanda",City="Noida", State="UP", Country="CANADA"},
                new User{UserId=5,FirstName="Harsh",LastName="Kumar",City="Ghaziabad", State="UP", Country="INDIA"},
                new User{UserId=6,FirstName="Reetesh",LastName="Tomar",City="Mumbai", State="MP", Country="INDIA"},
                new User{UserId=7,FirstName="Deven",LastName="Verma",City="Palwal", State="HP", Country="ENGLAND"},
                new User{UserId=8,FirstName="Ravi",LastName="Taneja",City="Delhi", State="DEL", Country="INDIA"},

            };

            UpdateCommand = new RelayCommand(UpdateCustomer);
        }
        public User SelectedUser
        {
            get
            {
                return selecteduser;
            }
            set
            {
                selecteduser = value;
                NotifyPropertyChanged("SelectedUser");
            }
        }

        public ObservableCollection<User> Users
        {
            get
            { return _UsersList; }
            set
            {
                _UsersList = value;
                NotifyPropertyChanged("Users");
            }
        }
        public RelayCommand UpdateCommand { get; set; }

        public void UpdateCustomer(object parameter)
        {
            Users.Add(
                new User
                {
                    FirstName = parameter as string,
                    LastName = "XYZ",
                    City = "TEST",
                    Country = "TESTREST",
                    State = "GUJRAT",
                    UserId = 9

                });

        }

    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;


        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> updateCustomer)
        {
            _execute = updateCustomer;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
