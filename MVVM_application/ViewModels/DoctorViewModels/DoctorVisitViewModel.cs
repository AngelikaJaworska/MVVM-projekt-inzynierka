using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.DoctorModels;
using System.Collections.ObjectModel;
using MVVM_application.Models.Manager;

namespace MVVM_application.ViewModels.DoctorViewModels
{
    public class DoctorVisitViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly DoctorVisitModel _doctorVisitModel;
        private Doctor _doctor;

        private ObservableCollection<VisitManager> _doctorVisitsList;
        public ObservableCollection<VisitManager> DoctorVisitsList
        {
            get { return _doctorVisitsList; }
            set
            {
                _doctorVisitsList = value;
                RaisePropertyChanged("DoctorVisitsList");
            }
        }

        public DoctorVisitViewModel(IManager manager, DoctorVisitModel doctorVisitModel)
        {
            _manager = manager;
            _doctorVisitModel = doctorVisitModel;
            _doctor = _manager.GetDoctor();

            if (_doctor != null)
            {
                DoctorVisitsList = new ObservableCollection<VisitManager>(_doctorVisitModel.GetAllVisitsWithDoctor(_doctor));
            }
        }
    }
}
