using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.PatientCardModels
{
    public class PatientEditDataModel
    {
        private IManager _manager;
        private Clinic _database;
        private Patient _patient;

        public PatientEditDataModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _patient = _manager.GetPatient();
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

        public void SetPatientName(string _name)
        {
            if (_patient != null && _name != null && _name != "")
            {
                _patient.First_Name = _name;
                _database.SaveChanges();
            }
        }

        public void SetPatientSurame(string _surname)
        {
            if (_patient != null && _surname != null && _surname != "")
            {
                _patient.Last_Name = _surname;
                _database.SaveChanges();
            }
        }

        public void SetPatientStreet(string _street)
        {
            if (_patient != null && _street != null && _street != "")
            {
                _patient.Street = _street;
                _database.SaveChanges();
            }
        }

        public void SetPatientHomeNr(string _homeNr)
        {
            if (_patient != null && _homeNr != null && _homeNr != "")
            {
                _patient.HomeNr = _homeNr;
                _database.SaveChanges();
            }
        }

        public void SetPatientCity(string _city)
        {
            if (_patient != null && _city != null && _city != "")
            {
                _patient.City = _city;
                _database.SaveChanges();
            }
        }

        public void SetPatientPhone(string _phone)
        {
            if (_patient != null && _phone != null && _phone != "")
            {
                _patient.Phone = _phone;
                _database.SaveChanges();
            }
        }

        internal void DeletePatient()
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

        public void SetPatientDateOfBirth(string _dateOfBirth)
        {
            if (_patient != null && _dateOfBirth != null && _dateOfBirth != "")
            {
                var date = _dateOfBirth.Split('-');
                var year = Int32.Parse(date[0]);
                var month = Int32.Parse(date[1]);
                var day = Int32.Parse(date[2]);

                DateTime dateOfBirth = new DateTime(year, month, day);
                _patient.DateOfBirth = dateOfBirth;
                _database.SaveChanges();
            }
        }

        public void SetPatientPesel(string _pesel)
        {
            if (_patient != null && _pesel != null && _pesel != "")
            {
                _patient.PESEL = _pesel;
                _database.SaveChanges();
            }
        }
    }
}
