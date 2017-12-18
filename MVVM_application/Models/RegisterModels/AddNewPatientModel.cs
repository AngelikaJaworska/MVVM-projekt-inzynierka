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

        private int _year;
        private int _month;
        private int _day;

        public AddNewPatientModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();

            _year = 0;
            _month = 0;
            _day = 0;
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

                if (CheckIfStringContainsOnlyLetter(name)
                     && CheckIfStringContainsOnlyLetter(surname)
                     && CheckIfStringContainsOnlyLetter(city)
                     && CheckIfStringContainsDate(dateOfBirth)
                     && CheckIfStringContainsPhoneNumber(phone)
                     && CheckIfStringContainsPesel(pesel))
                {
                    Patient _patient = new Patient();
                    _patient.First_Name = name;
                    _patient.Last_Name = surname;
                    _patient.DateOfBirth = new DateTime(_year, _month, _day);
                    _patient.Street = street;
                    _patient.HomeNr = homeNr;
                    _patient.City = city;
                    _patient.Phone = phone;
                    _patient.PESEL = pesel;

                    _database.Patient.Add(_patient);
                    _database.SaveChanges();
                    _manager.SetPatient(_patient);
                    return true;
                }
                else return false;
            }
            else return false;
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
