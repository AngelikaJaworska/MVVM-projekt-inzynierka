using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientNewVisitViewModel:ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public PatientNewVisitViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
