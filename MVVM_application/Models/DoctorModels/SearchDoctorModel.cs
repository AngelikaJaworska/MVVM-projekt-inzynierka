using MVVM_application.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.DoctorModels
{
    public class SearchDoctorModel
    {
        private IViewManager _viewManager;
        //private Clinic _database;
        private Doctor _doctor;

        public SearchDoctorModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
            //_database = _viewManager.GetDatabase();
            _doctor = _viewManager.GetDoctor();
        }

        public string GetDoctorName()
        {
            var _name = _doctor.First_Name;
            return _name;
        }

        public string GetDoctorSurame()
        {
            var _surname = _doctor.Last_Name;
            return _surname;
        }

        public string GetDoctorStreet()
        {
            var _street = _doctor.Street;
            return _street;
        }

        public string GetDoctorHomeNr()
        {
            var _homeNr = _doctor.HomeNr;
            return _homeNr;
        }

        public string GetDoctorCity()
        {
            var _city = _doctor.City;
            return _city;
        }

        public string GetDoctorPhone()
        {
            var _phone = _doctor.Phone;
            return _phone;
        }

        public string GetDoctorSpecialisation()
        {
            var _specialisation = _doctor.Specialisation.Name;
            return _specialisation;
        }
    }
}
