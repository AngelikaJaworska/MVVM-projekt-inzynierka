using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using MVVM_application.Models;
using System.ComponentModel;
using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private IViewManager _viewManager;

        public ICommand LoginCommand { get; private set; }

          public LoginViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;

            LoginCommand = new RelayCommand(ExecuteLoginViewCommand);
        }

        private void ExecuteLoginViewCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DailyViewModel);
        }
    }
}