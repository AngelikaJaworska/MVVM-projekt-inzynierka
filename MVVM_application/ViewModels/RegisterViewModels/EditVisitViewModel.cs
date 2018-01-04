using System;
using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.RegisterModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MVVM_application.ViewModels.RegisterViewModels
{
    public class EditVisitViewModel : ViewModelBase
    {
        private readonly IManager _manager;
        private EditVisitModel _editVisitModel;
        private List<DateTime> _newVisitDateList;

        private string _patient;
        private string _doctor;
        private DateTime _visitDate;
        private DateTime _newVisitDate;

        public string Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                RaisePropertyChanged("Patient");
            }
        }
        public string Doctor
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                RaisePropertyChanged("Doctor");
            }
        }
        public DateTime VisitDate
        {
            get { return _visitDate; }
            set
            {
                _visitDate = value;
                RaisePropertyChanged("VisitDate");
            }
        }
        public DateTime NewVisitDate
        {
            get { return _newVisitDate; }
            set
            {
                _newVisitDate = value;
                RaisePropertyChanged("NewVisitDate");
            }
        }

        public ObservableCollection<DateTime> VisitDateList { get; set; }
        public ObservableCollection<DateTime> NewVisitDateList { get; set; }
        
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand<DateTime> RefreshNewVisitCommand { get; private set; }
        public RelayCommand DeleteVisitCommand { get; private set; }

        public EditVisitViewModel(IManager manager, EditVisitModel editVisitModel)
        {
            _manager = manager;
            _editVisitModel = editVisitModel;
            if (_manager.GetDoctor() != null && _manager.GetPatient() != null)
            {
                FillData();
            }
            InitialiseCommand();
        }

        private void FillData()
        {
            _newVisitDateList = new List<DateTime>();
            _patient = _editVisitModel.SetPatient();
            _doctor = _editVisitModel.SetDoctor();
            this.VisitDateList = new ObservableCollection<DateTime>(_editVisitModel.FillVisitList());
            this.NewVisitDateList = new ObservableCollection<DateTime>();
        }

        private void InitialiseCommand()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
            RefreshNewVisitCommand = new RelayCommand<DateTime>(ExecuteRefreshNewVisitCommand);
            DeleteVisitCommand = new RelayCommand(ExecuteDeleteVisitCommand);
        }

        private async void ExecuteDeleteVisitCommand()
        {
            if(_visitDate != null)
            {
                if(_editVisitModel.DeleteVisit(_visitDate))
                {
                    var message1 = await MetroMessageBoxManager.ShowMessageAsync("", "Wizyta została odwołana");
                    this.VisitDateList.Remove(_visitDate);
                    if (VisitDateList.Count == 0)
                    {
                        var message2 = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Brak kolejnych wizyt do edycji");
                        _manager.RefreshAll(TypesOfViews.DailyViewModel);
                    }
                }
                else
                {
                    var message3 = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Nie wybrano daty wizyty do usunięcia");
                }
            }
            else
            {
                var message4 = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Proszę wybrać wizytę do odwołania");
            }
        }

        private void ExecuteRefreshNewVisitCommand(DateTime visit)
        {
            if (visit != null)
            {
                _newVisitDateList.Clear();
                _newVisitDateList = _editVisitModel.FillNewVisitDateList(visit);
                this.NewVisitDateList.Clear();
                for (int i = 0; i < _newVisitDateList.Count; i++)
                {
                    this.NewVisitDateList.Add(_newVisitDateList[i]);
                }
            }
        }

        private void ExecuteCancelCommand()
        {
            _manager.ChangeView(TypesOfViews.DailyViewModel);
        }

        private async void ExecuteSaveCommand()
        {
            if(_visitDate != null && _newVisitDate != null)
            {
               if( _editVisitModel.ChangeVisitDate(_visitDate, _newVisitDate))
                {
                    var message1 = await MetroMessageBoxManager.ShowMessageAsync("", "Wizyta została edytowana");
                    this.VisitDateList.Remove(_visitDate);
                    this.NewVisitDateList.Add(_newVisitDate);

                    if (VisitDateList.Count == 0)
                    {
                        var message2 = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Brak kolejnych wizyt do edycji");
                        _manager.RefreshAll(TypesOfViews.PatientVisitViewModel);
                    }
                }
                else
                {
                    var message3 = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Nie wybrano daty wizyty do edycji");
                }
            }
            else
            {
                var message4 = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Proszę uzupełnić prawidłowo wszystkie dane");
            }
        }

    }
}
