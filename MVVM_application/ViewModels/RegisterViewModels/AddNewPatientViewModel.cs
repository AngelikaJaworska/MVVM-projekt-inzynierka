using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.RegisterModels;
using System.Windows.Input;
using System;
using System.Windows;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class AddNewPatientViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly AddNewPatientModel _addNewPatientModel;

        //private Patient _patient;
        private string _name;
        private string _surname;
        private string _street;
        private string _homeNr;
        private string _city;
        private string _phone;
        private string _dateOfBirth;
        private string _pesel;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                RaisePropertyChanged("Surname");
            }
        }
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                RaisePropertyChanged("Street");
            }
        }
        public string HomeNr
        {
            get { return _homeNr; }
            set
            {
                _homeNr = value;
                RaisePropertyChanged("HomeNr");
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged("City");
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }
        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                RaisePropertyChanged("DateOfBirth");
            }
        }
        public string Pesel
        {
            get { return _pesel; }
            set
            {
                _pesel = value;
                RaisePropertyChanged("Pesel");
            }
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public AddNewPatientViewModel(IManager manager, AddNewPatientModel addNewPatientModel)
        {
            _manager = manager;
            _addNewPatientModel = addNewPatientModel;
            //_patient = _manager.GetPatient();

            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }
        private void ExecuteSaveCommand()
        {
           if(_addNewPatientModel.CreatePatientCard(_name, _surname, _dateOfBirth, _street, _homeNr, _city, _phone, _pesel))
            {
                MessageBox.Show("Pacjent zapisany");
                SetBlankSpace();
            }
            else
            {
                MessageBox.Show("Prosze uzupelnic dane");
            }
        }

        private void ExecuteCancelCommand()
        {
            MessageBox.Show("Anulowanie");
            SetBlankSpace();
        }

        private void SetBlankSpace()
        {
            _name = "";
            _surname = "";
            _dateOfBirth = "";
            _street = "";
            _homeNr = "";
            _city = "";
            _phone = "";
            _pesel = "";
        }
    }
}
