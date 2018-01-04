using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.MainModels;
using MVVM_application.Models.Manager;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using MVVM_application.Views.WindowDialogViews;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Models.WindowDialogModels;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class DailyViewModel : ViewModelBase
    {
        private readonly IManager _manager;
        private readonly DailyModel _dailyModel;
        private Receptionist _receptionist;

        private VisitManager _visitManager;
        private VisitManager _visitManagerObject;
        public VisitManager VisitManagerObject
        {
            get { return _visitManagerObject; }
            set
            {
                _visitManagerObject = value;
                if(_visitManagerObject != null)
                {
                    _visitManager = _visitManagerObject;
                }
                RaisePropertyChanged("VisitManagerObject");
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }

        private ObservableCollection<VisitManager> _todayVisitsList;
        public ObservableCollection<VisitManager> TodayVisitsList
        {
            get { return _todayVisitsList; }
            set
            {
                _todayVisitsList = value;
                RaisePropertyChanged("TodayVisitsList");
            }
        }

        private ObservableCollection<DateTime> _dateList;
        public ObservableCollection<DateTime> DateList
        {
            get { return _dateList; }
            set
            {
                _dateList = value;
                RaisePropertyChanged("DateList");
            }
        }

        public RelayCommand ShowVisitCommand { get; private set; }
        public RelayCommand RefreshDateCommand { get; private set; }

        public ShowVisitWindowDialogViewModel ShowVisitWDViewModel { get; set; }
        private ShowVisitWindowDialogModel _showVisitWindowDialogModel;

        public DailyViewModel(IManager manager, DailyModel dailyModel)
        {
            _manager = manager;
            _dailyModel = dailyModel;
            _receptionist = _manager.GetReceptionist();
            FillData();
            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            ShowVisitCommand = new RelayCommand(ExecuteShowVisitCommand);
            RefreshDateCommand = new RelayCommand(ExecuteRefreshDateCommand);
        }

        public void FillData()
        {
            var date = DateTime.Parse("0001-01-01 00:00:00");
            if (_receptionist.IDReceptionist != 0)
            {
                if (_date != date)
                {
                    this.TodayVisitsList = new ObservableCollection<VisitManager>(_dailyModel.GetAllVisitsWithReceptionist(_receptionist.IDReceptionist, _date));
                }
                else
                {
                    this.TodayVisitsList = new ObservableCollection<VisitManager>(_dailyModel.GetAllVisitsWithReceptionist(_receptionist.IDReceptionist, DateTime.Today));
                }
                this.DateList = new ObservableCollection<DateTime>(_dailyModel.GetDate());
            }

            _showVisitWindowDialogModel = new ShowVisitWindowDialogModel(_manager);
            ShowVisitWDViewModel = new ShowVisitWindowDialogViewModel(_manager, _showVisitWindowDialogModel);
        }

        private void ExecuteRefreshDateCommand()
        {
             this.TodayVisitsList = new ObservableCollection<VisitManager>(_dailyModel.GetAllVisitsWithReceptionist(_receptionist.IDReceptionist, _date));
        }

        private async void ExecuteShowVisitCommand()
        {
            if(_visitManager != null)
            {
                _manager.SetVisitManager(_visitManager);
                _manager.RefreshAll(TypesOfViews.DailyViewModel);
                ShowVisitWindowDialog showVisitWindowDialog = new ShowVisitWindowDialog();
                showVisitWindowDialog.ShowDialog();
            }
            else
            {
                var message = await MetroMessageBoxManager.ShowMessageAsync("Błąd", "Proszę wybrać wizytę zaznaczając ją na liście");
            }
        }
    }
}