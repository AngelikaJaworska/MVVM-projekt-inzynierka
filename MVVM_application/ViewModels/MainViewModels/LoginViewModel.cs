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
using MVVM_application.Models.MainModels;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private IViewManager _viewManager;
        private readonly LoginModel _loginModel;

        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }
        public ObservableCollection<Receptionist> ReceptionistList { get; set; }

        public ICommand LoginCommand { get; private set; }

          public LoginViewModel(IViewManager viewManager, LoginModel loginModel)
        {
            _viewManager = viewManager;
            _loginModel = loginModel;

            this.ReceptionistList = new ObservableCollection<Receptionist>(_loginModel.FillReceptionsList());

            LoginCommand = new RelayCommand(ExecuteLoginViewCommand);
        }

        private void ExecuteLoginViewCommand()
        {
            var reception = _loginModel.Login(_login, _password);
            if (reception != null)
            {
                _viewManager.SetReceptionist(reception);
                _viewManager.RefreshViewModel(TypesOfViews.DailyViewModel);
            }
            else
            {
                MessageBox.Show("Prosze, podac prawidlowe haslo");
            }
        }
    }
}