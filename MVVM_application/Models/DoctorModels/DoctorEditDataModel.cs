using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool SetSpecialisation(string _specialisation)
        {
            if(_specialisation != null && _specialisation != "")
            {
                if(CheckIfStringContainsOnlyLetter(_specialisation))
                {
                    var specialisationId = _database.Specialisation
                        .Where(s => s.Name.Equals(_specialisation))
                        .Select(s => s.IDSpecialisation)
                        .Single();

                    _doctor.IDSpecialisation = specialisationId;
                    _database.SaveChanges();
                    return true;
                }
                else return false;
            }
            else 
            {
                var specialisationName = _database.Specialisation
                    .Where(s => s.IDSpecialisation == _doctor.IDSpecialisation)
                    .Select(s => s.Name)
                    .Single();

                _specialisation = specialisationName;
                _database.SaveChanges();
                return true;
            }
        }

        public bool SetDoctorName(string _name)
        {
            if(_doctor != null && _name != null && _name != "")
            {
                if (CheckIfStringContainsOnlyLetter(_name))
                {
                    _doctor.First_Name = _name;
                    _database.SaveChanges();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public bool SetDoctorSurame(string _surname)
        {
            if (_doctor != null && _surname != null && _surname != "")
            {
                if (CheckIfStringContainsOnlyLetter(_surname))
                {
                    _doctor.Last_Name = _surname;
                    _database.SaveChanges();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public bool SetDoctorStreet(string _street)
        {
            if (_doctor != null && _street != null && _street != "")
            {
                _doctor.Street = _street;
                _database.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetDoctorHomeNr(string _homeNr)
        {
            if (_doctor != null && _homeNr != null && _homeNr != "")
            {
                _doctor.HomeNr = _homeNr;
                _database.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetDoctorCity(string _city)
        {
            if (_doctor != null && _city != null && _city != "")
            {
                if (CheckIfStringContainsOnlyLetter(_city))
                {
                    _doctor.City = _city;
                    _database.SaveChanges();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public bool SetDoctorPhone(string _phone)
        {
            if (_doctor != null && _phone != null && _phone != "")
            {
                if(CheckIfStringContainsPhoneNumber(_phone))
                {
                    _doctor.Phone = _phone;
                    _database.SaveChanges();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public void DeleteDoctor()
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

        private bool CheckIfStringContainsOnlyLetter(string stringToCheck)
        {
            Match match = Regex.Match(stringToCheck, @"[\p{L} ]+$");
            return match.Success;
        }

        private bool CheckIfStringContainsPhoneNumber(string stringToCheck)
        {
            Match match = Regex.Match(stringToCheck, @"\d{9}");
            return match.Success;
        }

       
    }
}
