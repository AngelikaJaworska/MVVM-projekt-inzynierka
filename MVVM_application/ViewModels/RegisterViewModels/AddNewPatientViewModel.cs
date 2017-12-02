using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class AddNewPatientViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        
        public AddNewPatientViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
