using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class RegisterUCModel: ViewModelBase
    {
        private readonly IManager _manager;

        #region ICommand

        public ICommand SearchPatientRegisterCommand { get; private set; }
        public ICommand AddVisitCommand { get; private set; }
        public ICommand EditVisitCommand { get; private set; }
        public ICommand AddNewPatientCommand { get; private set; }

        #endregion ICommand
        
        public RegisterUCModel(IManager manager)
        {
            _manager = manager;

            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            SearchPatientRegisterCommand = new RelayCommand(ExecuteSearchPatientRegisterCommand);
            AddVisitCommand = new RelayCommand(ExecuteAddVisitCommand);
            EditVisitCommand = new RelayCommand(ExecuteEditVisitCommand);
            AddNewPatientCommand = new RelayCommand(ExecuteAddNewPatientCommand);
        }

        public void ExecuteSearchPatientRegisterCommand()
        {
            _manager.ChangeView(TypesOfViews.SearchPatientViewModel);
        }

        public void ExecuteAddVisitCommand()
        {
            _manager.ChangeView(TypesOfViews.AddVisitViewModel);
        }

        public void ExecuteEditVisitCommand()
        {
            _manager.ChangeView(TypesOfViews.EditVisitViewModel);
        }

        public void ExecuteAddNewPatientCommand()
        {
            _manager.ChangeView(TypesOfViews.AddNewPatientViewModel);
        }
    }
}
