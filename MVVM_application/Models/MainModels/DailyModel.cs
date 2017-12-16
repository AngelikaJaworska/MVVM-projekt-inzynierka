using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_application.Models.Manager;
using MVVM_application.Manager;

namespace MVVM_application.Models.MainModels
{
    public class DailyModel
    {
        private IManager _manager;
        private Clinic _database;

        public DailyModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
        }

        public List<VisitManager> GetAllVisitsWithReceptionist(int idReceptionist)
        {
            var receptionist = _database.Receptionist
                  .SingleOrDefault(x => x.IDReceptionist == idReceptionist);

            if (receptionist == null)
            {
                return null;
            }

            var todayVisitList = receptionist.Visits
                   .Where(x => x.VisitDate.Date == DateTime.Today)
                   .Select(x => CreateNewTodayVisit(x))
                   .ToList();

            return todayVisitList;
        }

        private VisitManager CreateNewTodayVisit(Visits visit)
        {
            var patientInfo = visit.Patient.First_Name.ToString() + " " + visit.Patient.Last_Name.ToString()+" " + visit.Patient.PESEL;
            var specialisation = visit.Doctor.Specialisation.Name.ToString();
            var doctorInfo = visit.Doctor.First_Name.ToString() + " " + visit.Doctor.Last_Name.ToString();
            var timeOfDay = visit.VisitDate.TimeOfDay.ToString();

            return new VisitManager(patientInfo, specialisation, doctorInfo, timeOfDay);
        }
    }
}
