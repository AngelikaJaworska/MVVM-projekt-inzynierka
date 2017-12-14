using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.DoctorModels
{
    public class DoctorEditDataModel
    {
        private IManager _manager;
        private Clinic _database;
        private Doctor _doctor;

        public DoctorEditDataModel(IManager manager)
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

        public List<string> FillSpecialisationList()
        {
            var specialisationList = _database.Specialisation
                            .Select(s => s.Name)
                            .ToList();

            return specialisationList;
        }

        internal void SetSpecialisation(string _specialisation)
        {
            if(_specialisation != null && _specialisation != "")
            {
                _doctor.Specialisation.Name = _specialisation;
                _database.SaveChanges();
            }
        }

        internal void SetDoctorName(string _name)
        {
            if(_doctor != null && _name != null && _name != "")
            {
                _doctor.First_Name = _name;
                _database.SaveChanges();
            }
        }

        internal void SetDoctorSurame(string _surname)
        {
            if (_doctor != null && _surname != null && _surname != "")
            {
                _doctor.Last_Name = _surname;
                _database.SaveChanges();
            }
        }

        internal void SetDoctorStreet(string _street)
        {
            if (_doctor != null && _street != null && _street != "")
            {
                _doctor.Street = _street;
                _database.SaveChanges();
            }
        }

        internal void SetDoctorHomeNr(string _homeNr)
        {
            if (_doctor != null && _homeNr != null && _homeNr != "")
            {
                _doctor.HomeNr = _homeNr;
                _database.SaveChanges();
            }
        }

        internal void SetDoctorCity(string _city)
        {
            if (_doctor != null && _city != null && _city != "")
            {
                _doctor.City = _city;
                _database.SaveChanges();
            }
        }

        internal void SetDoctorPhone(string _phone)
        {
            if (_doctor != null && _phone != null && _phone != "")
            {
                _doctor.Phone = _phone;
                _database.SaveChanges();
            }
        }

        internal void DeleteDoctor()
        {
            if(_doctor != null)
            {
                var deleteVisits = _database.Visits
                    .Where(d => d.IDDoctor == _doctor.IDDoctor)
                    .ToList();

                foreach(Visits v in deleteVisits)
                {
                    _database.Visits.Remove(v);
                }

                _manager.SetDoctor(null);
                _database.Doctor.Remove(_doctor);
                _database.SaveChanges();
            }
        }
    }
}
