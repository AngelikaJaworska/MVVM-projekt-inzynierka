using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class DoctorViewModel :ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public DoctorViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
