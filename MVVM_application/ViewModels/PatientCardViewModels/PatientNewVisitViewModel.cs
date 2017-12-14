using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.PatientCardModels;
using System.Windows.Input;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientNewVisitViewModel:ViewModelBase
    {
        private readonly IManager _manager;
        private readonly PatientNewVisitModel _patientNewVisitModel;
        
        private List<string> _doctorNameList;
        private List<DateTime> _visitDateList;

        private string _patientName;
        private string _specialisationName;
        private string _doctorName;
        private string _visitDate;
        private string _comments;

        public string PatientName
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
                RaisePropertyChanged("PatientName");
            }
        }
        public string SpecialisationName
        {
            get { return _specialisationName; }
            set
            {
                _specialisationName = value;
                RaisePropertyChanged("SpecialisationName");
            }
        }
        public string DoctorName
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
                RaisePropertyChanged("DoctorName");
            }
        }
        public string VisitDate
        {
            get { return _visitDate; }
            set
            {
                _visitDate = value;
                RaisePropertyChanged("VisitDate");
            }
        }
        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged("Comments");
            }
        }

        public ObservableCollection<string> PatientNameList { get; set; }
        public ObservableCollection<string> SpecialisationtNameList { get; set; }
        public ObservableCollection<string> DoctorNameList { get; set; }
        public ObservableCollection<DateTime> VisitDateList { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public RelayCommand RefreshPatientCommand { get; private set; }
        public RelayCommand RefreshDoctorCommand { get; private set; }
        public RelayCommand RefreshVisitDateCommand { get; private set; }

        public PatientNewVisitViewModel(IManager manager, PatientNewVisitModel patientNewVisitModel)
        {
            _manager = manager;
            _patientNewVisitModel = patientNewVisitModel;

            if (_manager.GetPatientList() != null)
            {
                FillData();
            }
            else if (_manager.GetPatient() != null)
            {
                FillData();
            }

            InitialiseCommand();
        }

        private void FillData()
        {
            PatientNameList = new ObservableCollection<string>(_patientNewVisitModel.FillPatientNameList());
            SpecialisationtNameList = new ObservableCollection<string>(_patientNewVisitModel.FillSpecialisationList());
            DoctorNameList = new ObservableCollection<string>();
            VisitDateList = new ObservableCollection<DateTime>();
            _doctorNameList = new List<string>();
            _visitDateList = new List<DateTime>();
        }

        private void InitialiseCommand()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
            RefreshPatientCommand = new RelayCommand(ExecuteRefreshPatientCommand);
            RefreshDoctorCommand = new RelayCommand(ExecuteRefreshDoctorCommand);
            RefreshVisitDateCommand = new RelayCommand(ExecuteRefreshVisitDateCommand);
        }

        private void ExecuteRefreshPatientCommand()
        {
            if(_patientName != null)
            {
                _patientNewVisitModel.SetPatient(_patientName);
            }
        }

        private void ExecuteSaveCommand()
        {
            if (_patientNewVisitModel.CreateVisitWithData(_patientName, _specialisationName, _doctorName, _visitDate, _comments))
            {
                MessageBox.Show("Wizyta zapisana");
                ClearLists();
            }
                else
            {
                MessageBox.Show("Prosze uzupelnic dane");
            }
        }
        
        private void ExecuteCancelCommand()
        {
            MessageBox.Show("Anulowanie");
            ClearLists();
        }

        private void ExecuteRefreshDoctorCommand()
        {
            if (_specialisationName != null)
            {
                _doctorNameList.Clear();
                _doctorNameList = _patientNewVisitModel.FillDoctorNameList(_specialisationName);
                this.DoctorNameList.Clear();
                for (int i = 0; i < _doctorNameList.Count; i++)
                {
                    this.DoctorNameList.Add(_doctorNameList[i]);
                }
            }
        }

        private void ExecuteRefreshVisitDateCommand()
        {
            if (_doctorName != null && _specialisationName != null)
            {
                _visitDateList.Clear();
                _visitDateList = _patientNewVisitModel.FillVisitDateList(_doctorName, _specialisationName);
                this.VisitDateList.Clear();
                for (int i = 0; i < _visitDateList.Count; i++)
                {
                    this.VisitDateList.Add(_visitDateList[i]);
                }
            }
        }

        private void ClearLists()
        {
            this.PatientNameList.Clear();
            this.SpecialisationtNameList.Clear();
            this.DoctorNameList.Clear();
            this.VisitDateList.Clear();
        }
    }
}
