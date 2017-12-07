using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models;
using MVVM_application.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.ViewModels
{
    public class SearchVisitToEditWindowDialogViewModel :ViewModelBase
    {
        private IManager _manager;
        private SearchVisitToEditWindowDialogModel _searchVisitToEditWindowDialogModel;

        private Patient _patient;
        private string _patientName;
        private string _doctor;
        private string _specialisation;
        private List<string> _doctorNameList;

        public string Patient
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
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
        public string Specialisation
        {
            get { return _specialisation; }
            set
            {
                _specialisation = value;
                RaisePropertyChanged("Specialisation");
            }
        }

        public ObservableCollection<string> SpecialisationtList { get; set; }
        public ObservableCollection<string> DoctorList { get; set; }

        public RelayCommand<SearchVisitToEditWindowDialog> SearchVisitCommand { get; private set; }
        public RelayCommand<SearchVisitToEditWindowDialog> CancelVisitCommand { get; private set; }
        public RelayCommand<string> RefreshVisitCommand { get; private set; }

        public SearchVisitToEditWindowDialogViewModel(IManager manager, SearchVisitToEditWindowDialogModel searchVisitToEditWindowDialogModel)
        {
            _manager = manager;
            _searchVisitToEditWindowDialogModel = searchVisitToEditWindowDialogModel;
            _patient = _manager.GetPatient();
            if(_patient != null)
            {
                FillData();
            }
            InitialiseCommand();
        }

        private void FillData()
        {
            _patientName = _searchVisitToEditWindowDialogModel.SetPatient();
            _doctorNameList = new List<string>();
            this.SpecialisationtList = new ObservableCollection<string>(_searchVisitToEditWindowDialogModel.FillSpecialisationList());
            this.DoctorList = new ObservableCollection<string>();
        }

        private void InitialiseCommand()
        {
            SearchVisitCommand = new RelayCommand<SearchVisitToEditWindowDialog>(ExecuteSearchVisitCommand);
            CancelVisitCommand = new RelayCommand<SearchVisitToEditWindowDialog>(ExecuteCancelVisitCommand);
            RefreshVisitCommand = new RelayCommand<string>(ExecuteRefreshVisitCommand);
        }

        private void ExecuteRefreshVisitCommand(string specialisation)
        {
            if (specialisation != null)
            {
                _doctorNameList.Clear();
                _doctorNameList = _searchVisitToEditWindowDialogModel.FillDoctorList(specialisation);
                this.DoctorList.Clear();
                for (int i = 0; i < _doctorNameList.Count; i++)
                {
                    this.DoctorList.Add(_doctorNameList[i]);
                }
            }
        }

        private void ExecuteCancelVisitCommand(SearchVisitToEditWindowDialog windowEditVisit)
        {
            windowEditVisit.Close();
            _manager.SetUnchangedView(true);
        }
        
        private void ExecuteSearchVisitCommand(SearchVisitToEditWindowDialog windowEditVisit)
        {
            var doctor = _searchVisitToEditWindowDialogModel.SearchDoctor(_specialisation, _doctor);

            if (doctor != null)
            {
                if (_searchVisitToEditWindowDialogModel.CheckIfAnyVisitExist(doctor, _patient))
                {
                    _manager.SetDoctor(doctor);
                    windowEditVisit.DialogResult = true;
                    _manager.SetUnchangedView(false);
                }
                else
                {
                    MessageBox.Show("Brak wizyt dla wybranych danych");
                }
            }
            else
            {
                MessageBox.Show("Prosze, wybrać odpowiednie dane"); 
            }
            
        }
    }
}
