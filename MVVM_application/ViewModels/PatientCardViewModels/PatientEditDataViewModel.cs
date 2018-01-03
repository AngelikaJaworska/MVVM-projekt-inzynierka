using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.PatientCardModels;
using System.Windows.Input;
using System;
using System.Windows;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientEditDataViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly PatientEditDataModel _patientEditDataModel;

        private Patient _patient;
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


        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }

        public PatientEditDataViewModel(IManager manager, PatientEditDataModel patientEditDataModel)
        {
            _manager = manager;
            _patientEditDataModel = patientEditDataModel;
            _patient = _manager.GetPatient();
            if (_patient != null)
            {
                FillData();
            }
            InitialiseCommand();
        }

        private void InitialiseCommand()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            GoBackCommand = new RelayCommand(ExecuteGoBackCommand);
        }

        private void ExecuteGoBackCommand()
        {
            _manager.ChangeView(TypesOfViews.SearchPatientViewModel);
        }

        private async void ExecuteDeleteCommand()
        {
            if(_patient != null)
            {
                _patientEditDataModel.DeletePatient();
                //MessageBox.Show("Pacjent został wyrejestrowany");
                var message = await MetroMessageBoxManager.ShowMessageAsync("", "Pacjent został wyrejestrowany");
                _manager.RefreshAll(TypesOfViews.PatientCardViewModel);
            }
        }

        private async void ExecuteSaveCommand()
        {
            if(SetData())
            {
                //MessageBox.Show("Dane prawidłowo zmienione");
                var message = await MetroMessageBoxManager.ShowMessageAsync("Prawidłowe dane", "Pacjet został edytowany");
                _manager.RefreshAll(TypesOfViews.SearchPatientViewModel);
            }
            else
            {
                //MessageBox.Show("Niepoprawne dane");
                var message = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Proszę uzupełnić prawidłowo wszystkie dane");
            }
        }

        private bool SetData()
        {
            if (_patientEditDataModel.SetPatientName(_name)
                &&   _patientEditDataModel.SetPatientSurame(_surname)
                && _patientEditDataModel.SetPatientStreet(_street)
                && _patientEditDataModel.SetPatientHomeNr(_homeNr)
                &&  _patientEditDataModel.SetPatientCity(_city)
                && _patientEditDataModel.SetPatientPhone(_phone)
                && _patientEditDataModel.SetPatientDateOfBirth(_dateOfBirth)
                && _patientEditDataModel.SetPatientPesel(_pesel))
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        private void FillData()
        {
            _name = _patientEditDataModel.GetPatientName();
            _surname = _patientEditDataModel.GetPatientSurame();
            _street = _patientEditDataModel.GetPatientStreet();
            _homeNr = _patientEditDataModel.GetPatientHomeNr();
            _city = _patientEditDataModel.GetPatientCity();
            _phone = _patientEditDataModel.GetPatientPhone();
            _dateOfBirth = _patientEditDataModel.GetPatientDateOfBirth();
            _pesel = _patientEditDataModel.GetPatientPesel();
        }
    }
}
