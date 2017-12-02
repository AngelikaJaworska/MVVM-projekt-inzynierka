using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Views;
using MVVM_application.Models.WindowDialogModels;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class DoctorUCModel : ViewModelBase
    {
        private readonly IManager _manager;
        private readonly SearchDoctorWindowDialogModel _searchDoctorWindowDialogModel;

        #region Command

        public RelayCommand SearchDoctorCommand { get; private set; }
        public ICommand DoctorDailyVisitCommand { get; private set; }
        public ICommand DoctorVisitCommand { get; private set; }
        public ICommand DoctorEditDataCommand { get; private set; }
        
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
            DoctorDailyVisitCommand = new RelayCommand(ExecuteDoctorDailyVisitCommand);
            DoctorVisitCommand = new RelayCommand(ExecuteDoctorVisitCommand);
            DoctorEditDataCommand = new RelayCommand(ExecuteDoctorEditDataCommand);
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

        public void ExecuteDoctorDailyVisitCommand()
        {
            _manager.ChangeView(TypesOfViews.DoctorDailyVisitViewModel);
        }

        public void ExecuteDoctorVisitCommand()
        {
            _manager.ChangeView(TypesOfViews.DoctorVisitViewModel);
        }

        public void ExecuteDoctorEditDataCommand()
        {
            if(_manager.GetDoctor() != null)
            {
                _manager.ChangeView(TypesOfViews.DoctorEditDataViewModel);
            }
            else
            {
                MessageBox.Show("Nie wybrano lekarza do edycji");
            }
        }
    }
}
