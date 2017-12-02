using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.PatientCardModels
{
    public class SearchPatientModel
    {
        private IManager _manager;
        private Patient _patient;

        public SearchPatientModel(IManager manager)
        {
           _manager = manager;
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
    }
}
