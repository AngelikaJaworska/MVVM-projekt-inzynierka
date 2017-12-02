using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.DoctorModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;

namespace MVVM_application.ViewModels.DoctorViewModels
{
    public class DoctorEditDataViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly DoctorEditDataModel _doctorEditDataModel;

        private Doctor _doctor;

        private string _name;
        private string _surname;
        private string _street;
        private string _homeNr;
        private string _city;
        private string _phone;

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
        public ObservableCollection<string> SpecialisationtList { get; set; }

        public ICommand EditVisitHoursCommand { get; private set; }

        public DoctorEditDataViewModel(IManager manager, DoctorEditDataModel doctorEditDataModel)
        {
            _manager = manager;
            _doctorEditDataModel = doctorEditDataModel;
            _doctor = _manager.GetDoctor();
            if (_doctor != null)
            {
                _name = _doctorEditDataModel.GetDoctorName();
                _surname = _doctorEditDataModel.GetDoctorSurame();
                _street = _doctorEditDataModel.GetDoctorStreet();
                _homeNr = _doctorEditDataModel.GetDoctorHomeNr();
                _city = _doctorEditDataModel.GetDoctorCity();
                _phone = _doctorEditDataModel.GetDoctorPhone();
            }
            this.SpecialisationtList = new ObservableCollection<string>(_doctorEditDataModel.FillSpecialisationList());
            EditVisitHoursCommand = new RelayCommand(ExecuteEditVisitHoursCommand);

        }

        private void ExecuteEditVisitHoursCommand()
        {
            MessageBox.Show("dziala! //todo");
        }
    }
}
