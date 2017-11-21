using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class SearchPatientViewModel : ViewModelBase
    {
        private readonly IViewManager _viewManager;

        public SearchPatientViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}
