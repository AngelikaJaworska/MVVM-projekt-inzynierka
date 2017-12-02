using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.PatientCardModels;
using System.Windows.Input;
using System;
using System.Windows;
using System.Collections.ObjectModel;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientNewVisitViewModel:ViewModelBase
    {
        private readonly IManager _manager;
        private readonly PatientNewVisitModel _patientNewVisitModel;

        private Patient _patient;

        private string _nameAndSurname;
        public string NameAndSurname
        {
            get { return _nameAndSurname; }
            set
            {
                _nameAndSurname = value;
                RaisePropertyChanged("NameAndSurname");
            }
        }

        public ObservableCollection<string> SpecialisationtList { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public PatientNewVisitViewModel(IManager manager, PatientNewVisitModel patientNewVisitModel)
        {
            _manager = manager;
            _patientNewVisitModel = patientNewVisitModel;
            _patient = _manager.GetPatient();
            if(_patient != null)
            {
                FillData();
            }
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }

        private void ExecuteCancelCommand()
        {
            MessageBox.Show("Anulowanie");
        }

        private void ExecuteSaveCommand()
        {
            SetData();
            MessageBox.Show("Dane prawidlowo zmienione");
        }

        private void SetData()
        {
            _patientNewVisitModel.SetPatientNameAndSurname(_nameAndSurname);
            
        }

        private void FillData()
        {
           _nameAndSurname =  _patientNewVisitModel.GetPatientNameAndSurname();

            SpecialisationtList = new ObservableCollection<string>(_patientNewVisitModel.FillSpecialisationList());
        }
    }
}
