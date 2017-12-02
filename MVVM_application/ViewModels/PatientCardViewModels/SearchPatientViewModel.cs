using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class SearchPatientViewModel : ViewModelBase
    {
        private readonly IManager _manager;

        public SearchPatientViewModel(IManager manager)
        {
            _manager = manager;
        }
    }
}
