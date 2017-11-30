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
using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class AddDoctorWindowDialogViewModel: ViewModelBase
    {

        private IViewManager _viewManager;

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

        private AddDoctorWindowDialogModel _addDoctorWindowDialogModel;

        public ObservableCollection<string> SpecialisationtList { get; set; }
        public ObservableCollection<string> DoctorList { get; set; }

        public RelayCommand<AddDoctorWindowDialog> SearchDoctorCommand { get; private set; }
        public RelayCommand<AddDoctorWindowDialog> SkipDoctorCommand { get; private set; }
        public RelayCommand<AddDoctorWindowDialog> CancelDoctorCommand { get; private set; }
        public RelayCommand<string> RefreshDoctorCommand { get; private set; }

        public AddDoctorWindowDialogViewModel(IViewManager viewManager, AddDoctorWindowDialogModel addDoctorWindowDialogModel)
        {
            _viewManager = viewManager;
            _addDoctorWindowDialogModel = addDoctorWindowDialogModel;
            _doctorNameList = new List<string>();
            this.SpecialisationtList = new ObservableCollection<string>(_addDoctorWindowDialogModel.FillSpecialisationList());
            this.DoctorList = new ObservableCollection<string>();

            SearchDoctorCommand = new RelayCommand<AddDoctorWindowDialog>(ExecuteSearchDoctorCommand);
            SkipDoctorCommand = new RelayCommand<AddDoctorWindowDialog>(ExecuteSkipDoctorCommand);
            CancelDoctorCommand = new RelayCommand<AddDoctorWindowDialog>(ExecuteCancelDoctorCommand);
            RefreshDoctorCommand = new RelayCommand<string>(RefreshDoctorsInfo);
        }

        private void ExecuteSearchDoctorCommand(AddDoctorWindowDialog windowSearchdoctor)
        {
            var doctor = _addDoctorWindowDialogModel.SearchDoctor(_specialisation, _doctor);
            if (doctor != null)
            {
                _viewManager.SetDoctor(doctor);
                windowSearchdoctor.DialogResult = true;
                _viewManager.SetUnchangedView(false);
            }
            else
            {
                MessageBox.Show("Prosze, wybrać odpowiednie dane");
            }

        }

        private void ExecuteCancelDoctorCommand(AddDoctorWindowDialog windowSearchdoctor)
        {
            windowSearchdoctor.Close();
            _viewManager.SetUnchangedView(true);
        }

        private void ExecuteSkipDoctorCommand(AddDoctorWindowDialog windowSearchdoctor)
        {
            windowSearchdoctor.Close();
            _viewManager.SetUnchangedView(false);
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
