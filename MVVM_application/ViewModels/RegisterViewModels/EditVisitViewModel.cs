using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class EditVisitViewModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public EditVisitViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
