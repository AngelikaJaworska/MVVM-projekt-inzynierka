using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.MainModels;
using MVVM_application.Models.Manager;

namespace MVVM_application.ViewModels.MainViewModels
{
    public class DailyViewModel : ViewModelBase
    {
        private readonly IManager _manager;
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

        public DailyViewModel(IManager manager, DailyModel dailyModel)
        {
            _manager = manager;
            _dailyModel = dailyModel;
            _receptionist = _manager.GetReceptionist();
            if (_receptionist.IDReceptionist != 0)
            {
                TodayVisitsList = new ObservableCollection<VisitManager>(_dailyModel.GetAllVisitsWithReceptionist(_receptionist.IDReceptionist));
            }
        }
    }
}