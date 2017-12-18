using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Views;
using MVVM_application.Views.WindowDialogViews;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class PatientCardUCModel :ViewModelBase
    {
        private readonly IManager _manager;
        private SearchPatientWindowDialogModel _searchPatientWindowDialogModel;
        private PatientListWindowDialogModel _patientListWindowDialogModel;
        public SearchPatientWindowDialogViewModel SearchPatientWDViewModel { get; private set; }
        public PatientListWindowDialogViewModel PatientListWDViewModel { get; private set; }

        #region ICommand

        public RelayCommand SearchPatientCommand { get; private set; }
        public RelayCommand PatientVisitCommand { get; private set; }

        #endregion ICommand


        public PatientCardUCModel(IManager manager)
        { 
            _manager = manager;
            FillData();
            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            SearchPatientCommand = new RelayCommand(ExecuteSearchPatientCommand);
            PatientVisitCommand = new RelayCommand(ExecutePatientVisitCommand);
        }

        public void FillData()
        {
            _searchPatientWindowDialogModel = new SearchPatientWindowDialogModel(_manager);
            SearchPatientWDViewModel = new SearchPatientWindowDialogViewModel(_manager, _searchPatientWindowDialogModel);

            _patientListWindowDialogModel = new PatientListWindowDialogModel(_manager);
            PatientListWDViewModel = new PatientListWindowDialogViewModel(_manager, _patientListWindowDialogModel);
        }

        public void ExecuteSearchPatientCommand()
        {
            SearchPatient(TypesOfViews.SearchPatientViewModel);
        }

        public void ExecutePatientVisitCommand()
        {
            if (_manager.GetPatient() != null)
            {
                _manager.ChangeView(TypesOfViews.PatientVisitViewModel);
            }
            else
            {
                MessageBox.Show("Nie wybrano pacjenta");
            }
        }
        
        
        public void SearchPatient(TypesOfViews typeOfView)
        {
            _manager.SetPatient(null);
            _manager.SetPatientList(null);

            SearchPatientWindowDialog searchPatientWindowDialog = new SearchPatientWindowDialog();
            searchPatientWindowDialog.ShowDialog();

            if (_manager.GetPatient() != null && _manager.GetUnchangedView() == false)
            {
                _manager.RefreshAll(typeOfView);
            }
            else if (_manager.GetPatientList() != null && _manager.GetUnchangedView() == false)
            {
                _manager.RefreshViewModel();

                PatientListWindowDialog patientListWindowDialog = new PatientListWindowDialog();
                patientListWindowDialog.ShowDialog();

                if (_manager.GetUnchangedView() == false)
                {
                    _manager.RefreshAll(typeOfView);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano pacjenta");
            }
        }
    }
}
