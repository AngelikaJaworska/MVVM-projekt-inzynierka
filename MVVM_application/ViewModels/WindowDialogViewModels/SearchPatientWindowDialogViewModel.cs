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
    public class SearchPatientWindowDialogViewModel : ViewModelBase
    {
        private IManager _manager;

        private string _pesel;
        private string _patient;
        private List<string> _patientNameList;

        public string Pesel
        {
            get { return _pesel; }
            set
            {
                _pesel = value;
                RaisePropertyChanged("Pesel");
            }
        }

        public string Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                RaisePropertyChanged("Patient");
            }
        }

        public ObservableCollection<string> PeselList { get; set; }
        public ObservableCollection<string> PatientList { get; set; }

        private SearchPatientWindowDialogModel _searchPatientWindowDialogModel;

        public RelayCommand<SearchPatientWindowDialog> SearchPatientCommand { get; private set; }
        public RelayCommand<SearchPatientWindowDialog> SkipPatientCommand { get; private set; }
        public RelayCommand<SearchPatientWindowDialog> CancelPatientCommand { get; private set; }
        public RelayCommand<string> RefreshPatientCommand { get; private set; }

        public SearchPatientWindowDialogViewModel(IManager manager, SearchPatientWindowDialogModel searchPatientWindowDialogModel)
        {
            _manager = manager;
            _searchPatientWindowDialogModel = searchPatientWindowDialogModel;
            _patientNameList = new List<string>();

            this.PeselList = new ObservableCollection<string>(_searchPatientWindowDialogModel.FillPeselList());
            this.PatientList = new ObservableCollection<string>();

            SearchPatientCommand = new RelayCommand<SearchPatientWindowDialog>(ExecuteSearchPatientCommand);
            SkipPatientCommand = new RelayCommand<SearchPatientWindowDialog>(ExecuteSkipPatientCommand);
            CancelPatientCommand = new RelayCommand<SearchPatientWindowDialog>(ExecuteCancelPatientCommand);
            RefreshPatientCommand = new RelayCommand<string>(ExecuteRefreshPatientCommand);
        }

        private void ExecuteRefreshPatientCommand(string pesel)
        {
            if (pesel != null)
            {
                _patientNameList.Clear();
                _patientNameList = _searchPatientWindowDialogModel.FillPatientList(pesel);
                this.PatientList.Clear();
                for (int i = 0; i < _patientNameList.Count; i++)
                {
                    this.PatientList.Add(_patientNameList[i]);
                }
            }
        }

        private void ExecuteCancelPatientCommand(SearchPatientWindowDialog windowSearchPatient)
        {
            windowSearchPatient.Close();
            _manager.SetUnchangedView(true);
        }

        private void ExecuteSearchPatientCommand(SearchPatientWindowDialog windowSearchPatient)
        {
            var patient = _searchPatientWindowDialogModel.SearchPatient(_pesel, _patient);
            if (patient != null)
            {
                _manager.SetPatient(patient);
                windowSearchPatient.DialogResult = true;
                _manager.SetUnchangedView(false);
            }
            else
            {
                MessageBox.Show("Prosze, wybrać odpowiednie dane");
            }
        }

        private void ExecuteSkipPatientCommand(SearchPatientWindowDialog windowSearchPatient)
        {
            windowSearchPatient.Close();
            _manager.SetUnchangedView(false);
        }
    }
}
