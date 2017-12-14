using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.Views;
using MVVM_application.Models;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class RegisterUCModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly SearchVisitToEditWindowDialogModel _searchVisitToEditWindowDialogModel;
        public SearchVisitToEditWindowDialogViewModel SearchVisitToEditWDViewModel { get; private set; }

        #region ICommand
            
        public ICommand AddVisitCommand { get; private set; }
        public RelayCommand EditVisitCommand { get; private set; }
        public ICommand AddNewPatientCommand { get; private set; }

        #endregion ICommand
        
        public RegisterUCModel(IManager manager)
        {
            _manager = manager;
            _searchVisitToEditWindowDialogModel = new SearchVisitToEditWindowDialogModel(_manager);
            SearchVisitToEditWDViewModel = new SearchVisitToEditWindowDialogViewModel(_manager, _searchVisitToEditWindowDialogModel);

            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            AddVisitCommand = new RelayCommand(ExecuteAddVisitCommand);
            EditVisitCommand = new RelayCommand(ExecuteEditVisitCommand);
            AddNewPatientCommand = new RelayCommand(ExecuteAddNewPatientCommand);
        }
        
        public void ExecuteAddVisitCommand()
        {
            SearchPatientWindowDialog searchPatientWindowDialog = new SearchPatientWindowDialog();
            searchPatientWindowDialog.ShowDialog();
            if (_manager.GetPatient() != null && _manager.GetUnchangedView() == false)
            {
                _manager.RefreshAll(TypesOfViews.PatientNewVisitViewModel);
            }
            else if (_manager.GetPatientList() != null && _manager.GetUnchangedView() == false)
            {
                _manager.RefreshAll(TypesOfViews.PatientNewVisitViewModel);
            }
        }

        public void ExecuteEditVisitCommand()
        {
            SearchVisitToEditWindowDialog searchVisitToEditWindowDialog = new SearchVisitToEditWindowDialog();
            searchVisitToEditWindowDialog.ShowDialog();
            if (_manager.GetUnchangedView() == false)
            {
                _manager.RefreshAll(TypesOfViews.EditVisitViewModel);
            }

        }

        public void ExecuteAddNewPatientCommand()
        {
            _manager.ChangeView(TypesOfViews.AddNewPatientViewModel);
        }
    }
}
