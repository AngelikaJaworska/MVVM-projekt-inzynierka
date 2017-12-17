using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Views;
using MVVM_application.Models.WindowDialogModels;
using System;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class DoctorUCModel : ViewModelBase
    {
        private readonly IManager _manager;
        private readonly SearchDoctorWindowDialogModel _searchDoctorWindowDialogModel;

        #region Command

        public RelayCommand SearchDoctorCommand { get; private set; }
        public RelayCommand DoctorVisitCommand { get; private set; }
        public RelayCommand AddNewDoctorCommand { get; private set; }

        #endregion

        public SearchDoctorWindowDialogViewModel SearchDoctorWDModel {get;set;}

        public DoctorUCModel(IManager manager)
        {
            _manager = manager;
            _searchDoctorWindowDialogModel = new SearchDoctorWindowDialogModel(_manager);
            SearchDoctorWDModel = new SearchDoctorWindowDialogViewModel(_manager, _searchDoctorWindowDialogModel);

            InitialiseCommand();
        }
        public void InitialiseCommand()
        {
            SearchDoctorCommand = new RelayCommand(ExecuteSearchDoctorCommand);
            DoctorVisitCommand = new RelayCommand(ExecuteDoctorVisitCommand);
            AddNewDoctorCommand = new RelayCommand(ExecuteAddNewDoctorCommand);
        }

        private void ExecuteAddNewDoctorCommand()
        {
            _manager.ChangeView(TypesOfViews.AddNewDoctorViewModel);
        }

        private void ExecuteSearchDoctorCommand()
        {
            SearchDoctorWindowDialog searchDoctorWindowDialog = new SearchDoctorWindowDialog();
            searchDoctorWindowDialog.ShowDialog();
            if (_manager.GetUnchangedView() == false)
            {//ChangeView
                _manager.RefreshAll(TypesOfViews.SearchDoctorViewModel);
            }
        }
        

        public void ExecuteDoctorVisitCommand()
        {
            _manager.ChangeView(TypesOfViews.DoctorVisitViewModel);
        }


    }
}
