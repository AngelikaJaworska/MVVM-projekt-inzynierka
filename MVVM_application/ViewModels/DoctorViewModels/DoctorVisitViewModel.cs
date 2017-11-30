using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;
using MVVM_application.Models.DoctorModels;
using System.Collections.ObjectModel;
using MVVM_application.Models.Manager;

namespace MVVM_application.ViewModels.DoctorViewModels
{
    public class DoctorVisitViewModel: ViewModelBase
    {
        private readonly IViewManager _viewManager;
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

        public DoctorVisitViewModel(IViewManager viewManager, DoctorVisitModel doctorVisitModel)
        {
            _viewManager = viewManager;
            _doctorVisitModel = doctorVisitModel;
            _doctor = _viewManager.GetDoctor();

            if (_doctor != null)
            {
                DoctorVisitsList = new ObservableCollection<VisitManager>(_doctorVisitModel.GetAllVisitsWithDoctor(_doctor));
            }
        }
    }
}
