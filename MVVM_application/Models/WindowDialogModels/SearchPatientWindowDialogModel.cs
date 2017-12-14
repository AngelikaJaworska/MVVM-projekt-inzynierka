using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models.WindowDialogModels
{
    public class SearchPatientWindowDialogModel
    {
        private Clinic _database;
        private IManager _manager;

        public SearchPatientWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
        }

        public Patient SearchPatient(string pesel, string patientSurname)
        {
            if (pesel != null && pesel != ""
                && patientSurname != null && patientSurname != "")
            {
                try
                {
                    var _patient = _database.Patient
                         .Where(p =>
                     (p.Last_Name
                     .Equals(patientSurname)
                     && (p.PESEL
                     .Equals(pesel))))
                     .Single();
                    return _patient;
                }
                catch
                {
                    MessageBox.Show("Szukany pacjent nie istnieje");
                }
            }
            return null;

        }

        public Patient SearchPatientByPesel(string pesel)
        {
            if (pesel != null && pesel != "")
            {
                try
                {
                    var _patient = _database.Patient
                         .Where(p => p.PESEL.Equals(pesel))
                     .Single();
                    return _patient;
                }
                catch
                {
                    MessageBox.Show("Szukany pacjent z wpisanym numerem pesel nie istnieje");
                }
            }
            return null;
        }

        internal List<Patient> SearchPatientBySurname(string patientSurname)
        {
            if(patientSurname != null && patientSurname != "")
            {
                var _patientList = _database.Patient
                  .Where(p => p.Last_Name.Contains(patientSurname))
                  .ToList();
                return _patientList;
               
            }
            return null;
        }
    }
}
