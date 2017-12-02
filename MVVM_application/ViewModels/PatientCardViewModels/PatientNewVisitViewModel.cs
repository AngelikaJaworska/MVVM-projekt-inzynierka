using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientNewVisitViewModel:ViewModelBase
    {
        private readonly IManager _manager;

        public PatientNewVisitViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
