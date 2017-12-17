using MVVM_application.Manager;
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

        public AddNewDoctorModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
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
            if (name != null && name != ""
               && surname != null && surname != ""
               && dateOfBirth != null && dateOfBirth != ""
               && street != null && street != ""
               && homeNr != null && homeNr != ""
               && city != null && city != ""
               && phone != null && phone != ""
               && specialisation != null && specialisation != "")
            {
                var date = Regex.Split(dateOfBirth, @"\W+");// @      special verbatim string syntax
                                                            // \W+    one or more non-word characters together

                var year = Int32.Parse(date[0]);
                var month = Int32.Parse(date[1]);
                var day = Int32.Parse(date[2]);

                Doctor _doctor = new Doctor();
                _doctor.First_Name = name;
                _doctor.Last_Name = surname;
                _doctor.Specialisation.Name = specialisation;
                _doctor.Phone = phone;
                _doctor.Street = street;
                _doctor.HomeNr = homeNr;
                _doctor.City = city;
                _doctor.DateOfBith = new DateTime(year, month, day);

                _database.Doctor.Add(_doctor);
                _database.SaveChanges();
                _manager.SetDoctor(_doctor);

                return true;
            }
            return false;
        }
    }
}
