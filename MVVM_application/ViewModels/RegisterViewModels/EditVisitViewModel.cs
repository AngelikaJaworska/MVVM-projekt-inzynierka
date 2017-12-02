using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class EditVisitViewModel: ViewModelBase
    {
        private readonly IManager _manager;

        public EditVisitViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
