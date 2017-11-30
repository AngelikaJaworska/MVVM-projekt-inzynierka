using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_application.Models.Manager;
using MVVM_application.ViewModels.Manager;

namespace MVVM_application.Models.DoctorModels
{
    public class DoctorDailyVisitModel
    {
        private IViewManager _viewManager;
        private Clinic _database;

        public DoctorDailyVisitModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
            _database = _viewManager.GetDatabase();
        }

        public List<VisitManager> GetAllDailyVisitsWithDoctor(Doctor doctor)
        {
            var _doctor = doctor;
            var doctorDailyVisitsList = _doctor.Visits
                .Where(x => x.VisitDate.Date == DateTime.Today)
                .Select(x => CreateNewDoctorDailyVisit(x))
                .ToList();

            return doctorDailyVisitsList;

        }

        private VisitManager CreateNewDoctorDailyVisit(Visits visit)
        {
            var patientInfo = visit.Patient.First_Name.ToString() + " " + visit.Patient.Last_Name.ToString();
            var timeOfDay = visit.VisitDate.TimeOfDay.ToString();
            //var comments = visit.Comments.Single();
            return new VisitManager(patientInfo, timeOfDay);//, comments);
        }
        
    }
}
