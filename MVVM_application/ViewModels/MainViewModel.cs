using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;

using MVVM_application.ViewModels.MainViewModels;
using MVVM_application.ViewModels.RegisterViewModels;
using MVVM_application.ViewModels.PatientCardViewModels;
using MVVM_application.ViewModels.DoctorViewModels;
using MVVM_application.ViewModels.UserControlsModel;

using MVVM_application.Models.MainModels;

namespace MVVM_application.ViewModels
{
    public class MainViewModel : ViewModelBase, IViewManager
    {
        Clinic _database;

        #region ViewModel

        private ViewModelBase _currentViewModel;

        private LoginViewModel _loginViewModel;
        private DailyViewModel _dailyViewModel;
        private RegisterViewModel _registerViewModel;
        private PatientCardViewModel _patientCardViewModel;
        private DoctorViewModel _doctorViewModel;
        private AddNewPatientViewModel _addNewPatientViewModel;
        private AddVisitViewModel _addVisitViewModel;
        private EditVisitViewModel _editVisitViewModel;
        private PatientVisitViewModel _patientVisitViewModel;
        private PatientNewVisitViewModel _patientNewVisitViewModel;
        private PatientEditDataViewModel _patientEditDataViewModel;
        private DoctorDailyVisitViewModel _doctorDailyVisitViewModel;
        private DoctorEditDataViewModel _doctorEditDataViewModel;
        private DoctorVisitView _doctorVisitView;

        private MainUCModel _mainUcModel;
        private RegisterUCModel _registerUCModel;
        private PatientCardUCModel _patientCardUCModel;
        private DoctorUCModel _doctorUCModel;
        private ActionUCModel _actionUCModel;

        #region Properties

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            private set
            {
                if (_currentViewModel == value)
                {
                    return;
                }
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public MainUCModel MainUcModel
        {
            get { return _mainUcModel; }
            set
            {
                _mainUcModel = value;
                RaisePropertyChanged("MainUcModel");
            }
        }
        public RegisterUCModel RegisterUCModel
        {
            get { return _registerUCModel; }
            set
            {
                _registerUCModel = value;
                RaisePropertyChanged("RegisterUCModel");
            }
        }
        public PatientCardUCModel PatientCardUCModel
        {
            get { return _patientCardUCModel; }
            set
            {
                _patientCardUCModel = value;
                RaisePropertyChanged("PatientCardUCModel");
            }
        }
        public DoctorUCModel DoctorUCModel
        {
            get { return _doctorUCModel; }
            set
            {
                _doctorUCModel = value;
                RaisePropertyChanged("DoctorUCModel");
            }
        }
        public ActionUCModel ActionUCModel
        {
            get { return _actionUCModel; }
            set
            {
                _actionUCModel = value;
                RaisePropertyChanged("ActionUCModel");
            }
        }
        #endregion
        #endregion

        #region Model
        LoginModel _loginModel;
        #endregion

        public MainViewModel()
        {
            InitialiseDatabase();
            InitialiseAllModels();
            InitialiseAllViewModels();

            ChangeView(TypesOfViews.LoginViewModel);
        }

        public void InitialiseDatabase()
        {
            _database = new Clinic();
        }

        public void InitialiseAllViewModels()
        {
            _loginViewModel = new LoginViewModel(this, _loginModel);
            _dailyViewModel = new DailyViewModel(this);
            _registerViewModel = new RegisterViewModel(this);
            _patientCardViewModel = new PatientCardViewModel(this);
            _doctorViewModel = new DoctorViewModel(this);

            _addNewPatientViewModel = new AddNewPatientViewModel(this);
            _addVisitViewModel = new AddVisitViewModel(this);
            _editVisitViewModel = new EditVisitViewModel(this);

            _patientVisitViewModel = new PatientVisitViewModel(this);
            _patientNewVisitViewModel = new PatientNewVisitViewModel(this);
            _patientEditDataViewModel = new PatientEditDataViewModel(this);

            _doctorDailyVisitViewModel = new DoctorDailyVisitViewModel(this);
            _doctorEditDataViewModel = new DoctorEditDataViewModel(this);
            _doctorVisitView = new DoctorVisitView(this);

            _mainUcModel = new MainUCModel(this);
            _registerUCModel = new RegisterUCModel(this);
            _patientCardUCModel = new PatientCardUCModel(this);
            _doctorUCModel = new DoctorUCModel(this);
            _actionUCModel = new ActionUCModel(this);
        }

        public void InitialiseAllModels()
        {
            _loginModel = new LoginModel(this);
        }

        public void ChangeView(TypesOfViews view)
        {
            CurrentViewModel = GetView(view);
        }

        public ViewModelBase GetView(TypesOfViews view)
        {
            switch (view)
            {
                case TypesOfViews.LoginViewModel:
                    return _loginViewModel;
                case TypesOfViews.DailyViewModel:
                    return _dailyViewModel;
                case TypesOfViews.RegisterViewModel:
                    return _registerViewModel;
                case TypesOfViews.PatientCardViewModel:
                    return _patientCardViewModel;
                case TypesOfViews.DoctorViewModel:
                    return _doctorViewModel;

                case TypesOfViews.AddNewPatientViewModel:
                    return _addNewPatientViewModel;
                case TypesOfViews.AddVisitViewModel:
                    return _addVisitViewModel;
                case TypesOfViews.EditVisitViewModel:
                    return _editVisitViewModel;

                case TypesOfViews.PatientNewVisitViewModel:
                    return _patientNewVisitViewModel;
                case TypesOfViews.PatientVisitViewModel:
                    return _patientVisitViewModel;
                case TypesOfViews.PatientEditDataViewModel:
                    return _patientEditDataViewModel;

                case TypesOfViews.DoctorDailyVisitViewModel:
                    return _doctorDailyVisitViewModel;
                case TypesOfViews.DoctorEditDataViewModel:
                    return _doctorEditDataViewModel;
                case TypesOfViews.DoctorVisitView:
                    return _doctorVisitView;

                //case TypesOfViews.MainUCModel:
                //    return _mainUcModel;
                //case TypesOfViews.RegisterUCModel:
                //    return _registerUCModel;
                //case TypesOfViews.PatientCardUCModel:
                //    return _patientCardUCModel;
                //case TypesOfViews.DoctorUCModel:
                //    return _doctorUCModel;
                //case TypesOfViews.ActionUCModel:
                //    return _actionUCModel;
                default:
                    return null;
            }
        }

        public Clinic GetDatabase()
        {
            return _database;
        }
    }
}