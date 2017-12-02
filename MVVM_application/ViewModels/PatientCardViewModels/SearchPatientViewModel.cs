using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.PatientCardModels;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class SearchPatientViewModel : ViewModelBase
    {
        private readonly IManager _manager;
        private readonly SearchPatientModel _searchPatientModel;

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



        public SearchPatientViewModel(IManager manager, SearchPatientModel searchPatientModel)
        {
            _manager = manager;
            _searchPatientModel = searchPatientModel;
            _patient = _manager.GetPatient();
            if (_patient != null)
            {
                _name = _searchPatientModel.GetPatientName();
                _surname = _searchPatientModel.GetPatientSurame();
                _street = _searchPatientModel.GetPatientStreet();
                _homeNr = _searchPatientModel.GetPatientHomeNr();
                _city = _searchPatientModel.GetPatientCity();
                _phone = _searchPatientModel.GetPatientPhone();
                _dateOfBirth = _searchPatientModel.GetPatientDateOfBirth();
                _pesel = _searchPatientModel.GetPatientPesel();

            }
        }
    }
}
