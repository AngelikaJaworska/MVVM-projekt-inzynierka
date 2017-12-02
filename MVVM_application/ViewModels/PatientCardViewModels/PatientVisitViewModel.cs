using GalaSoft.MvvmLight;

using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using MVVM_application.Models.PatientCardModels;
using System.Collections.ObjectModel;

namespace MVVM_application.ViewModels.PatientCardViewModels
{
    public class PatientVisitViewModel: ViewModelBase
    {
        private readonly IManager _manager;
        private readonly PatientVisitModel _patientVisitModel;

        private Patient _patient;
        private ObservableCollection<VisitManager> _patientVisitsList;
        public ObservableCollection<VisitManager> PatientVisitsList
        {
            get { return _patientVisitsList; }
            set
            {
                _patientVisitsList = value;
                RaisePropertyChanged("PatientVisitsList");
            }
        }

        public PatientVisitViewModel(IManager manager, PatientVisitModel patientVisitModel)
        {
            _manager = manager;
            _patientVisitModel = patientVisitModel;
            _patient = _manager.GetPatient();

            if(_patient != null)
            {
                PatientVisitsList = new ObservableCollection<VisitManager>(_patientVisitModel.GetAllVisitsWithPatient(_patient));
            }
        }
    }
}
