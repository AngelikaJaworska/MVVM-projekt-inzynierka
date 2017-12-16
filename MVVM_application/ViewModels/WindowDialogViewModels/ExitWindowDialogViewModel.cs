using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Views;
using System.Windows;

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class ExitWindowDialogViewModel: ViewModelBase
    {
        public RelayCommand<ExitWindowDialog> ExitApplicationCommand;
        public RelayCommand<ExitWindowDialog> CancelExitCommand;

        public ExitWindowDialogViewModel()
        {
            ExitApplicationCommand = new RelayCommand<ExitWindowDialog>(ExecuteExitApplicationCommand);
            CancelExitCommand = new RelayCommand<ExitWindowDialog>(ExecuteCancelExitCommand);
        }

        private void ExecuteExitApplicationCommand(ExitWindowDialog windowExit)
        {
            windowExit.Close();
            Application.Current.Shutdown();
        }

        private void ExecuteCancelExitCommand(ExitWindowDialog windowExit)
        {
            windowExit.Close();
        }
    }
}
