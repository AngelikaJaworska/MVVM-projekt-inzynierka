using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models
{
    public class SearchVisitToEditWindowDialogModel
    {
        private IManager _manager;
        private Clinic _database;
        private List<string> _doctorNameList;

        public SearchVisitToEditWindowDialogModel(IManager manager)
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

        public string SetPatient()
        {
            var _patient = _manager.GetPatient();
            var _patientName = _patient.First_Name + " " + _patient.Last_Name;
            return _patientName;
        }

        internal List<string> FillDoctorList(string specialisation)
        {
            if (specialisation != null)
            {
                var _doctorList = _database.Doctor
                    .Where(s => s.Specialisation.Name
                    .Equals(specialisation))
                    .ToList();

                foreach (Doctor d in _doctorList)
                {
                    _doctorNameList.Add(d.First_Name + " " + d.Last_Name);
                }

            }
            else
            {
                MessageBox.Show("Proszę wybrać specjalność");
            }

            return _doctorNameList;
        }

        internal Doctor SearchDoctor(string specialisation, string doctor)
        {
            if (specialisation != null && doctor != null)
            {
                var _doctorName = doctor.Split(' ');
                var _doctorFirstName = _doctorName[0];
                var _doctorLastName = _doctorName[1];

                var _doctor = _database.Doctor
                    .Where(d =>
                (d.First_Name.Equals(_doctorFirstName))
                && (d.Last_Name
                .Equals(_doctorLastName)
                && (d.Specialisation.Name
                .Equals(specialisation))))
                .Single();
                return _doctor;
            }
            return null;
        }
    }
}
