using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Views;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class PatientCardUCModel :ViewModelBase
    {
        private readonly IManager _manager;
        private readonly SearchPatientWindowDialogModel _searchPatientWindowDialogModel;

        #region ICommand

        public ICommand SearchPatientCommand { get; private set; }
        public ICommand PatientNewVisitCommand { get; private set; }
        public ICommand PatientVisitCommand { get; private set; }
        public ICommand PatientEditDataCommand { get; private set; }

        #endregion ICommand

        public SearchPatientWindowDialogViewModel SearchPatientWDViewModel { get; private set; }
        
        public PatientCardUCModel(IManager manager)
        { 
            _manager = manager;
            _searchPatientWindowDialogModel = new SearchPatientWindowDialogModel(_manager);
            SearchPatientWDViewModel = new SearchPatientWindowDialogViewModel(_manager, _searchPatientWindowDialogModel);
            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            SearchPatientCommand = new RelayCommand(ExecuteSearchPatientCommand);
            PatientNewVisitCommand = new RelayCommand(ExecutePatientNewVisitCommand);
            PatientVisitCommand = new RelayCommand(ExecutePatientVisitCommand);
            PatientEditDataCommand = new RelayCommand(ExecutePatientEditDataCommand);
        }

        public void ExecuteSearchPatientCommand()
        {
            SearchPatientWindowDialog searchPatientWindowDialog = new SearchPatientWindowDialog();
            searchPatientWindowDialog.ShowDialog();
            if(_manager.GetUnchangedView() == false)
            {//ChangeView
                _manager.RefreshAll(TypesOfViews.SearchPatientViewModel);
            }
        }

        public void ExecutePatientNewVisitCommand()
        {
            if(_manager.GetPatient() != null)
            {
                _manager.ChangeView(TypesOfViews.PatientNewVisitViewModel);
            }
            else
            {
                MessageBox.Show("Nie wybrano pacjenta");
            }

        }
        public void ExecutePatientVisitCommand()
        {
            _manager.ChangeView(TypesOfViews.PatientVisitViewModel);
        }
        public void ExecutePatientEditDataCommand()
        {
            if (_manager.GetPatient() != null)
            {
                _manager.ChangeView(TypesOfViews.PatientEditDataViewModel);
            }
            else
            {
                MessageBox.Show("Nie wybrano pacjenta do edycji");
            }

        }
    }
}
