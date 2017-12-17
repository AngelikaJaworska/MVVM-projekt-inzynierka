using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using MVVM_application.Models.PatientCardModels;
using System.Collections.ObjectModel;
using System;
using MVVM_application.ViewModels.WindowDialogViewModels;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.Views.WindowDialogViews;
using System.Windows;

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

        public RelayCommand ShowVisitCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }

        public ShowVisitWindowDialogViewModel ShowVisitWDViewModel { get; set; }
        private ShowVisitWindowDialogModel _showVisitWindowDialogModel;

        public PatientVisitViewModel(IManager manager, PatientVisitModel patientVisitModel)
        {
            _manager = manager;
            _patientVisitModel = patientVisitModel;
            _patient = _manager.GetPatient();

            if(_patient != null)
            {
                PatientVisitsList = new ObservableCollection<VisitManager>(_patientVisitModel.GetAllVisitsWithPatient(_patient));
            }

            ShowVisitCommand = new RelayCommand(ExecuteShowVisitCommand);
            GoBackCommand = new RelayCommand(ExecuteGoBackCommand);

            _showVisitWindowDialogModel = new ShowVisitWindowDialogModel(_manager);
            ShowVisitWDViewModel = new ShowVisitWindowDialogViewModel(_manager, _showVisitWindowDialogModel);
        }

        private void ExecuteGoBackCommand()
        {
            _manager.ChangeView(TypesOfViews.SearchPatientViewModel);
        }

        private void ExecuteShowVisitCommand()
        {
            if (_visitManager != null)
            {
                _manager.SetVisitManager(_visitManager);
                _manager.RefreshAll(TypesOfViews.PatientVisitViewModel);
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
