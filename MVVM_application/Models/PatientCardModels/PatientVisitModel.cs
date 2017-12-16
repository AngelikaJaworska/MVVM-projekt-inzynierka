using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.PatientCardModels
{
    public class PatientVisitModel
    {
        private IManager _manager;
        private Clinic _database;

        public  PatientVisitModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
        }

        public List<VisitManager> GetAllVisitsWithPatient(Patient patient)
        {
            var _patient = patient;
            var doctorAllVisitsList = _patient.Visits
                //.Where(x => x.VisitDate.Date <= DateTime.Today)
                .Select(x => CreateNewPatientVisit(x))
                .ToList();

            return doctorAllVisitsList;

        }

        private VisitManager CreateNewPatientVisit(Visits visit)
        {
            var specialisation = visit.Doctor.Specialisation.Name.ToString();
            var doctor = visit.Doctor.First_Name.ToString() + " " + visit.Doctor.Last_Name.ToString();
            var visitDate = visit.VisitDate.ToString("yyyy-MM-dd HH:mm");
            return new VisitManager(specialisation, doctor, visitDate);
        }

    }
}
