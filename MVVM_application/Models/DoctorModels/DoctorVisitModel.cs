﻿using MVVM_application.Models.Manager;
using MVVM_application.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.DoctorModels
{
    public class DoctorVisitModel
    {
        private IViewManager _viewManager;
        private Clinic _database;

        public DoctorVisitModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
            _database = _viewManager.GetDatabase();
        }

        public List<VisitManager> GetAllVisitsWithDoctor(Doctor doctor)
        {
            var _doctor = doctor;
            var doctorAllVisitsList = _doctor.Visits
                .Where(x => x.VisitDate.Date <= DateTime.Today)
                .Select(x => CreateNewDoctorVisit(x))
                .ToList();

            return doctorAllVisitsList;

        }

        private VisitManager CreateNewDoctorVisit(Visits visit)
        {
            var patientInfo = visit.Patient.First_Name.ToString() + " " + visit.Patient.Last_Name.ToString();
            var timeOfDay = visit.VisitDate.ToString("yyyy-MM-dd HH:mm");
            //var comments = visit.Comments.Single();
            return new VisitManager(patientInfo, timeOfDay);//, comments);
        }
    }
}
