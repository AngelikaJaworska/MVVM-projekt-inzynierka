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
        private List<string> _patientNameList;

        public SearchPatientWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _patientNameList = new List<string>();
        }

        internal List<string> FillPeselList()
        {
            var peselList = _database.Patient
                .Select(p => p.PESEL)
                .ToList();

            return peselList;
        }

        public List<string> FillPatientList(string pesel)
        {
            if (pesel != null)
            {
                var _patientList = _database.Patient
                    .Where(p => p.PESEL
                    .Equals(pesel))
                    .ToList();

                foreach (Patient p in _patientList)
                {
                    _patientNameList.Add(p.First_Name + " " + p.Last_Name);
                }

            }
            else
            {
                MessageBox.Show("Proszę wybrać pesel");
            }

            return _patientNameList;
        }

        public Patient SearchPatient(string pesel, string patientName)
        {
            if (pesel != null && patientName != null)
            {
                var _patientName = patientName.Split(' ');
                var _patientFirstName = _patientName[0];
                var _patientLastName = _patientName[1];

                var _patient = _database.Patient
                    .Where(p =>
                (p.First_Name.Equals(_patientFirstName))
                && (p.Last_Name
                .Equals(_patientLastName)
                && (p.PESEL
                .Equals(pesel))))
                .Single();
                return _patient;
            }
            return null;

        }
    }
}
