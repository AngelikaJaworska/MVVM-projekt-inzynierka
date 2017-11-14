using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class AddVisitViewModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public AddVisitViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
