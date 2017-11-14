using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class RegisterUCModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;

        #region ICommand

        public ICommand AddVisitCommand { get; private set; }
        public ICommand EditVisitCommand { get; private set; }
        public ICommand AddNewPatientCommand { get; private set; }

        #endregion ICommand
        
        public RegisterUCModel(IViewManager viewManager)
        {
            _viewManager = viewManager;

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
            _viewManager.ChangeView(TypesOfViews.AddVisitViewModel);
        }

        public void ExecuteEditVisitCommand()
        {
            _viewManager.ChangeView(TypesOfViews.EditVisitViewModel);
        }

        public void ExecuteAddNewPatientCommand()
        {
            _viewManager.ChangeView(TypesOfViews.AddNewPatientViewModel);
        }
    }
}
