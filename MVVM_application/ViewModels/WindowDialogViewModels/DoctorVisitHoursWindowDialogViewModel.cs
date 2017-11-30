using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.ViewModels.Manager;
using MVVM_application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class DoctorVisitHoursWindowDialogViewModel : ViewModelBase
    {
        IViewManager _viewManager;

        public string _startHour;
        public string _endHour;

        public string StartHour
        {
            get { return _startHour;}

            set
            {
                _startHour = value;
                RaisePropertyChanged("StartHour");
            }
        }

        public string EndHour
        {
            get { return _endHour;}

            set
            {
                _endHour = value;
                RaisePropertyChanged("EndHour");
            }
        }

        public RelayCommand<DoctorVisitHoursWindowDialog> Close { get; private set; }

        public DoctorVisitHoursWindowDialogViewModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
            Close = new RelayCommand<DoctorVisitHoursWindowDialog>(ExecuteClose);
        }

        private void ExecuteClose(DoctorVisitHoursWindowDialog windowDoctorVisitHours)
        {
            windowDoctorVisitHours.DialogResult = true;
        }
    }
}
