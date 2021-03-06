﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.DoctorModels;
using System.Collections.ObjectModel;
using MVVM_application.Models.Manager;
using GalaSoft.MvvmLight.Command;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.Views.WindowDialogViews;
using System.Windows;

namespace MVVM_application.ViewModels.DoctorViewModels
{
    public class DoctorVisitViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly DoctorVisitModel _doctorVisitModel;
        private Doctor _doctor;

        private VisitManager _visitManager;    
        private VisitManager _visitManagerObject;
        public VisitManager VisitManagerObject
        {
            get { return _visitManagerObject; }
            set
            {
                _visitManagerObject = value;
                if (_visitManagerObject != null)
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

        private string _doctorInfo;
        public string DoctorInfo
        {
            get { return _doctorInfo; }
            set
            {
                _doctorInfo = value;
                RaisePropertyChanged("DoctorInfo");
            }
        }

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

        public ShowVisitWindowDialogViewModel ShowVisitWDViewModel { get; set; }
        private ShowVisitWindowDialogModel _showVisitWindowDialogModel;

        public RelayCommand RefreshDateCommand { get; private set; }
        public RelayCommand ShowVisitCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }

        public DoctorVisitViewModel(IManager manager, DoctorVisitModel doctorVisitModel)
        {
            _manager = manager;
            _doctorVisitModel = doctorVisitModel;
            _doctor = _manager.GetDoctor();
            FillData();
            InitialiseCommand();            
        }

        public void FillData()
        {
            var date = DateTime.Parse("0001-01-01 00:00:00");

            if (_doctor != null)
            {
                if (_date != date)
                {
                    DoctorVisitsList = new ObservableCollection<VisitManager>(_doctorVisitModel.GetAllVisitsWithDoctor(_doctor, _date));
                }
                else
                {
                    DoctorVisitsList = new ObservableCollection<VisitManager>(_doctorVisitModel.GetAllVisitsWithDoctor(_doctor, DateTime.Today));
                }
                _doctorInfo = _doctorVisitModel.SetDoctorInfo();

            }

            this.DateList = new ObservableCollection<DateTime>(_doctorVisitModel.GetDate());

            _showVisitWindowDialogModel = new ShowVisitWindowDialogModel(_manager);
            ShowVisitWDViewModel = new ShowVisitWindowDialogViewModel(_manager, _showVisitWindowDialogModel);
        }

        public void InitialiseCommand()
        {
            ShowVisitCommand = new RelayCommand(ExecuteShowVisitCommand);
            RefreshDateCommand = new RelayCommand(ExecuteRefreshDateCommand);
            GoBackCommand = new RelayCommand(ExecuteGoBackCommand);
        }

        private void ExecuteGoBackCommand()
        {
            _manager.ChangeView(TypesOfViews.SearchDoctorViewModel);
        }

        private void ExecuteRefreshDateCommand()
        {
            if(_doctor != null)
            {
                this.DoctorVisitsList = new ObservableCollection<VisitManager>(_doctorVisitModel.GetAllVisitsWithDoctor(_doctor, _date));
            }
        }

        private async void ExecuteShowVisitCommand()
        {
            if (_visitManager != null)
            {
                _manager.SetVisitManager(_visitManager);
                _manager.RefreshAll(TypesOfViews.DoctorVisitViewModel);
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
