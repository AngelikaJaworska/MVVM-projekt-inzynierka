using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.PatientCardModels
{
    public class PatientNewVisitModel
    {
        private IManager _manager;
        private Clinic _database;
        private Visits _visit;
        private Patient _patient;

        public PatientNewVisitModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _patient = _manager.GetPatient();
        }

        public string GetPatientNameAndSurname()
        {
            var nameAndSurname = _patient.First_Name
                .ToString()
                + " "
                + _patient.Last_Name
                .ToString();
            return nameAndSurname;
        }

        public void SetPatientNameAndSurname(string _nameAndSurname)
        {
            if (_patient != null && _nameAndSurname != null && _nameAndSurname != "")
            {
                var patient = _nameAndSurname.Split(' ');
                var name = patient[0];
                var surname = patient[1];

                _patient.First_Name = name;
                _patient.Last_Name = surname;
                _database.SaveChanges();
            }
        }

        public List<string> FillSpecialisationList()
        {
            var specialisationList = _database.Specialisation
                            .Select(s => s.Name)
                            .ToList();

            return specialisationList;
        }
    }
}
