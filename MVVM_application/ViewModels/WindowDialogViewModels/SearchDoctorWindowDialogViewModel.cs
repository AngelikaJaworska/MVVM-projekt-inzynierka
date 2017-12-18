using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using MVVM_application.Views;
using System.Windows;
using System.Collections.ObjectModel;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.Manager;

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class SearchDoctorWindowDialogViewModel: ViewModelBase
    {
        private IManager _manager;

        private string _doctor;
        private string _specialisation;
        private List<string> _doctorNameList;

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

        private SearchDoctorWindowDialogModel _addDoctorWindowDialogModel;

        public ObservableCollection<string> SpecialisationtList { get; set; }
        public ObservableCollection<string> DoctorList { get; set; }

        public RelayCommand<SearchDoctorWindowDialog> SearchDoctorCommand { get; private set; }
        public RelayCommand<SearchDoctorWindowDialog> SkipDoctorCommand { get; private set; }
        public RelayCommand<SearchDoctorWindowDialog> CancelDoctorCommand { get; private set; }
        public RelayCommand<string> RefreshDoctorCommand { get; private set; }

        public SearchDoctorWindowDialogViewModel(IManager manager, SearchDoctorWindowDialogModel addDoctorWindowDialogModel)
        {
            _manager = manager;
            _addDoctorWindowDialogModel = addDoctorWindowDialogModel;
            FillData();
            InitialiseCommand();
        }
        private void FillData()
        {
            _doctorNameList = new List<string>();
            this.SpecialisationtList = new ObservableCollection<string>(_addDoctorWindowDialogModel.FillSpecialisationList());
            this.DoctorList = new ObservableCollection<string>();
        }

        private void InitialiseCommand()
        {
            SearchDoctorCommand = new RelayCommand<SearchDoctorWindowDialog>(ExecuteSearchDoctorCommand);
            SkipDoctorCommand = new RelayCommand<SearchDoctorWindowDialog>(ExecuteSkipDoctorCommand);
            CancelDoctorCommand = new RelayCommand<SearchDoctorWindowDialog>(ExecuteCancelDoctorCommand);
            RefreshDoctorCommand = new RelayCommand<string>(RefreshDoctorsInfo); 
        }
        
        private void ExecuteSearchDoctorCommand(SearchDoctorWindowDialog windowSearchdoctor)
        {
            var doctor = _addDoctorWindowDialogModel.SearchDoctor(_specialisation, _doctor);
            if (doctor != null)
            {
                _manager.SetDoctor(doctor);
                windowSearchdoctor.DialogResult = true;
                _manager.SetUnchangedView(false);
            }
            else
            {
                MessageBox.Show("Proszę wybrać odpowiednie dane");
            }

        }

        private void ExecuteCancelDoctorCommand(SearchDoctorWindowDialog windowSearchdoctor)
        {
            windowSearchdoctor.Close();
            _manager.SetUnchangedView(true);
        }

        private void ExecuteSkipDoctorCommand(SearchDoctorWindowDialog windowSearchdoctor)
        {
            if(_manager.GetDoctor() != null)
            {
                windowSearchdoctor.Close();
                _manager.SetUnchangedView(false);
            }
            else
            {
                MessageBox.Show("Proszę wybrać najpierw lekarza");
            }
        }

        private void RefreshDoctorsInfo(string specialisation)
        {
            if (specialisation != null)
            {
                _doctorNameList.Clear();
                _doctorNameList = _addDoctorWindowDialogModel.FillDoctorList(specialisation);
                this.DoctorList.Clear();
                for (int i = 0; i < _doctorNameList.Count; i++)
                {
                    this.DoctorList.Add(_doctorNameList[i]);
                }
            }
        }
    }
}
