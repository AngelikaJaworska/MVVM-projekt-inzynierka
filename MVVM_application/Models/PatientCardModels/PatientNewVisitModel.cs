using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models.PatientCardModels
{
    public class PatientNewVisitModel
    {
        private IManager _manager;
        private Clinic _database;
        
        private Doctor _doctor;

        private List<string> _patientNameList;
        private List<string> _doctorNameList;
        private List<DateTime> _visitDateList;
        private List<string> _specialisationList;

        public PatientNewVisitModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();

            _patientNameList = new List<string>();
            _specialisationList = new List<string>();
            _doctorNameList = new List<string>();
            _visitDateList = new List<DateTime>();
        }
        
        public List<string> FillSpecialisationList()
        {
            var doctorSpecialisationList = _database.Doctor
                .Select(d => d.Specialisation.Name).ToList();

            foreach(string specialisation in doctorSpecialisationList)
            {
                if(!_specialisationList.Contains(specialisation))
                {
                    _specialisationList.Add(specialisation);
                }
            }

            return _specialisationList;
        }

        public List<string> FillDoctorNameList(string specialisationName)
        {
            if (specialisationName != null)
            {
                var _doctorList = _database.Doctor
                    .Where(s => s.Specialisation.Name
                    .Equals(specialisationName))
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

        public List<DateTime> FillVisitDateList(string doctor, string specialisation)
        {
            if(doctor != null && specialisation != null)
            {
                var _doctorName = doctor.Split(' ');
                var _doctorFirstName = _doctorName[0];
                var _doctorLastName = _doctorName[1];

                _doctor = _database.Doctor
                    .Where(d =>
                (d.First_Name.Equals(_doctorFirstName))
                && (d.Last_Name
                .Equals(_doctorLastName)
                && (d.Specialisation.Name
                .Equals(specialisation))))
                .Single();

                foreach (DateTime day in EachDay(DateTime.Today, DateTime.Now.AddDays(4)))
                {
                    _visitDateList.Add(day);
                }
                _visitDateList = CheckingIfPossible(_visitDateList);
             }
            return _visitDateList;
        }

        internal string SetPatientName()
        {
            var patient = _manager.GetPatient().First_Name.ToString() + " " + _manager.GetPatient().Last_Name.ToString() + "," + _manager.GetPatient().PESEL;
            return patient;
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

        public List<string> FillPatientNameList()
        {
            _patientNameList.Clear();

            if (_manager.GetPatient() != null)
            {
                var patient = _manager.GetPatient();
                _patientNameList.Add(patient.First_Name + " " + patient.Last_Name + "," + patient.PESEL);
            }
            else if (_manager.GetPatientList() != null)
            {
                var patientList = _manager.GetPatientList();
                foreach (Patient p in patientList)
                {
                    _patientNameList.Add(p.First_Name + " " + p.Last_Name + "," + p.PESEL);
                }
            }
            return _patientNameList;
        }

        public List<DateTime> CheckingIfPossible(List<DateTime> date)
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
                    if (v.IDPatient == _manager.GetPatient().IDPatient && v.VisitDate.Date == d.Date) //czy pacjent juz w tym dniu jest zapisany na wizyte
                    {
                        possibleVisitsList.Remove(d);
                    }
                }
                foreach (Visits vv in visits)
                {
                    if (vv.VisitDate.TimeOfDay == d.TimeOfDay && vv.VisitDate.Date == d.Date && vv.IDPatient == _manager.GetPatient().IDPatient) //czy pacjent juz do innego lekarza o tej godz
                    {
                        possibleVisitsList.Remove(d);
                    }
                }
            }
            return possibleVisitsList;


        }
        
        internal bool CreateVisitWithData(string nameAndSurname, string specialisationName, 
            string doctorName, string visitDate, string comments)
        {
            if (nameAndSurname != null && nameAndSurname != "" 
                && specialisationName != null && specialisationName != "" 
                && doctorName != null && doctorName != "" 
                && visitDate != null && visitDate != "" )
            {
                var _patientName = nameAndSurname.Split(',');
                var _pesel = _patientName[1];

                var _doctorName = doctorName.Split(' ');
                var _doctorFirstName = _doctorName[0];
                var _doctorLastName = _doctorName[1];

                var patient = _database.Patient
                .Where(p => p.PESEL.Equals(_pesel))
                .Single();

                var doctor = _database.Doctor
                    .Where(d => (d.First_Name.Equals(_doctorFirstName))
                    && (d.Last_Name.Equals(_doctorLastName))
                    && (d.Specialisation.Name.Equals(specialisationName)))
                    .Single();

                Visits _visit = new Visits();
                _visit.IdReceptionist = _manager.GetReceptionist().IDReceptionist;
                _visit.IDPatient = patient.IDPatient;
                _visit.IDDoctor = doctor.IDDoctor;
                _visit.VisitDate = DateTime.Parse(visitDate);
                _visit.Comments = comments;

                _database.Visits.Add(_visit);
                _database.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
