using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using MVVM_application.Models.PatientCardModels;
using System.Collections.ObjectModel;
using System;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.Views.WindowDialogViews;
using System.Windows;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientVisitViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly PatientVisitModel _patientVisitModel;

        private Patient _patient;
        private ObservableCollection<VisitManager> _patientVisitsList;
        public ObservableCollection<VisitManager> PatientVisitsList
        {
            get { return _patientVisitsList; }
            set
            {
                _patientVisitsList = value;
                RaisePropertyChanged("PatientVisitsList");
            }
        }

        private string _patientInfo;
        public string PatientInfo
        {
            get { return _patientInfo; }
            set
            {
                _patientInfo = value;
                RaisePropertyChanged("PatientInfo");
            }
        }

        private VisitManager _visitManager;
        
        private VisitManager _visitManagerObject;
        public VisitManager VisitManagerObject
        {
            get { return _visitManagerObject; }
            set
            {
                _visitManagerObject = value;
                if (_visitManagerObject != null)
                {
                    _visitManager = _visitManagerObject;
                }
                RaisePropertyChanged("VisitManagerObject");
            }
        }

        public RelayCommand ShowVisitCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }
        public RelayCommand RefreshDateCommand { get; private set; }

        public ShowVisitWindowDialogViewModel ShowVisitWDViewModel { get; set; }
        private ShowVisitWindowDialogModel _showVisitWindowDialogModel;

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }

        private ObservableCollection<DateTime> _dateList;
        public ObservableCollection<DateTime> DateList
        {
            get { return _dateList; }
            set
            {
                _dateList = value;
                RaisePropertyChanged("DateList");
            }
        }

        public PatientVisitViewModel(IManager manager, PatientVisitModel patientVisitModel)
        {
            _manager = manager;
            _patientVisitModel = patientVisitModel;
            _patient = _manager.GetPatient();
            FillData();
            InitialiseCommand();
        }

        public void FillData()
        {
            var date = DateTime.Parse("0001-01-01 00:00:00");
            
            if (_patient != null)
            {
                if (_date != date)
                {
                    PatientVisitsList = new ObservableCollection<VisitManager>(_patientVisitModel.GetAllVisitsWithPatient(_patient, _date));
                }
                else
                {
                    PatientVisitsList = new ObservableCollection<VisitManager>(_patientVisitModel.GetAllVisitsWithPatient(_patient, DateTime.Today));
                }
                _patientInfo = _patientVisitModel.SetPatientInfo();
            }


            this.DateList = new ObservableCollection<DateTime>(_patientVisitModel.GetDate());
            _showVisitWindowDialogModel = new ShowVisitWindowDialogModel(_manager);
            ShowVisitWDViewModel = new ShowVisitWindowDialogViewModel(_manager, _showVisitWindowDialogModel);
        }

        private void InitialiseCommand()
        {
            ShowVisitCommand = new RelayCommand(ExecuteShowVisitCommand);
            GoBackCommand = new RelayCommand(ExecuteGoBackCommand);
            RefreshDateCommand = new RelayCommand(ExecuteRefreshDateCommand);
        }

        private void ExecuteRefreshDateCommand()
        {
            PatientVisitsList = new ObservableCollection<VisitManager>(_patientVisitModel.GetAllVisitsWithPatient(_patient, _date));
        }

        private void ExecuteGoBackCommand()
        {
            _manager.ChangeView(TypesOfViews.SearchPatientViewModel);
        }

        private void ExecuteShowVisitCommand()
        {
            if (_visitManager != null)
            {
                _manager.SetVisitManager(_visitManager);
                _manager.RefreshAll(TypesOfViews.PatientVisitViewModel);
                ShowVisitWindowDialog showVisitWindowDialog = new ShowVisitWindowDialog();
                showVisitWindowDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Proszę wybrać wizytę zaznaczając ją na liście");
            }

        }
    }
}
