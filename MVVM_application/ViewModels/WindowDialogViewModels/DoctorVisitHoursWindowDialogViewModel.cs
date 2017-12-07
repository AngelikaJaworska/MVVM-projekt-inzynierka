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

        private List<string> _endHourList;
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

        public ObservableCollection<string> StartHourList { get; set; }
        public ObservableCollection<string> EndHourList { get; set; }

        public RelayCommand<DoctorVisitHoursWindowDialog> CloseCommand { get; private set; }
        public RelayCommand<DoctorVisitHoursWindowDialog> SaveCommand { get; private set; }
        public RelayCommand<string> RefreshEndHoursCommand { get; private set; }

        public DoctorVisitHoursWindowDialogViewModel(IManager manager, DoctorVisitHoursWindowDialogModel doctorVisitHoursWindowDialogModel)
        {
            _manager = manager;
            _doctorVisitHoursWindowDialogModel = doctorVisitHoursWindowDialogModel;
            InitialiseCommand();
            if (_manager.GetDoctor() != null)
            {
                FillData();
            }        
        }

        private void InitialiseCommand()
        {
            CloseCommand = new RelayCommand<DoctorVisitHoursWindowDialog>(ExecuteCloseCommand);
            SaveCommand = new RelayCommand<DoctorVisitHoursWindowDialog>(ExecuteSaveCommand);
            RefreshEndHoursCommand = new RelayCommand<string>(ExecuteRefreshEndHoursCommand);
        }

        private void ExecuteRefreshEndHoursCommand(string startHour)
        {
            if (startHour != null)
            {
                _endHourList.Clear();
                _endHourList = _doctorVisitHoursWindowDialogModel.FillEndHoursList(startHour);
                this.EndHourList.Clear();
                for (int i = 0; i < _endHourList.Count; i++)
                {
                    this.EndHourList.Add(_endHourList[i]);
                }
            }
        }

        private void FillData()
        {
            _endHourList = new List<string>();
            _startHour = _doctorVisitHoursWindowDialogModel.SetStartHour();
            _endHour = _doctorVisitHoursWindowDialogModel.SetEndHour();
            this.StartHourList = new ObservableCollection<string>( _doctorVisitHoursWindowDialogModel.SetStartHourList());
            this.EndHourList = new ObservableCollection<string>();
        }

        private void ExecuteCloseCommand(DoctorVisitHoursWindowDialog windowDoctorVisitHours)
        {
            FillData();
            windowDoctorVisitHours.DialogResult = true;
        }

        private void ExecuteSaveCommand(DoctorVisitHoursWindowDialog windowDoctorVisitHours)
        {
            if(_doctorVisitHoursWindowDialogModel.SaveData(_startHour, _endHour))
            {
                windowDoctorVisitHours.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Proszę uzupełnić odpowiednio dane");
            }
        }

    }
}
