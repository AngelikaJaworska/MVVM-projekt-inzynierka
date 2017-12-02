using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class DoctorViewModel :ViewModelBase
    {
        private readonly IManager _manager;

        public DoctorViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
