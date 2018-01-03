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
    public class EditDoctorVisitHoursWindowDialogViewModel: ViewModelBase
    {
        private IManager _manager;
        private EditDoctorVisitHoursWindowDialogModel _editDoctorVisitHoursWindowDialogModel;

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

        public RelayCommand<EditDoctorVisitHourWindowDialog> CloseCommand { get; private set; }
        public RelayCommand<EditDoctorVisitHourWindowDialog> SaveCommand { get; private set; }
        public RelayCommand<string> RefreshEndHoursCommand { get; private set; }

        public EditDoctorVisitHoursWindowDialogViewModel(IManager manager, EditDoctorVisitHoursWindowDialogModel editdoctorVisitHoursWindowDialogModel)
        {
            _manager = manager;
            _editDoctorVisitHoursWindowDialogModel = editdoctorVisitHoursWindowDialogModel;
            InitialiseCommand();
            if (_manager.GetDoctor() != null)
            {
                FillData();
            }
        }

        private void InitialiseCommand()
        {
            CloseCommand = new RelayCommand<EditDoctorVisitHourWindowDialog>(ExecuteCloseCommand);
            SaveCommand = new RelayCommand<EditDoctorVisitHourWindowDialog>(ExecuteSaveCommand);
            RefreshEndHoursCommand = new RelayCommand<string>(ExecuteRefreshEndHoursCommand);
        }

        private void ExecuteRefreshEndHoursCommand(string startHour)
        {
            if (startHour != null)
            {
                _endHourList.Clear();
                _endHourList = _editDoctorVisitHoursWindowDialogModel.FillEndHoursList(startHour);
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

        private void FillData()
        {
            _endHourList = new List<string>();
            _startHour = _editDoctorVisitHoursWindowDialogModel.SetStartHour();
            _endHour = _editDoctorVisitHoursWindowDialogModel.SetEndHour();
            this.StartHourList = new ObservableCollection<string>(_editDoctorVisitHoursWindowDialogModel.SetStartHourList());
            this.EndHourList = new ObservableCollection<string>();
        }

        private void ExecuteCloseCommand(EditDoctorVisitHourWindowDialog windowEditDoctorVisitHours)
        {
            FillData();
            windowEditDoctorVisitHours.DialogResult = true;
        }

        private async void ExecuteSaveCommand(EditDoctorVisitHourWindowDialog windowEditDoctorVisitHours)
        {
            if (_editDoctorVisitHoursWindowDialogModel.SaveData(_startHour, _endHour))
            {
                windowEditDoctorVisitHours.DialogResult = true;
            }
            else
            {
                //MessageBox.Show("Proszę uzupełnić odpowiednio dane");
                var message = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Proszę uzupełnić prawidłowo wszystkie dane");
            }
        }

    }
}
