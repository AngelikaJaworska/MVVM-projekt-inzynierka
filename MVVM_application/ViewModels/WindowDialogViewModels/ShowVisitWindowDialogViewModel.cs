using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_application.Manager;
using MVVM_application.Models.WindowDialogModels;
using MVVM_application.Views.WindowDialogViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.ViewModels.WindowDialogViewModels
{
    public class ShowVisitWindowDialogViewModel: ViewModelBase
    {
        private IManager _manager;
        private ShowVisitWindowDialogModel _showVisitWindowDialogModel;

        public string _patientInfo;
        public string _doctorInfo;
        public string _dateInfo;

        public string PatientInfo
        {
            get { return _patientInfo; }

            set
            {
                _patientInfo = value;
                RaisePropertyChanged("PatientInfo");
            }
        }

        public string DoctorInfo
        {
            get { return _doctorInfo; }

            set
            {
                _doctorInfo = value;
                RaisePropertyChanged("DoctorInfo");
            }
        }

        public string DateInfo
        {
            get { return _dateInfo; }

            set
            {
                _dateInfo = value;
                RaisePropertyChanged("DateInfo");
            }
        }

        public RelayCommand<ShowVisitWindowDialog> CloseCommand { get; private set; }
        public RelayCommand<ShowVisitWindowDialog> EditVisitCommand { get; private set; }
        public RelayCommand<ShowVisitWindowDialog> DeleteVisitCommand { get; private set; }

        public ShowVisitWindowDialogViewModel(IManager manager, ShowVisitWindowDialogModel showvisitWindowDialogModel)
        {
            _manager = manager;
            _showVisitWindowDialogModel = showvisitWindowDialogModel;
            InitialiseCommand();
            _patientInfo = _showVisitWindowDialogModel.SetPatient();
            _doctorInfo = _showVisitWindowDialogModel.SetDocotr();
            _dateInfo = _showVisitWindowDialogModel.SetDate();
        }

        public void InitialiseCommand()
        {
            CloseCommand = new RelayCommand<ShowVisitWindowDialog>(ExecuteCloseCommand);
            EditVisitCommand = new RelayCommand<ShowVisitWindowDialog>(ExecuteEditVisitCommand);
            DeleteVisitCommand = new RelayCommand<ShowVisitWindowDialog>(ExecuteDeleteVisitCommand);
        }

        private void ExecuteDeleteVisitCommand(ShowVisitWindowDialog windowShowVisit)
        {
            if (_showVisitWindowDialogModel.DeleteVisit())
            {
                MessageBox.Show("Wizyta odwołana");
                _manager.RefreshAll(TypesOfViews.DailyViewModel);
            }
            windowShowVisit.Close();
        }

        private void ExecuteEditVisitCommand(ShowVisitWindowDialog windowShowVisit)
        {
            if (_showVisitWindowDialogModel.EditVisit())
            {
                _manager.RefreshAll(TypesOfViews.EditVisitViewModel);
            }
            windowShowVisit.Close();

        }

        private void ExecuteCloseCommand(ShowVisitWindowDialog windowShowVisit)
        {
            windowShowVisit.Close();
        }
    }
}
