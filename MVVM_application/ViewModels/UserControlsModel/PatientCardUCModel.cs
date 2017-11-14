using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class PatientCardUCModel :ViewModelBase
    {
        private readonly IViewManager _viewManager;

        #region ICommand

        public ICommand PatientNewVisitCommand { get; private set; }
        public ICommand PatientVisitCommand { get; private set; }
        public ICommand PatientEditDataCommand { get; private set; }

        #endregion ICommand
        
        public PatientCardUCModel(IViewManager viewManager)
        { 
            _viewManager = viewManager;
            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            PatientNewVisitCommand = new RelayCommand(ExecutePatientNewVisitCommand);
            PatientVisitCommand = new RelayCommand(ExecutePatientVisitCommand);
            PatientEditDataCommand = new RelayCommand(ExecutePatientEditDataCommand);
        }

        public void ExecutePatientNewVisitCommand()
        {
            _viewManager.ChangeView(TypesOfViews.PatientNewVisitViewModel);
        }
        public void ExecutePatientVisitCommand()
        {
            _viewManager.ChangeView(TypesOfViews.PatientVisitViewModel);
        }
        public void ExecutePatientEditDataCommand()
        {
            _viewManager.ChangeView(TypesOfViews.PatientEditDataViewModel);
        }
    }
}
