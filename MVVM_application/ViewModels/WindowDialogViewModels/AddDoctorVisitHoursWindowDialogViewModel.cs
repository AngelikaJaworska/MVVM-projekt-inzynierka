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

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class AddDoctorVisitHoursWindowDialogViewModel : ViewModelBase
    {
        private IManager _manager;
        private AddDoctorVisitHoursWindowDialogModel _addDoctorVisitHoursWindowDialogModel;

        private List<string> _endHourList;
        public string _startHour;
        public string _endHour;

        public string StartHour
        {
            get { return _startHour; }

            set
            {
                _startHour = value;
                RaisePropertyChanged("StartHour");
            }
        }

        public string EndHour
        {
            get { return _endHour; }

            set
            {
                _endHour = value;
                RaisePropertyChanged("EndHour");
            }
        }

        public ObservableCollection<string> StartHourList { get; set; }
        public ObservableCollection<string> EndHourList { get; set; }

        public RelayCommand<AddDoctorVisitHoursWindowDialog> CloseCommand { get; private set; }
        public RelayCommand<AddDoctorVisitHoursWindowDialog> SaveCommand { get; private set; }
        public RelayCommand<string> RefreshEndHoursCommand { get; private set; }

        public AddDoctorVisitHoursWindowDialogViewModel(IManager manager, AddDoctorVisitHoursWindowDialogModel addDoctorVisitHoursWindowDialogModel)
        {
            _manager = manager;
            _addDoctorVisitHoursWindowDialogModel = addDoctorVisitHoursWindowDialogModel;
            FillData();
            InitialiseCommand();
        }

        private void FillData()
        {
            _endHourList = new List<string>();
            this.StartHourList = new ObservableCollection<string>(_addDoctorVisitHoursWindowDialogModel.SetStartHourList());
            this.EndHourList = new ObservableCollection<string>();
        }

        private void InitialiseCommand()
        {
            CloseCommand = new RelayCommand<AddDoctorVisitHoursWindowDialog>(ExecuteCloseCommand);
            SaveCommand = new RelayCommand<AddDoctorVisitHoursWindowDialog>(ExecuteSaveCommand);
            RefreshEndHoursCommand = new RelayCommand<string>(ExecuteRefreshEndHoursCommand);
        }

        private void ExecuteRefreshEndHoursCommand(string startHour)
        {
            if (startHour != null)
            {
                _endHourList.Clear();
                _endHourList = _addDoctorVisitHoursWindowDialogModel.FillEndHoursList(startHour);
                if(_endHourList != null)
                {
                    this.EndHourList.Clear();
                    for (int i = 0; i < _endHourList.Count; i++)
                    {
                        this.EndHourList.Add(_endHourList[i]);
                    }

                }
            }
        }

        private async void ExecuteSaveCommand(AddDoctorVisitHoursWindowDialog windowAddDoctorVisitHours)
        {
            if (_addDoctorVisitHoursWindowDialogModel.SaveData(_startHour, _endHour))
            {
                windowAddDoctorVisitHours.DialogResult = true;
            }
            else
            {
                //MessageBox.Show("Proszę uzupełnić odpowiednio dane");
                var message = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Proszę uzupełnić prawidłowo wszystkie dane");

            }
        }

        private void ExecuteCloseCommand(AddDoctorVisitHoursWindowDialog windowAddDoctorVisitHours)
        {
            windowAddDoctorVisitHours.DialogResult = true;
        }
    }
}
