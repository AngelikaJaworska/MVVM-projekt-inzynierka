using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientVisitViewModel: ViewModelBase
    {
        private readonly IManager _manager;

        public PatientVisitViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
