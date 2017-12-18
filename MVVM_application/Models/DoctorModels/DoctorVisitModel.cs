using MVVM_application.Models.Manager;
using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.DoctorModels
{
    public class DoctorVisitModel
    {
        private IManager _manager;
        private Clinic _database;

        public DoctorVisitModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
        }

        public List<VisitManager> GetAllVisitsWithDoctor(Doctor doctor, DateTime date)
        {
            var _doctor = doctor;
            if(_doctor != null)
            {
                var doctorAllVisitsList = _doctor.Visits
                    .Where(x => x.VisitDate.Date == date)
                    .Select(x => CreateNewDoctorVisit(x))
                    .ToList();

                return doctorAllVisitsList;
            }
            return null;

        }

        private VisitManager CreateNewDoctorVisit(Visits visit)
        {
            if(visit != null)
            {
                var patientInfo = visit.Patient.First_Name.ToString() + " " + visit.Patient.Last_Name.ToString() + " " + visit.Patient.PESEL;
                var timeOfDay = visit.VisitDate.ToString("yyyy-MM-dd HH:mm");
                return new VisitManager(patientInfo, timeOfDay);
            }
            return null;
           
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

        public string SetDoctorInfo()
        {
            var doctor = _manager.GetDoctor();
            var specialisationName = _database.Specialisation
                .Where(s => s.IDSpecialisation == doctor.IDSpecialisation)
                .Select(s => s.Name)
                .Single();

            var doctorInfo = "Podgląd wizyt lekarza: " + _manager.GetDoctor().First_Name + " " + _manager.GetDoctor().Last_Name 
                + ", " + specialisationName;

            return doctorInfo;
        }
    }
}
