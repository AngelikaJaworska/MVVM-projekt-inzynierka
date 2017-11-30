using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Views;
using MVVM_application.Models.WindowDialogModels;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class DoctorUCModel : ViewModelBase
    {
        private readonly IViewManager _viewManager;
        private readonly AddDoctorWindowDialogModel _addDoctorWindowDialogModel;

        #region Command

        public RelayCommand SearchDoctorCommand { get; private set; }
        public ICommand DoctorDailyVisitCommand { get; private set; }
        public ICommand DoctorVisitCommand { get; private set; }
        public ICommand DoctorEditDataCommand { get; private set; }
        
        #endregion 

        public AddDoctorWindowDialogViewModel AddDoctorWDModel {get;set;}

        public DoctorUCModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
            _addDoctorWindowDialogModel = new AddDoctorWindowDialogModel(_viewManager);
            AddDoctorWDModel = new AddDoctorWindowDialogViewModel(_viewManager, _addDoctorWindowDialogModel);

            InitialiseCommand();
        }
        public void InitialiseCommand()
        {
            SearchDoctorCommand = new RelayCommand(ExecuteSearchDoctorCommand);
            DoctorDailyVisitCommand = new RelayCommand(ExecuteDoctorDailyVisitCommand);
            DoctorVisitCommand = new RelayCommand(ExecuteDoctorVisitCommand);
            DoctorEditDataCommand = new RelayCommand(ExecuteDoctorEditDataCommand);
        }

        private void ExecuteSearchDoctorCommand()
        {
            AddDoctorWindowDialog addDoctorWindowDialog = new AddDoctorWindowDialog();
            addDoctorWindowDialog.ShowDialog();
            if (_viewManager.GetUnchangedView() == false)
            {//ChangeView
                _viewManager.RefreshAll(TypesOfViews.SearchDoctorViewModel);
            }
        }

        public void ExecuteDoctorDailyVisitCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DoctorDailyVisitViewModel);
        }

        public void ExecuteDoctorVisitCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DoctorVisitViewModel);
        }

        public void ExecuteDoctorEditDataCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DoctorEditDataViewModel);
        }
    }
}
