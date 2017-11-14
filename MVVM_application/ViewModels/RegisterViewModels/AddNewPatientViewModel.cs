using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class AddNewPatientViewModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;
        
        public AddNewPatientViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
