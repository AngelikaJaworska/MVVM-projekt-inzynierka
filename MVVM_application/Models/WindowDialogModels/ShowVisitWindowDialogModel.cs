using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models.WindowDialogModels
{
    public class ShowVisitWindowDialogModel
    {
        private IManager _manager;
        private Clinic _database;
        private Patient _patient;
        private Doctor _doctor;

        public ShowVisitWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
        }

        public bool DeleteVisit()
        {
            var visitToDelete = SearchVisit();
            if (visitToDelete != null)
            {
                if(visitToDelete.VisitDate.Date >= DateTime.Today)
                {
                    _database.Visits.Remove(visitToDelete);
                    _database.SaveChanges();
                    _manager.SetVisitManager(null);
                    return true;
                }
                else
                {
                    MessageBox.Show("Nie można odwołać wizyty z przeszłości");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public bool EditVisit()
        {
            var visit = SearchVisit();

            if(visit != null)
            {
                var patient = visit.Patient;
                var doctor = visit.Doctor;

                _manager.SetPatient(patient);
                _manager.SetDoctor(doctor);

                return true;
            }
            return false;
        }

        public Visits SearchVisit()
        {
            VisitManager _visitManager = _manager.GetVisitManager();

            if(_visitManager != null)
            {
                if(_visitManager.Patient != null)
                {
                    var patientVisitManager = _visitManager.Patient.Split(' ');
                    var patientVisitManagerName = patientVisitManager[0];
                    var patientVisitManagerSurname = patientVisitManager[1];
                    var patientVisitManagerPesel = patientVisitManager[2];

                    _patient = _database.Patient
                         .Where(p => (p.First_Name.Equals(patientVisitManagerName))
                         && (p.Last_Name.Equals(patientVisitManagerSurname))
                         && (p.PESEL.Equals(patientVisitManagerPesel)))
                         .Single();
                }
                else
                {
                    _patient = _manager.GetPatient();
                }
               
                if(_visitManager.Doctor != null)
                {
                    var doctorVisitManager = _visitManager.Doctor.Split(' ');
                    var doctorVisitManagerName = doctorVisitManager[0];
                    var docotrVisitManagerSurname = doctorVisitManager[1];

                    var specialisationVisitManager = _visitManager.Specialisation;

                    _doctor = _database.Doctor
                        .Where(d => (d.First_Name.Equals(doctorVisitManagerName)
                        && (d.Last_Name.Equals(docotrVisitManagerSurname))
                        && (d.Specialisation.Name.Equals(specialisationVisitManager))))
                        .Single();
                }
                else
                {
                    _doctor = _manager.GetDoctor();
                }

                try
                {
                    var dateVisitManager = DateTime.Parse(_visitManager.VisitDate);

                    var visit = _database.Visits
                             .Where(v => (v.Patient.IDPatient == _patient.IDPatient)
                             && (v.Doctor.IDDoctor == _doctor.IDDoctor)
                             && (v.VisitDate == dateVisitManager))
                             .Single();

                    return visit;
                }
                catch
                {
                    return null;
                }
            }
            return null;

        }

        public string SetPatient()
        {
            var visit = SearchVisit();
            if (visit != null)
            {
                var patient = visit.Patient.First_Name + " " + visit.Patient.Last_Name + ", " + visit.Patient.PESEL;
                return patient;
            }
            return null;
        }

        public  string SetDocotr()
        {
            var visit =  SearchVisit();
            if(visit != null)
            {
                var doctor = visit.Doctor.First_Name + " " + visit.Doctor.Last_Name + ", " + visit.Doctor.Specialisation.Name;
                return doctor;
            }
            return null;
        }

        public string SetDate()
        {
            var visit = SearchVisit();
            if (visit != null)
            {
                var date = visit.VisitDate.Date.ToShortDateString() + visit.VisitDate.TimeOfDay.ToString();
                return date;
            }
            return null;
        }
    }
}
