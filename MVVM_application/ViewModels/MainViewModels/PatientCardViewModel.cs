using GalaSoft.MvvmLight;
using MVVM_application.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class PatientCardViewModel : ViewModelBase
    {
        private readonly IManager _manager;

        public PatientCardViewModel(IManager manager)
        {
            _manager = manager;
        }

    }
}
