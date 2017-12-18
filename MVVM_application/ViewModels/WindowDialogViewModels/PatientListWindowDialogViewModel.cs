using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.Views.WindowDialogViews;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class PatientListWindowDialogViewModel :ViewModelBase
    {
        private IManager _manager;
        private PatientListWindowDialogModel _patientListWindowDialogModel;

        private PatientManager _patient;
        public PatientManager Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                RaisePropertyChanged("Patient");
            }
        }

        private ObservableCollection<PatientManager> _patientList;
        public ObservableCollection<PatientManager> PatientList
        {
            get { return _patientList; }
            set
            {
                _patientList = value;
                RaisePropertyChanged("PatientList");
            }
        }

        public RelayCommand<PatientListWindowDialog> GetPatientFromListCommand { get; private set; }
        public RelayCommand<PatientListWindowDialog> CancelCommand { get; private set; }

        public PatientListWindowDialogViewModel(IManager manager, PatientListWindowDialogModel patientListWindowDialogModel)
        {
            _manager = manager;
            _patientListWindowDialogModel = patientListWindowDialogModel;
            FillData();
            InitialiseCommand();
        }

        public void FillData()
        {
            if (_manager.GetPatientList() != null)
            {
                this.PatientList = new ObservableCollection<PatientManager>(_patientListWindowDialogModel.GetAllPatient());
            }
        }

        public void InitialiseCommand()
        {
            GetPatientFromListCommand = new RelayCommand<PatientListWindowDialog>(ExecuteGetPatientFromListCommand);
            CancelCommand = new RelayCommand<PatientListWindowDialog>(ExecuteCancelCommand);
        }

        private void ExecuteCancelCommand(PatientListWindowDialog windowPatientList)
        {
            windowPatientList.Close();
            _manager.SetUnchangedView(true);
        }

        private void ExecuteGetPatientFromListCommand(PatientListWindowDialog windowPatientList)
        {
            if(_patient != null)
            {
                _patientListWindowDialogModel.SetPatientFromList(_patient);
                _manager.SetUnchangedView(false);
                windowPatientList.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Proszę zaznaczyć pacjenta klikając na liste");
            }
        }
    }
}
