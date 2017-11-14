using GalaSoft.MvvmLight;
using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public RegisterViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
