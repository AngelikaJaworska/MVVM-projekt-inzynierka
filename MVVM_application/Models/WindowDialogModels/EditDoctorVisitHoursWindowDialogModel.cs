using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models.WindowDialogModels
{
    public class EditDoctorVisitHoursWindowDialogModel
    {
        private IManager _manager;
        private Clinic _database;
        private Doctor _doctor;
        private List<string> _startHoursList;
        private List<string> _endHoursList;

        public EditDoctorVisitHoursWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _doctor = _manager.GetDoctor();
            _startHoursList = new List<string>();
            _endHoursList = new List<string>();
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

        internal bool SaveData(string startHour, string endHour)
        {
            if (startHour != null && startHour != ""
                && endHour != null && endHour != "")
            {
                _doctor.WorkStart = TimeSpan.Parse(startHour);
                _doctor.WorkEnd = TimeSpan.Parse(endHour);
                _database.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal List<string> FillEndHoursList(string startHour)
        {
            if (startHour != null)
            {
                var start = DateTime.Parse(startHour).Hour;
                IEnumerable<string> end = null;

                for (int j = 0; j <= 18 - start; j++)
                {
                    end = Enumerable.Range(start + 1, 0 + j).Select(i => (DateTime.MinValue.AddHours(i)).ToString("HH:mm tt"));
                }
                foreach (string hour in end)
                {
                    _endHoursList.Add(hour);
                }
            }
            else
            {
                MessageBox.Show("Proszę wybrać godzinę rozpoczęcia");
            }

            return _endHoursList;
        }

        internal List<string> SetStartHourList()
        {
            var startHours = Enumerable.Range(09, 09).Select(i => (DateTime.MinValue.AddHours(i)).ToString("HH:mm tt"));
            foreach (string hour in startHours)
            {
                _startHoursList.Add(hour);
            }
            return _startHoursList;
        }
    }
}
