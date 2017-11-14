using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class DoctorUCModel : ViewModelBase
    {
        private readonly IViewManager _viewManager;

        #region Command

        public ICommand DoctorDailyVisitCommand { get; private set; }
        public ICommand DoctorVisitCommand { get; private set; }
        public ICommand DoctorEditDataCommand { get; private set; }
        
        #endregion 
        
        public DoctorUCModel(IViewManager viewManager)
        {
            _viewManager = viewManager;

            InitialiseCommand();
        }
        public void InitialiseCommand()
        {
            DoctorDailyVisitCommand = new RelayCommand(ExecuteDoctorDailyVisitCommand);
            DoctorVisitCommand = new RelayCommand(ExecuteDoctorVisitCommand);
            DoctorEditDataCommand = new RelayCommand(ExecuteDoctorEditDataCommand);
        }

        public void ExecuteDoctorDailyVisitCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DoctorDailyVisitViewModel);
        }

        public void ExecuteDoctorVisitCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DoctorVisitView);
        }

        public void ExecuteDoctorEditDataCommand()
        {
            _viewManager.ChangeView(TypesOfViews.DoctorEditDataViewModel);
        }
    }
}
