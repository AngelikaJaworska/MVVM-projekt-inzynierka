﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;
using MVVM_application.Models.DoctorModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using MVVM_application.Views;
using MVVM_application.ViewModels.WindowDialogViewModels;

namespace MVVM_application.ViewModels.DoctorViewModels
{
    public class SearchDoctorViewModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;
        private readonly SearchDoctorModel _searchDoctorModel;

        //private readonly DoctorVisitHoursWindowDialogModel _doctorVisitHoursWindowDialogModel;
        public DoctorVisitHoursWindowDialogViewModel DoctorVisitHoursWDViewModel { get; set; }

        private Doctor _doctor;

        private string _name;
        private string _surname;
        private string _street;
        private string _homeNr;
        private string _city;
        private string _phone;
        private string _specialisation;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                RaisePropertyChanged("Surname");
            }
        }
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                RaisePropertyChanged("Street");
            }
        }
        public string HomeNr
        {
            get { return _homeNr; }
            set
            {
                _homeNr = value;
                RaisePropertyChanged("HomeNr");
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged("City");
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }
        public string Specialisation
        {
            get { return _specialisation; }
            set
            {
                _specialisation = value;
                RaisePropertyChanged("Specialisation");
            }
        }

        public ICommand VisitHoursCommand { get; private set; }

        public SearchDoctorViewModel(IViewManager viewManager, SearchDoctorModel searchDoctorModel)
        {
            _viewManager = viewManager;
            _searchDoctorModel = searchDoctorModel;
            _doctor = _viewManager.GetDoctor();

            if(_doctor != null)
            {
                _name = _searchDoctorModel.GetDoctorName();
                _surname = _searchDoctorModel.GetDoctorSurame();
                _street = _searchDoctorModel.GetDoctorStreet();
                _homeNr = _searchDoctorModel.GetDoctorHomeNr();
                _city = _searchDoctorModel.GetDoctorCity();
                _phone = _searchDoctorModel.GetDoctorPhone();
                _specialisation = _searchDoctorModel.GetDoctorSpecialisation();

            }

            VisitHoursCommand = new RelayCommand(ExecuteVisitHoursCommand);
            DoctorVisitHoursWDViewModel = new DoctorVisitHoursWindowDialogViewModel(_viewManager);
        }

        private void ExecuteVisitHoursCommand()
        {
            DoctorVisitHoursWindowDialog doctorVisitHoursWindowDialog = new DoctorVisitHoursWindowDialog();
            doctorVisitHoursWindowDialog.ShowDialog();
        }
    }
}
