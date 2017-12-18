using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVVM_application.Models.PatientCardModels
{
    public class PatientEditDataModel
    {
        private IManager _manager;
        private Clinic _database;
        private Patient _patient;

        private int _year;
        private int _month;
        private int _day;

        public PatientEditDataModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _patient = _manager.GetPatient();

            _year = 0;
            _month = 0;
            _day = 0;
        }

        public string GetPatientName()
        {
            var name = _patient.First_Name
                .ToString();
            return name;
        }

        public string GetPatientSurame()
        {
            var surname = _patient.Last_Name
               .ToString();
            return surname;
        }

        public string GetPatientStreet()
        {
            var street = _patient.Street
               .ToString();
            return street;
        }

        public string GetPatientCity()
        {
            var city = _patient.City
               .ToString();
            return city;
        }

        public string GetPatientPhone()
        {
            var phone = _patient.Phone
                .ToString();
            return phone;
        }

        public string GetPatientHomeNr()
        {
            var homeNr = _patient.HomeNr
               .ToString();
            return homeNr;
        }

        public string GetPatientDateOfBirth()
        {
            var dateOfBirth = _patient.DateOfBirth
               .ToString("yyyy-MM-dd");
            return dateOfBirth;
        }

        public string GetPatientPesel()
        {
            var pesel = _patient.PESEL
               .ToString();
            return pesel;
        }

        public bool SetPatientName(string _name)
        {
            if (_patient != null && _name != null && _name != "")
            {
                if (CheckIfStringContainsOnlyLetter(_name))
                {
                    _patient.First_Name = _name;
                    _database.SaveChanges();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public bool SetPatientSurame(string _surname)
        {
            if (_patient != null && _surname != null && _surname != "")
            {
                if(CheckIfStringContainsOnlyLetter(_surname))
                {
                    _patient.Last_Name = _surname;
                    _database.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool SetPatientStreet(string _street)
        {
            if (_patient != null && _street != null && _street != "")
            {
                _patient.Street = _street;
                _database.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetPatientHomeNr(string _homeNr)
        {
            if (_patient != null && _homeNr != null && _homeNr != "")
            {
                _patient.HomeNr = _homeNr;
                _database.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetPatientCity(string _city)
        {
            if (_patient != null && _city != null && _city != "")
            {
                if(CheckIfStringContainsOnlyLetter(_city))
                {
                    _patient.City = _city;
                    _database.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool SetPatientPhone(string _phone)
        {
            if (_patient != null && _phone != null && _phone != "")
            {
                if(CheckIfStringContainsPhoneNumber(_phone))
                {
                    _patient.Phone = _phone;
                    _database.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

        public void DeletePatient()
        {
            if(_patient != null)
            {
              var deleteVisits = _database.Visits
                    .Where(p => p.IDPatient == _patient.IDPatient)
                    .ToList();
                foreach(Visits v in deleteVisits)
                {
                    _database.Visits.Remove(v);
                }

                _manager.SetPatient(null);
                _database.Patient.Remove(_patient);
                _database.SaveChanges();
            }
        }

        public bool SetPatientDateOfBirth(string _dateOfBirth)
        {
            if (_patient != null && _dateOfBirth != null && _dateOfBirth != "")
            {
                if(CheckIfStringContainsDate(_dateOfBirth))
                {
                    _patient.DateOfBirth = new DateTime(_year,_month, _day);
                    _database.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool SetPatientPesel(string _pesel)
        {
            if (_patient != null && _pesel != null && _pesel != "")
            {
                if(CheckIfStringContainsPesel(_pesel))
                {
                    _patient.PESEL = _pesel;
                    _database.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
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

        private bool CheckIfStringContainsPesel(string stringToCheck)
        {
            Match match = Regex.Match(stringToCheck, @"\d{11}");
            return match.Success;
        }

        private bool CheckIfStringContainsDate(string stringToCheck)
        {
            try
            {
                var date = Regex.Split(stringToCheck, @"\W+");// @      special verbatim string syntax
                                                              // \W+    one or more non-word characters together
                _year = Int32.Parse(date[0]);
                _month = Int32.Parse(date[1]);
                _day = Int32.Parse(date[2]);

                if (_year != 0 && _month != 0 && _day != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
