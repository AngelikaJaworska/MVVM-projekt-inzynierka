using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class DailyViewModel : ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public DailyViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}