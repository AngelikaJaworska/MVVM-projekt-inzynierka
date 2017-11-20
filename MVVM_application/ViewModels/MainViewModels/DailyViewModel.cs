using GalaSoft.MvvmLight;

using MVVM_application.ViewModels.Manager;
using MVVM_application.Models.MainModels;
using MVVM_application.Models.Manager;
using System.Collections.ObjectModel;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class DailyViewModel : ViewModelBase
    {
        private readonly IViewManager _viewManager;
        private readonly DailyModel _dailyModel;
        
        private Receptionist _receptionist;

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

        public DailyViewModel(IViewManager viewManager, DailyModel dailyModel)
        {
            _viewManager = viewManager;
            _dailyModel = dailyModel;
            _receptionist = _viewManager.GetReceptionist();
            if (_receptionist.IDReceptionist != 0)
            {
                TodayVisitsList = new ObservableCollection<VisitManager>(_dailyModel.GetAllVisitsWithReceptionist(_receptionist.IDReceptionist));
            }
        }
    }
}