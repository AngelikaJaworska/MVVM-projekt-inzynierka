using GalaSoft.MvvmLight;
using MVVM_application.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IManager _manager;

        public RegisterViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
