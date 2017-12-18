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

        private SearchPatientWindowDialogModel _searchPatientWindowDialogModel;

        public RelayCommand<SearchPatientWindowDialog> SearchPatientCommand { get; private set; }
        public RelayCommand<SearchPatientWindowDialog> CancelPatientCommand { get; private set; }

        public SearchPatientWindowDialogViewModel(IManager manager, SearchPatientWindowDialogModel searchPatientWindowDialogModel)
        {
            _manager = manager;
            _searchPatientWindowDialogModel = searchPatientWindowDialogModel;
            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            SearchPatientCommand = new RelayCommand<SearchPatientWindowDialog>(ExecuteSearchPatientCommand);
            CancelPatientCommand = new RelayCommand<SearchPatientWindowDialog>(ExecuteCancelPatientCommand);
        }

        private void ExecuteCancelPatientCommand(SearchPatientWindowDialog windowSearchPatient)
        {
            windowSearchPatient.Close();
            _manager.SetUnchangedView(true);
        }

        private void ExecuteSearchPatientCommand(SearchPatientWindowDialog windowSearchPatient)
        {
            if(_pesel != null && _pesel != ""
                && _patient != null && _patient != "")
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
                    MessageBox.Show("Proszę uzupełnić poprawnie wszystkie dane");
                }
            }
            else if(_pesel != null && _pesel != "")
            {
                var patient = _searchPatientWindowDialogModel.SearchPatientByPesel(_pesel);
                if (patient != null)
                {
                    _manager.SetPatient(patient);
                    windowSearchPatient.DialogResult = true;
                    _manager.SetUnchangedView(false);
                }
                else
                {
                    MessageBox.Show("Proszę uzupełnić poprawnie dane");
                }
            }
            else if(_patient != null && _patient != "")
            {
                var patientList = _searchPatientWindowDialogModel.SearchPatientBySurname(_patient);
                if (patientList.Count > 0)
                {
                    _manager.SetPatientList(patientList);
                    windowSearchPatient.DialogResult = true;
                    _manager.SetUnchangedView(false);
                }
                else
                {
                    MessageBox.Show("Proszę uzupełnić poprawnie dane");
                }
            }
            else
            {
                MessageBox.Show("Proszę uzupelnić poprawnie wszystkie dane");
            }

        }
        
    }
}
