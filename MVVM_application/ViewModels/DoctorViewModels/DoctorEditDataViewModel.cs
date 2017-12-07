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
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Views;

namespace MVVM_application.ViewModels.DoctorViewModels
{
    public class DoctorEditDataViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly DoctorEditDataModel _doctorEditDataModel;
        
        private EditDoctorVisitHoursWindowDialogModel _editDoctorVisitHoursWindowDialogModel;
        public EditDoctorVisitHoursWindowDialogViewModel EditDoctorVisitHoursWDViewModel { get; set; }

        private Doctor _doctor;

        private string _name;
        private string _specialisation;
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
        public string Specialisation
        {
            get { return _specialisation; }
            set
            {
                _specialisation = value;
                RaisePropertyChanged("Specialisation");
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
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public DoctorEditDataViewModel(IManager manager, DoctorEditDataModel doctorEditDataModel)
        {
            _manager = manager;
            _doctorEditDataModel = doctorEditDataModel;
            _doctor = _manager.GetDoctor();
            if (_doctor != null)
            {
                FillData();
            }
            EditVisitHoursCommand = new RelayCommand(ExecuteEditVisitHoursCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }

        private void FillData()
        {
            _name = _doctorEditDataModel.GetDoctorName();
            _surname = _doctorEditDataModel.GetDoctorSurame();
            _street = _doctorEditDataModel.GetDoctorStreet();
            _homeNr = _doctorEditDataModel.GetDoctorHomeNr();
            _city = _doctorEditDataModel.GetDoctorCity();
            _phone = _doctorEditDataModel.GetDoctorPhone();

            SpecialisationtList = new ObservableCollection<string>(_doctorEditDataModel.FillSpecialisationList());

            _editDoctorVisitHoursWindowDialogModel = new EditDoctorVisitHoursWindowDialogModel(_manager);
            EditDoctorVisitHoursWDViewModel = new EditDoctorVisitHoursWindowDialogViewModel(_manager, _editDoctorVisitHoursWindowDialogModel);
        }

        private void SetData()
        {
            _doctorEditDataModel.SetDoctorName(_name);
            _doctorEditDataModel.SetSpecialisation(_specialisation);
            _doctorEditDataModel.SetDoctorSurame(_surname);
            _doctorEditDataModel.SetDoctorStreet(_street);
            _doctorEditDataModel.SetDoctorHomeNr(_homeNr);
            _doctorEditDataModel.SetDoctorCity(_city);
            _doctorEditDataModel.SetDoctorPhone(_phone);
        }

        private void ExecuteCancelCommand()
        {
            FillData();
            MessageBox.Show("Anulowanie");
        }

        private void ExecuteSaveCommand()
        {
            SetData();
            MessageBox.Show("Dane prawidlowo zmienione");
        }

        private void ExecuteEditVisitHoursCommand()
        {
            EditDoctorVisitHourWindowDialog editDoctorVisitHoursWindowDialog = new EditDoctorVisitHourWindowDialog();
            editDoctorVisitHoursWindowDialog.ShowDialog();
        }
    }
}
