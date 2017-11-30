using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;
using MVVM_application.Models.Manager;

using MVVM_application.ViewModels.MainViewModels;
using MVVM_application.ViewModels.RegisterViewModels;
using MVVM_application.ViewModels.PatientCardViewModels;
using MVVM_application.ViewModels.DoctorViewModels;
using MVVM_application.ViewModels.UserControlsModel;

using MVVM_application.Models.MainModels;
using MVVM_application.Models;

namespace MVVM_application.ViewModels
{
    public class MainViewModel : ViewModelBase, IViewManager//, IModelManager
    {
        #region Model

        Clinic _database;
        Receptionist _reception;
        Doctor _doctor;

        #endregion

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

        private SearchPatientViewModel _searchPatientViewModel;
        private PatientVisitViewModel _patientVisitViewModel;
        private PatientNewVisitViewModel _patientNewVisitViewModel;
        private PatientEditDataViewModel _patientEditDataViewModel;

        private SearchDoctorViewModel _searchDoctorViewModel;
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
            get { return _currentViewModel;}
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
        DailyModel _dailyModel;
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
            _reception = new Receptionist();
        }

        public void InitialiseAllViewModels()
        {
            _loginViewModel = new LoginViewModel(this, _loginModel);
            _dailyViewModel = new DailyViewModel(this, _dailyModel);
            _registerViewModel = new RegisterViewModel(this);
            _patientCardViewModel = new PatientCardViewModel(this);
            _doctorViewModel = new DoctorViewModel(this);

            _addNewPatientViewModel = new AddNewPatientViewModel(this);
            _addVisitViewModel = new AddVisitViewModel(this);
            _editVisitViewModel = new EditVisitViewModel(this);


            _searchPatientViewModel = new SearchPatientViewModel(this);
            _patientVisitViewModel = new PatientVisitViewModel(this);
            _patientNewVisitViewModel = new PatientNewVisitViewModel(this);
            _patientEditDataViewModel = new PatientEditDataViewModel(this);

            _searchDoctorViewModel = new SearchDoctorViewModel(this);
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
            _dailyModel = new DailyModel(this);
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

                case TypesOfViews.SearchPatientViewModel:
                    return _searchPatientViewModel;
                case TypesOfViews.PatientNewVisitViewModel:
                    return _patientNewVisitViewModel;
                case TypesOfViews.PatientVisitViewModel:
                    return _patientVisitViewModel;
                case TypesOfViews.PatientEditDataViewModel:
                    return _patientEditDataViewModel;

                case TypesOfViews.SearchDoctorViewModel:
                    return _searchDoctorViewModel;
                case TypesOfViews.DoctorDailyVisitViewModel:
                    return _doctorDailyVisitViewModel;
                case TypesOfViews.DoctorEditDataViewModel:
                    return _doctorEditDataViewModel;
                case TypesOfViews.DoctorVisitViewModel:
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

        public void RefreshViewModel(TypesOfViews view)
        {
            if (_reception.IDReceptionist != 0)
            {
                InitialiseAllViewModels();
                ChangeView(view);
            }
        }

        public Clinic GetDatabase()
        {
            return _database;
        }

        public void SetReceptionist(Receptionist reception)
        {
            _reception = reception;
        }

        public Receptionist GetReceptionist()
        {
            return _reception;
        }

        public Doctor GetDoctor()
        {
            return _doctor;
        }

        public void SetDoctor(Doctor doctor)
        {
            _doctor = doctor;
        }

        //public ModelBase GetModel(TypesOfModels model)
        //{
        //    switch (model)
        //    {
        //        case TypesOfModels.LoginModel:
        //            return _loginModel;
        //        case TypesOfModels.DailyModel:
        //            return _dailyModel;
        //        //    case TypesOfModels.RegisterModel:
        //        //        return _registerModel;
        //        //    case TypesOfModels.PatientCardModel:
        //        //        return _patientCardModel;
        //        //    case TypesOfModels.DoctorModel:
        //        //        return _doctorModel;

        //        //    case TypesOfModels.AddNewPatientModel:
        //        //        return _addNewPatientModel;
        //        //    case TypesOfModels.AddVisitModel:
        //        //        return _addVisitModel;
        //        //    case TypesOfModels.EditVisitModel:
        //        //        return _editVisitModel;

        //        //    case TypesOfModels.SearchPatientModel:
        //        //        return _searchPatientModel;
        //        //    case TypesOfModels.PatientNewVisitModel:
        //        //        return _patientNewVisitModel;
        //        //    case TypesOfModels.PatientVisitModel:
        //        //        return _patientVisitModel;
        //        //    case TypesOfModels.PatientEditDataModel:
        //        //        return _patientEditDataModel;

        //        //    case TypesOfModels.SearchDoctorModel:
        //        //        return _searchDoctorModel;
        //        //    case TypesOfModels.DoctorDailyVisitModel:
        //        //        return _doctorDailyVisitModel;
        //        //    case TypesOfModels.DoctorEditDataModel:
        //        //        return _doctorEditDataModel;
        //        //    case TypesOfModels.DoctorVisitModel:
        //        //        return _doctorVisit;

        //        default:
        //            return null;
        //    }
        //}
    }
}