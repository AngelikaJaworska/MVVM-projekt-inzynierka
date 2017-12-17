﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.DoctorModels;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Views;
using MVVM_application.Views.WindowDialogViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.ViewModels.DoctorViewModels
{
    public class AddNewDoctorViewModel :ViewModelBase
    {
        private IManager _manager;
        private AddNewDoctorModel _addNewDoctorModel;

        private string _name;
        private string _specialisation;
        private string _surname;
        private string _street;
        private string _homeNr;
        private string _city;
        private string _phone;
        private string _dateOfBirth;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
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

        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                RaisePropertyChanged("DateOfBirth");
            }
        }

        public ObservableCollection<string> SpecialisationtList { get; set; }
        
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand VisitHoursCommand { get; private set; }


        private AddDoctorVisitHoursWindowDialogModel _addDoctorVisitHoursWindowDialogModel;
        public AddDoctorVisitHoursWindowDialogViewModel AddDoctorVisitHoursWDViewModel { get; set; }

        public AddNewDoctorViewModel(IManager manager, AddNewDoctorModel addNewDoctorModel)
        {
            _manager = manager;
            _addNewDoctorModel = addNewDoctorModel;

            SpecialisationtList = new ObservableCollection<string>(_addNewDoctorModel.FillSpecialisationList());
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
            VisitHoursCommand = new RelayCommand(ExecuteVisitHoursCommand);

            _addDoctorVisitHoursWindowDialogModel = new AddDoctorVisitHoursWindowDialogModel(_manager);
            AddDoctorVisitHoursWDViewModel = new AddDoctorVisitHoursWindowDialogViewModel(_manager, _addDoctorVisitHoursWindowDialogModel);

        }

        private void ExecuteVisitHoursCommand()
        {
            AddDoctorVisitHoursWindowDialog addDoctorVisitHourWindowDialog = new AddDoctorVisitHoursWindowDialog();
            addDoctorVisitHourWindowDialog.ShowDialog();
        }

        private void ExecuteCancelCommand()
        {
            _manager.ChangeView(TypesOfViews.DoctorViewModel);
        }

        private void ExecuteSaveCommand()
        {
            if(_addNewDoctorModel.CreateDoctor(_name, _surname, _specialisation, _street, _homeNr, _city, _phone, _dateOfBirth))
            {
                MessageBox.Show("Lekarz zapisany");
                _manager.RefreshAll(TypesOfViews.SearchDoctorViewModel);
            }
            else
            {
                MessageBox.Show("Prosze uzupelnic dane");
            }
        }

        
    }
}
