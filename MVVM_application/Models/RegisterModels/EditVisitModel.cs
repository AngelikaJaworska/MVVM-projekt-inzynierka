using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.RegisterModels
{
    public class EditVisitModel
    {
        private IManager _manager;
        private Clinic _database;
        private Patient _patient;
        private Doctor _doctor;
        private List<DateTime> _visitList;

        public EditVisitModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _patient = _manager.GetPatient();
            _doctor = _manager.GetDoctor();
            _visitList = new List<DateTime>();
        }

        public string SetPatient()
        {
            var nameAndSurname = _patient
                .First_Name
                .ToString()
                + " "
                + _patient.Last_Name
                .ToString();
            return nameAndSurname;
        }

        public string SetDoctor()
        {
            var nameAndSurname = _doctor
              .First_Name
              .ToString()
              + " "
              + _doctor.Last_Name
              .ToString();
            return nameAndSurname;
        }

        public List<DateTime> FillVisitList()
        {
            var visitList = _database.Visits
                    .Where(v => (v.IDDoctor == _doctor.IDDoctor)
                    && (v.IDPatient == _patient.IDPatient))
                    .Select(d => d.VisitDate)
                    .ToList();
            return visitList;
        }

        public List<DateTime> FillNewVisitDateList(DateTime visit)
        {
            foreach (DateTime day in EachDay(DateTime.Now, DateTime.Now.AddDays(4)))
            {
                _visitList.Add(day);
            }

            _visitList = CheckingIfPossible(_visitList, visit);
            return _visitList;
        }

        public bool DeleteVisit(DateTime _visitDate)
        {
            var date = DateTime.Parse("0001-01-01 00:00:00");
            if(_visitDate != date)
            {
                var visitList = _database.Visits.Where(v => (v.IDDoctor == _doctor.IDDoctor)
                && (v.IDPatient == _patient.IDPatient)).ToList();
                foreach (Visits v in visitList)
                {
                    if (v.VisitDate.Date.Day.Equals(_visitDate.Day))
                    {
                        if (v.VisitDate.TimeOfDay.Equals(_visitDate.TimeOfDay))
                        {
                            _database.Visits.Remove(v);
                            _database.SaveChanges();
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
            }
            return false;
        }

        public bool ChangeVisitDate(DateTime _visitDate, DateTime _newVisitDate)
        {
            var date = DateTime.Parse("0001-01-01 00:00:00");
            if (_newVisitDate != date && _visitDate != date)
            {
                var visitToChange = _database.Visits
                    .Where(v => (v.VisitDate == _visitDate)
                    && (v.IDDoctor == _doctor.IDDoctor)
                    && (v.IDPatient == _patient.IDPatient))
                    .Single();

                visitToChange.VisitDate = _newVisitDate;
                visitToChange.IdReceptionist = _manager.GetReceptionist().IDReceptionist;
                _database.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<DateTime> EachDay(DateTime start, DateTime end)
        {
            for (var day = start.Date; day.Date <= end.Date; day = day.AddMinutes(30))
            {
                if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday && day.DayOfYear != 24.12 && day.DayOfYear != 25.12 && day.DayOfYear != 26.12)
                {
                    yield return day;
                }
            }
        }
        public List<DateTime> CheckingIfPossible(List<DateTime> date, DateTime visit)
        {
            Doctor doctor = _database.Doctor.Select(s => s).Where(i => i.IDDoctor == _doctor.IDDoctor).Single();
            TimeSpan startTime = doctor.WorkStart;
            TimeSpan endTime = doctor.WorkEnd;
            List<Visits> thisDoctorVisit = _database.Visits.Select(s => s).Where(i => i.IDDoctor == _doctor.IDDoctor).ToList();
            List<Visits> visits = _database.Visits.Select(s => s).ToList();

            List<DateTime> possibleVisitsList = new List<DateTime>();

            foreach (DateTime dt in date)
            {
                possibleVisitsList.Add(dt);
            }

            foreach (DateTime d in date)
            {
                if (d.TimeOfDay < startTime || d.TimeOfDay >= endTime)  //czy w godzinach pracy lekarza
                {
                    possibleVisitsList.Remove(d);
                }

                if (DateTime.Now.TimeOfDay > d.TimeOfDay && DateTime.Now.Date == d.Date) //czy dana godz juz minela
                {
                    possibleVisitsList.Remove(d);
                }
                foreach (Visits v in thisDoctorVisit)
                {
                    if (v.VisitDate.TimeOfDay == d.TimeOfDay && v.VisitDate.Date == d.Date) //czy taka wizyta jest juz zajeta
                    {
                        possibleVisitsList.Remove(d);
                    }
                    if (v.VisitDate.Date == visit.Date && v.IDPatient == _patient.IDPatient) //czy w dniu dla ktorego edytujemy wizyte
                    {
                        possibleVisitsList.Remove(d);
                    }

                }
                foreach (Visits vv in visits)
                {
                    if (vv.VisitDate.TimeOfDay == d.TimeOfDay && vv.VisitDate.Date == d.Date && vv.IDPatient == _patient.IDPatient) //czy pacjent juz do innego lekarza o tej godz
                    {
                        possibleVisitsList.Remove(d);
                    }
                 }
            }
            return possibleVisitsList;
        }
     }
}
