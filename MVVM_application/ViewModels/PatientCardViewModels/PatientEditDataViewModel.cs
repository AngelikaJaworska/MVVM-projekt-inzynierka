using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientEditDataViewModel: ViewModelBase
    {
        private readonly IManager _manager;

        public PatientEditDataViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
