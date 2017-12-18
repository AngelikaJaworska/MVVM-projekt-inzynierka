using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;
using MVVM_application.Models.Manager;

using MVVM_application.ViewModels.MainViewModels;
using MVVM_application.ViewModels.RegisterViewModels;
using MVVM_application.ViewModels.PatientCardViewModels;
using MVVM_application.ViewModels.DoctorViewModels;
using MVVM_application.ViewModels.UserControlsModel;

using MVVM_application.Models.MainModels;
using MVVM_application.Models;
using MVVM_application.Models.DoctorModels;
using MVVM_application.Models.PatientCardModels;
using MVVM_application.Models.RegisterModels;
using System.Collections.Generic;

namespace MVVM_application.ViewModels
{
    public class MainViewModel : ViewModelBase, IManager
    {

        Clinic _database;
        Receptionist _reception;
        Doctor _doctor;
        Patient _patient;
        List<Patient> _patientList;
        //sprawdza czy zmienil sie widok czy nie
        bool _unchangedView;
        VisitManager _visitManager;
        string[] _visitHours;


        #region ViewModel

        private ViewModelBase _currentViewModel;

        private LoginViewModel _loginViewModel;
        private DailyViewModel _dailyViewModel;
        private RegisterViewModel _registerViewModel;
        private PatientCardViewModel _patientCardViewModel;
        private DoctorViewModel _doctorViewModel;

        private AddNewPatientViewModel _addNewPatientViewModel;
        private EditVisitViewModel _editVisitViewModel;

        private SearchPatientViewModel _searchPatientViewModel;
        private PatientVisitViewModel _patientVisitViewModel;
        private PatientNewVisitViewModel _patientNewVisitViewModel;
        private PatientEditDataViewModel _patientEditDataViewModel;

        private SearchDoctorViewModel _searchDoctorViewModel;
        private DoctorEditDataViewModel _doctorEditDataViewModel;
        private DoctorVisitViewModel _doctorVisitViewModel;
        private AddNewDoctorViewModel _addNewDoctorViewModel;

        private MainUCModel _mainUcModel;
        private RegisterUCModel _registerUCModel;
        private PatientCardUCModel _patientCardUCModel;
        private DoctorUCModel _doctorUCModel;


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
        #endregion
        #endregion

        #region Model
        LoginModel _loginModel;
        DailyModel _dailyModel;
        
        DoctorEditDataModel _doctorEditDataModel;
        DoctorVisitModel _doctorVisitModel;
        SearchDoctorModel _searchDoctorModel;
        AddNewDoctorModel _addNewDoctorModel;

        PatientEditDataModel _patientEditDataModel;
        PatientNewVisitModel _patientNewVisitModel;
        PatientVisitModel _patientVisitModel;
        SearchPatientModel _searchPatientModel;

        AddNewPatientModel _addNewPatientModel;
        EditVisitModel _editVisitModel;
        #endregion

        public MainViewModel()
        {
            InitialiseDatabase();
            InitialiseAllModels();
            InitialiseAllViewModels();
            _unchangedView = true;
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

            _addNewPatientViewModel = new AddNewPatientViewModel(this, _addNewPatientModel);
            _editVisitViewModel = new EditVisitViewModel(this, _editVisitModel);


            _searchPatientViewModel = new SearchPatientViewModel(this, _searchPatientModel);
            _patientVisitViewModel = new PatientVisitViewModel(this, _patientVisitModel);
            _patientNewVisitViewModel = new PatientNewVisitViewModel(this, _patientNewVisitModel);
            _patientEditDataViewModel = new PatientEditDataViewModel(this, _patientEditDataModel);

            _searchDoctorViewModel = new SearchDoctorViewModel(this, _searchDoctorModel);
            _doctorEditDataViewModel = new DoctorEditDataViewModel(this, _doctorEditDataModel);
            _doctorVisitViewModel = new DoctorVisitViewModel(this, _doctorVisitModel);
            _addNewDoctorViewModel = new AddNewDoctorViewModel(this, _addNewDoctorModel);

            _mainUcModel = new MainUCModel(this);
            _registerUCModel = new RegisterUCModel(this);
            _patientCardUCModel = new PatientCardUCModel(this);
            _doctorUCModel = new DoctorUCModel(this);
        }

        public void InitialiseAllModels()
        {
            _loginModel = new LoginModel(this);
            _dailyModel = new DailyModel(this);

            _doctorEditDataModel = new DoctorEditDataModel(this);
            _doctorVisitModel = new DoctorVisitModel(this);
            _searchDoctorModel = new SearchDoctorModel(this);
            _addNewDoctorModel = new AddNewDoctorModel(this);

            _patientEditDataModel = new PatientEditDataModel(this);
            _patientNewVisitModel = new PatientNewVisitModel(this);
            _patientVisitModel = new PatientVisitModel(this);
            _searchPatientModel = new SearchPatientModel(this);

            _addNewPatientModel = new AddNewPatientModel(this);
            _editVisitModel = new EditVisitModel(this);
        }

        public void ChangeView(TypesOfViews view)
        {
            _unchangedView = false;
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
                case TypesOfViews.DoctorEditDataViewModel:
                    return _doctorEditDataViewModel;
                case TypesOfViews.DoctorVisitViewModel:
                    return _doctorVisitViewModel;
                case TypesOfViews.AddNewDoctorViewModel:
                    return _addNewDoctorViewModel;
                default:
                    return null;
            }
        }

        public void RefreshViewModel()
        {
            if (_reception.IDReceptionist != 0)
            {
                InitialiseAllViewModels();
            }
        }

        public void RefreshAll(TypesOfViews view)
        {
            if (_reception.IDReceptionist != 0)
            {
                InitialiseAllModels();
                InitialiseAllViewModels();
                ChangeView(view);
            }
        }

        public void SetUnchangedView(bool unchangedView)
        {
            _unchangedView = unchangedView;
        }

        public bool GetUnchangedView()
        {
            return _unchangedView;
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

        public Patient GetPatient()
        {
            return _patient;
        }

        public void SetPatient(Patient patient)
        {
            _patient = patient;
        }

        public List<Patient> GetPatientList()
        {
            return _patientList;
        }

        public void SetPatientList(List<Patient> patientList)
        {
            _patientList = patientList;
        }

        public VisitManager GetVisitManager()
        {
            return _visitManager;
        }

        public void SetVisitManager(VisitManager visitManager)
        {
            _visitManager = visitManager;
        }

        public void SetDoctorVisitHour(string[] visitHours)
        {
            _visitHours = visitHours;
        }

        public string[] GetDocotrVisitHour()
        {
            return _visitHours;
        }

    }
}