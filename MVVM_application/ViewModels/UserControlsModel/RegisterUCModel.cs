﻿using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.Views;
using MVVM_application.Models;
using MVVM_application.Views.WindowDialogViews;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class RegisterUCModel: ViewModelBase
    {
        private readonly IManager _manager;
        private SearchVisitToEditWindowDialogModel _searchVisitToEditWindowDialogModel;
        public SearchVisitToEditWindowDialogViewModel SearchVisitToEditWDViewModel { get; private set; }
                    
        public RelayCommand AddVisitCommand { get; private set; }
        public RelayCommand EditVisitCommand { get; private set; }
        public RelayCommand AddNewPatientCommand { get; private set; }
        
        
        public RegisterUCModel(IManager manager)
        {
            _manager = manager;
            FillData();
            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            AddVisitCommand = new RelayCommand(ExecuteAddVisitCommand);
            EditVisitCommand = new RelayCommand(ExecuteEditVisitCommand);
            AddNewPatientCommand = new RelayCommand(ExecuteAddNewPatientCommand);
        }
        
        public void FillData()
        {
            _searchVisitToEditWindowDialogModel = new SearchVisitToEditWindowDialogModel(_manager);
            SearchVisitToEditWDViewModel = new SearchVisitToEditWindowDialogViewModel(_manager, _searchVisitToEditWindowDialogModel);
        }
        
        public void ExecuteAddVisitCommand()
        {
            _manager.SetPatient(null);
            _manager.SetPatientList(null);
            SearchPatientWindowDialog searchPatientWindowDialog = new SearchPatientWindowDialog();
            searchPatientWindowDialog.ShowDialog();
            if (_manager.GetPatient() != null && _manager.GetUnchangedView() == false)
            {
                _manager.RefreshAll(TypesOfViews.PatientNewVisitViewModel);
            }
            else if (_manager.GetPatientList() != null && _manager.GetUnchangedView() == false)
            {
                _manager.RefreshViewModel();
                PatientListWindowDialog patientListWindowDialog = new PatientListWindowDialog();
                patientListWindowDialog.ShowDialog();
                if (_manager.GetUnchangedView() == false)
                {
                    _manager.RefreshAll(TypesOfViews.PatientNewVisitViewModel);
                }
            }
        }

        public void ExecuteEditVisitCommand()
        {
            SearchVisitToEditWindowDialog searchVisitToEditWindowDialog = new SearchVisitToEditWindowDialog();
            searchVisitToEditWindowDialog.ShowDialog();
            if (_manager.GetPatient() != null && _manager.GetUnchangedView() == false)
            {
                _manager.RefreshAll(TypesOfViews.EditVisitViewModel);
            }
        }

        public void ExecuteAddNewPatientCommand()
        {
            _manager.ChangeView(TypesOfViews.AddNewPatientViewModel);
        }
    }
}
