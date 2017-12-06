using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVVM_application.Models.RegisterModels
{
    public class AddNewPatientModel
    {
        private IManager _manager;
        private Clinic _database;

        public AddNewPatientModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
        }

        public bool CreatePatientCard(string name, string surname, 
            string dateOfBirth, string street, string homeNr, string city, string phone, string pesel)
        {
            if (name != null && name != ""
               && surname != null && surname != ""
               && dateOfBirth != null && dateOfBirth != ""
               && street != null && street != ""
               && homeNr != null && homeNr != ""
               && city != null && city != ""
               && phone != null && phone != ""
               && pesel != null && pesel != "")
            {

                var date = Regex.Split(dateOfBirth, @"\W+");// @      special verbatim string syntax
                                                            // \W+    one or more non-word characters together

                var year = Int32.Parse(date[0]);
                var month = Int32.Parse(date[1]);
                var day = Int32.Parse(date[2]);

                Patient _patient = new Patient();
                _patient.First_Name = name;
                _patient.Last_Name = surname;
                _patient.DateOfBirth = new DateTime(year, month, day);
                _patient.Street = street;
                _patient.HomeNr = homeNr;
                _patient.City = city;
                _patient.Phone = phone;
                _patient.PESEL = pesel;

                _database.Patient.Add(_patient);
                _database.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
