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

        public List<VisitManager> GetAllVisitsWithPatient(Patient patient, DateTime date)
        {
            var _patient = patient;
            var doctorAllVisitsList = _patient.Visits
                .Where(x => x.VisitDate.Date == date)
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

        public string SetPatientInfo()
        {
            var patientInfo = "Podgląd wizyt pacjenta: " + _manager.GetPatient().First_Name + " " 
                + _manager.GetPatient().Last_Name + ", " + _manager.GetPatient().PESEL;
            return patientInfo;

        }

        public List<DateTime> GetDate()
        {
            var dateList = new List<DateTime>();

            foreach (DateTime day in EachDay(DateTime.Today, DateTime.Now.AddMonths(1)))
            {
                dateList.Add(day);
            }

            return dateList;
        }

        public IEnumerable<DateTime> EachDay(DateTime start, DateTime end)
        {
            for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday && day.DayOfYear != 24.12 && day.DayOfYear != 25.12 && day.DayOfYear != 26.12)
                {
                    yield return day;
                }
            }
        }
    }
}
