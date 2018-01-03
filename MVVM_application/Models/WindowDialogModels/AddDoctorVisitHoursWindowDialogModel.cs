using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models.WindowDialogModels
{
    public class AddDoctorVisitHoursWindowDialogModel
    {
        private IManager _manager;
        private Clinic _database;
        private List<string> _startHoursList;
        private List<string> _endHoursList;

        public AddDoctorVisitHoursWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _startHoursList = new List<string>();
            _endHoursList = new List<string>();
        }

        public  List<string> FillEndHoursList(string startHour)
        {
            if (startHour != null)
            {
                var start = DateTime.Parse(startHour).Hour;
                IEnumerable<string> end = null;

                for (int j = 0; j <= 18 - start; j++) //przychodnia czynna do 18
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
                // MessageBox.Show("Proszę wybrać godzinę rozpoczęcia");
                return null;
            }

            return _endHoursList;
        }

        public List<string> SetStartHourList()
        {
            var startHours = Enumerable.Range(09, 09).Select(i => (DateTime.MinValue.AddHours(i)).ToString("HH:mm tt"));
            foreach (string hour in startHours)
            {
                _startHoursList.Add(hour);
            }
            return _startHoursList;
        }

        public bool SaveData(string startHour, string endHour)
        {
            if(CheckData(startHour, endHour))
            {
                var visitHours = new string[] { startHour, endHour };
                _manager.SetDoctorVisitHour(visitHours);
                return true;
            }
            return false;
            
        }

        public bool CheckData(string startHour, string endHour)
        {
            if (startHour != null && startHour != " " && endHour != null && endHour != " ")
            {
                return true;
            }
            return false;
        }

    }
}
