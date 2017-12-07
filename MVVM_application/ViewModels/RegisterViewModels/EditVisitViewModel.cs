using System;
using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.RegisterModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class EditVisitViewModel : ViewModelBase
    {
        private readonly IManager _manager;
        private EditVisitModel _editVisitModel;
        private List<DateTime> _newVisitDateList;

        private string _patient;
        private string _doctor;
        private string _visitDate;
        private string _newVisitDate;

        public string Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                RaisePropertyChanged("Patient");
            }
        }
        public string Doctor
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                RaisePropertyChanged("Doctor");
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
        public string NewVisitDate
        {
            get { return _newVisitDate; }
            set
            {
                _newVisitDate = value;
                RaisePropertyChanged("NewVisitDate");
            }
        }

        public ObservableCollection<DateTime> VisitDateList { get; set; }
        public ObservableCollection<DateTime> NewVisitDateList { get; set; }
        
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public RelayCommand<DateTime> RefreshNewVisitCommand { get; private set; }
        public RelayCommand DeleteVisitCommand { get; private set; }

        public EditVisitViewModel(IManager manager, EditVisitModel editVisitModel)
        {
            _manager = manager;
            _editVisitModel = editVisitModel;
            if (_manager.GetDoctor() != null && _manager.GetPatient() != null)
            {
                FillData();
            }
            InitialiseCommand();
        }

        private void FillData()
        {
            _newVisitDateList = new List<DateTime>();
            _patient = _editVisitModel.SetPatient();
            _doctor = _editVisitModel.SetDoctor();
            this.VisitDateList = new ObservableCollection<DateTime>(_editVisitModel.FillVisitList());
            this.NewVisitDateList = new ObservableCollection<DateTime>();
        }

        private void InitialiseCommand()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
            RefreshNewVisitCommand = new RelayCommand<DateTime>(ExecuteRefreshNewVisitCommand);
            DeleteVisitCommand = new RelayCommand(ExecuteDeleteVisitCommand);
        }

        private void ExecuteDeleteVisitCommand()
        {
            if(_visitDate != null)
            {
                _editVisitModel.DeleteVisit(_visitDate);
                MessageBox.Show("Wizyta odwolana");
            }
            else
            {
                MessageBox.Show("Prosze wybrac wizyte do odwolania");
            }
        }

        private void ExecuteRefreshNewVisitCommand(DateTime visit)
        {
            if (visit != null)
            {
                _newVisitDateList.Clear();
                _newVisitDateList = _editVisitModel.FillNewVisitDateList(visit);
                this.NewVisitDateList.Clear();
                for (int i = 0; i < _newVisitDateList.Count; i++)
                {
                    this.NewVisitDateList.Add(_newVisitDateList[i]);
                }
            }
        }

        private void ExecuteCancelCommand()
        {
            FillData();
            MessageBox.Show("Anulowanie");
        }

        private void ExecuteSaveCommand()
        {
            if(_visitDate != null && _newVisitDate != null)
            {
                _editVisitModel.ChangeVisitDate(_visitDate, _newVisitDate);
                MessageBox.Show("Wizyta edytowana");
            }
            else
            {
                MessageBox.Show("Prosze uzupelnic dane");
            }
        }

    }
}
