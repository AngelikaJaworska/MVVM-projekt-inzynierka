using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVVM_application.Models.DoctorModels
{
    public class AddNewDoctorModel
    {
        private IManager _manager;
        private Clinic _database;

        string[] _visitHours;
        private int _year;
        private int _month;
        private int _day;
        private TimeSpan _workStart;
        private TimeSpan _workEnd;

        public AddNewDoctorModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();

            _visitHours = new string[2];
            _year = 0;
            _month = 0;
            _day = 0;

            _workStart = new TimeSpan();
            _workEnd = new TimeSpan();
        }

        public List<string> FillSpecialisationList()
        {
            var specialisationList = _database.Specialisation
                .Select(s => s.Name)
                .ToList();
            return specialisationList;
        }
       
        public bool CreateDoctor(string name, string surname, string specialisation, 
            string street, string homeNr, string city, string phone, string dateOfBirth)
        {
            _visitHours = _manager.GetDocotrVisitHour();

            if (CheckData(name, surname, specialisation, street, homeNr, city, phone, dateOfBirth, _visitHours))
            {
                try
                {
                    var specialisationId = _database.Specialisation
                   .Where(s => s.Name.Equals(specialisation))
                   .Select(s => s.IDSpecialisation)
                   .Single();

                    Doctor _doctor = new Doctor();
                    _doctor.First_Name = name;
                    _doctor.Last_Name = surname;
                    _doctor.IDSpecialisation = specialisationId;
                    _doctor.Phone = phone;
                    _doctor.Street = street;
                    _doctor.HomeNr = homeNr;
                    _doctor.City = city;
                    _doctor.DateOfBith = new DateTime(_year, _month, _day);
                    _doctor.WorkStart = _workStart;
                    _doctor.WorkEnd = _workEnd;

                    _database.Doctor.Add(_doctor);
                    _database.SaveChanges();
                    _manager.SetDoctor(_doctor);

                    return true;
                }
                catch
                {
                    return false;
                }
               
            }
            return false;
        }

        public bool CheckData(string name, string surname, string specialisation,
           string street, string homeNr, string city, string phone, string dateOfBirth, string[] visitHours)
        {
            if (name != null && name != ""
               && surname != null && surname != ""
               && dateOfBirth != null && dateOfBirth != ""
               && street != null && street != ""
               && homeNr != null && homeNr != ""
               && city != null && city != ""
               && phone != null && phone != ""
               && specialisation != null && specialisation != ""
               && visitHours != null  && visitHours.Length != 0 && visitHours.Count() > 0)
            {
                if (CheckIfStringContainsOnlyLetter(name) 
                    && CheckIfStringContainsOnlyLetter(surname)
                    && CheckIfStringContainsOnlyLetter(city)
                    && CheckIfStringContainsOnlyLetter(specialisation)
                    && CheckIfStringContainsPhoneNumber(phone)
                    && CheckIfStringContainsDate(dateOfBirth)
                    && CheckIfStringContainsTimeSpan(visitHours))
                {
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

        private bool CheckIfStringContainsDate(string stringToCheck)
        {
            try
            {
                var date = Regex.Split(stringToCheck, @"\W+");// @      special verbatim string syntax
                                                              // \W+    one or more non-word characters together
                _year = Int32.Parse(date[0]);
                _month = Int32.Parse(date[1]);
                _day = Int32.Parse(date[2]);

                if(_year != 0 && _month != 0 && _day != 0)
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

        private bool CheckIfStringContainsTimeSpan(string[] stringToCheck)
        {
            try
            {
                _workStart = TimeSpan.Parse(stringToCheck[0]);
                _workEnd = TimeSpan.Parse(stringToCheck[1]);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
