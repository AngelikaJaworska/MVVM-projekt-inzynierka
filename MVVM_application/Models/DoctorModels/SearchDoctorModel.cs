using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.DoctorModels
{
    public class SearchDoctorModel
    {
        private IManager _manager;
        private Clinic _database;
        private Doctor _doctor;

        public SearchDoctorModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _doctor = _manager.GetDoctor();
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

        public bool DeleteDoctor()
        {
            try
            {
                var visitsToDelete = _database.Visits
                .Where(d => d.Doctor.IDDoctor == _doctor.IDDoctor)
                .ToList();

                foreach (Visits v in visitsToDelete)
                {
                    _database.Visits.Remove(v);
                }

                _manager.SetDoctor(null);
                _database.Doctor.Remove(_doctor);
                _database.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
