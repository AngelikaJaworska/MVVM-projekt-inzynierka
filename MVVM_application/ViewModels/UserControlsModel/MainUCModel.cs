using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class MainUCModel: ViewModelBase
    {
        private readonly IManager _manager;

        #region Command

        public ICommand DailyCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public ICommand PatientCardCommand { get; private set; }
        public ICommand DoctorCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        #endregion 
        
        public MainUCModel(IManager manager)
        {
           _manager = manager;
    
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
            _manager.ChangeView(TypesOfViews.DailyViewModel);
            
        }

        public void ExecuteRegisterCommand()
        {
            _manager.ChangeView(TypesOfViews.RegisterViewModel);
        }

        public void ExecutePatientCardCommand()
        {
            _manager.ChangeView(TypesOfViews.PatientCardViewModel);
        }

        public void ExecuteDoctorCommand()
        {
            _manager.ChangeView(TypesOfViews.DoctorViewModel);
        }

        public void ExecuteLogoutCommand()
        {
            _manager.ChangeView(TypesOfViews.LoginViewModel);
        }

        public void ExecuteExitCommand()
        {
            MessageBox.Show("Nastapi zamkniecie programu");
            Application.Current.Shutdown();
        }
    }
}
