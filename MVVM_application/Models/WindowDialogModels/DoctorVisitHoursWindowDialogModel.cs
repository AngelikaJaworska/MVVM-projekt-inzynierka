using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVM_application.Models.WindowDialogModels
{
    public class DoctorVisitHoursWindowDialogModel
    {
        private IManager _manager;
        private Clinic _database;
        private Doctor _doctor;

        public DoctorVisitHoursWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _doctor = _manager.GetDoctor();
        }

        public string SetStartHour()
        {
            var startHour = _database.Doctor
                .Where(d => d.IDDoctor == _doctor.IDDoctor)
                .Select(s => s.WorkStart)
                .Single();
            return startHour.ToString();
        }

        public string SetEndHour()
        {
            var endHour = _database.Doctor
            .Where(d => d.IDDoctor == _doctor.IDDoctor)
            .Select(s => s.WorkEnd)
            .Single();
            return endHour.ToString();
        }
        
    }
}
