using MVVM_application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models.WindowDialogModels
{
    public class SearchDoctorWindowDialogModel
    {
        private Clinic _database;
        private IManager _manager;
        private List<string> _specialisationList;
        private List<string> _doctorNameList;

        public SearchDoctorWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _specialisationList = new List<string>();
            _doctorNameList = new List<string>();
        }

        public List<string> FillSpecialisationList()
        {
            var doctorSpecialisationList = _database.Doctor
                .Select(d => d.Specialisation.Name).ToList();

            foreach (string specialisation in doctorSpecialisationList)
            {
                if (!_specialisationList.Contains(specialisation))
                {
                    _specialisationList.Add(specialisation);
                }
            }

            return _specialisationList;
        }

        public List<string> FillDoctorList(string specialisation)
        {
            if (specialisation != null)
            {
                var _doctorList = _database.Doctor
                    .Where(s => s.Specialisation.Name
                    .Equals(specialisation))
                    .ToList();

                foreach(Doctor d in _doctorList)
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

        public Doctor SearchDoctor(string specialisation, string doctorName)
        {
            if(specialisation != null && doctorName != null)
            {
                var _doctorName = doctorName.Split(' ');
                var _doctorFirstName = _doctorName[0];
                var _doctorLastName = _doctorName[1];

                var _doctor = _database.Doctor
                    .Where(d =>
                (d.First_Name.Equals(_doctorFirstName))
                && (d.Last_Name
                .Equals(_doctorLastName)
                && (d.Specialisation.Name
                .Equals(specialisation))))
                .Single();
                return _doctor;
            }
            return null;

        }

    }
}
