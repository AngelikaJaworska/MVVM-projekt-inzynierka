using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientEditDataViewModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public PatientEditDataViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
