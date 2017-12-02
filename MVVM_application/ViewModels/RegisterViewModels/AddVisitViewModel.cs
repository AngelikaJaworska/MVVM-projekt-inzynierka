using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class AddVisitViewModel: ViewModelBase
    {
        private readonly IManager _manager;

        public AddVisitViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
