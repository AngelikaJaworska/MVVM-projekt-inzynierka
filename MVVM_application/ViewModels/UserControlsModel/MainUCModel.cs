using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class MainUCModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;

        #region Command

        public ICommand DailyCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public ICommand PatientCardCommand { get; private set; }
        public ICommand DoctorCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        #endregion 
        
        public MainUCModel(IViewManager viewManager)
        {
           _viewManager = viewManager;
    
            InitiliseCommand();
        }

        public void InitiliseCommand()
        {
            DailyCommand = new RelayCommand(ExecuteDailyCommand);
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand);
            PatientCardCommand = new RelayCommand(ExecutePatientCardCommand);
            DoctorCommand = new RelayCommand(ExecuteDoctorCommand);
            LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
            ExitCommand = new RelayCommand(ExecuteExitCommand);
        }

        public void ExecuteDailyCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DailyViewModel);
        }

        public void ExecuteRegisterCommand()
        {
            _viewManager.ChangeView(TypesOfViews.RegisterViewModel);
        }

        public void ExecutePatientCardCommand()
        {
            _viewManager.ChangeView(TypesOfViews.PatientCardViewModel);
        }

        public void ExecuteDoctorCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DoctorViewModel);
        }

        public void ExecuteLogoutCommand()
        {
            _viewManager.ChangeView(TypesOfViews.LoginViewModel);
        }

        public void ExecuteExitCommand()
        {
            MessageBox.Show("TODO zamkniecie programu");
        }
    }
}
