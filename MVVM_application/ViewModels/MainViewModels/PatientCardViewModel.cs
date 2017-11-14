using GalaSoft.MvvmLight;
using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class PatientCardViewModel : ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public PatientCardViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }

    }
}
