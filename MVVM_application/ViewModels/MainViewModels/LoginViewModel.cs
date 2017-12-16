﻿using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;

using MVVM_application.Manager;
using MVVM_application.Models.MainModels;
using MVVM_application.Views;
using MVVM_application.ViewModels.WindowDialogViewModels;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private IManager _manager;
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
        public ICommand ExitCommand { get; private set; }

        public LoginViewModel(IManager manager, LoginModel loginModel)
        {
            _manager = manager;
            _loginModel = loginModel;

            this.ReceptionistList = new ObservableCollection<Receptionist>(_loginModel.FillReceptionsList());
            
            LoginCommand = new RelayCommand(ExecuteLoginViewCommand);
            ExitCommand = new RelayCommand(ExecuteExitCommand);
        }

        private void ExecuteExitCommand()
        {
            Application.Current.Shutdown();
            //ExitWindowDialog exitWindowDialog = new ExitWindowDialog();
            //exitWindowDialog.ShowDialog();
        }

        private void ExecuteLoginViewCommand()
        {
            var reception = _loginModel.Login(_login, _password);
            if (reception != null)
            {
                _manager.SetReceptionist(reception);
                _manager.RefreshViewModel();
                _manager.ChangeView(TypesOfViews.DailyViewModel);
            }
            else
            {
                MessageBox.Show("Prosze, podac prawidlowe haslo");
            }
        }
    }
}