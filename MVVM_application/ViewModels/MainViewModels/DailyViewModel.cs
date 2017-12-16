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

        public RelayCommand ShowVisitCommand { get; private set; }

        public ShowVisitWindowDialogViewModel ShowVisitWDViewModel { get; set; }
        private ShowVisitWindowDialogModel _showVisitWindowDialogModel;

        public DailyViewModel(IManager manager, DailyModel dailyModel)
        {
            _manager = manager;
            _dailyModel = dailyModel;
            _receptionist = _manager.GetReceptionist();
            if (_receptionist.IDReceptionist != 0)
            {
                TodayVisitsList = new ObservableCollection<VisitManager>(_dailyModel.GetAllVisitsWithReceptionist(_receptionist.IDReceptionist));
            }
            ShowVisitCommand = new RelayCommand(ExecuteShowVisitCommand);
            
            _showVisitWindowDialogModel = new ShowVisitWindowDialogModel(_manager);
            ShowVisitWDViewModel = new ShowVisitWindowDialogViewModel(_manager, _showVisitWindowDialogModel);
        }

        private void ExecuteShowVisitCommand()
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
                MessageBox.Show("Prosze wybrac wizyte zaznaczajac ja na liscie");
            }
        }
    }
}