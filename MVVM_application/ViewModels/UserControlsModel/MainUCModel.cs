using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.Views;
using MVVM_application.ViewModels.WindowDialogViewModels;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class MainUCModel: ViewModelBase
    {
        private readonly IManager _manager;
        private string _recepcionist;

        public string Recepcionist
        {
            get { return _recepcionist; }
            set
            {
                _recepcionist = value;
                RaisePropertyChanged("Recepcionist");
            }
        }
        
        #region Command

        public RelayCommand DailyCommand { get; private set; }
        public RelayCommand RegisterCommand { get; private set; }
        public RelayCommand PatientCardCommand { get; private set; }
        public RelayCommand DoctorCommand { get; private set; }
        public RelayCommand LogoutCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }

        #endregion 
        
        public MainUCModel(IManager manager)
        {
           _manager = manager;
            _recepcionist = "Zalogowany jako: "+ _manager.GetReceptionist().Login;
            
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
            _manager.RefreshAll(TypesOfViews.DailyViewModel);
            
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
            Application.Current.Shutdown();
        }
    }
}
