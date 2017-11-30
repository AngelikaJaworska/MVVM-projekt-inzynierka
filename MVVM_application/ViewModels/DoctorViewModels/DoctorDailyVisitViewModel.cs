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
    public class DoctorDailyVisitViewModel:ViewModelBase
    {
        private readonly IViewManager _viewManager;
        private readonly DoctorDailyVisitModel _doctorDailyVisitModel;
        private Doctor _doctor;
        
        private ObservableCollection<VisitManager> _doctorDailyVisitsList;
        public ObservableCollection<VisitManager> DoctorDailyVisitsList
        {
            get { return _doctorDailyVisitsList; }
            set
            {
                _doctorDailyVisitsList = value;
                RaisePropertyChanged("DoctorDailyVisitsList");
            }
        }

        public DoctorDailyVisitViewModel(IViewManager viewManager, DoctorDailyVisitModel doctorDailyVisitModel)
        {
            _viewManager = viewManager;
            _doctorDailyVisitModel = doctorDailyVisitModel;
            _doctor = _viewManager.GetDoctor();
            if(_doctor != null)
            {
                DoctorDailyVisitsList = new ObservableCollection<VisitManager>(_doctorDailyVisitModel.GetAllDailyVisitsWithDoctor(_doctor));
            }
        }
    }
}
