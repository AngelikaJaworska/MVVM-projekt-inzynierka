using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class DoctorVisitHoursWindowDialogViewModel : ViewModelBase
    {
        private IManager _manager;
        private DoctorVisitHoursWindowDialogModel _doctorVisitHoursWindowDialogModel;
        
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
        
        public RelayCommand<DoctorVisitHoursWindowDialog> CloseCommand { get; private set; }

        public DoctorVisitHoursWindowDialogViewModel(IManager manager, DoctorVisitHoursWindowDialogModel doctorVisitHoursWindowDialogModel)
        {
            _manager = manager;
            _doctorVisitHoursWindowDialogModel = doctorVisitHoursWindowDialogModel;
            CloseCommand = new RelayCommand<DoctorVisitHoursWindowDialog>(ExecuteCloseCommand);
            if (_manager.GetDoctor() != null)
            {
                FillData();
            }        
        }

       private void FillData()
        {
            _startHour = _doctorVisitHoursWindowDialogModel.SetStartHour();
            _endHour = _doctorVisitHoursWindowDialogModel.SetEndHour();
        }

        private void ExecuteCloseCommand(DoctorVisitHoursWindowDialog windowDoctorVisitHours)
        {
            FillData();
            windowDoctorVisitHours.DialogResult = true;
        }

    }
}
