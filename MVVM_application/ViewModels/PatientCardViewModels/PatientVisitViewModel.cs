using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientVisitViewModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public PatientVisitViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
