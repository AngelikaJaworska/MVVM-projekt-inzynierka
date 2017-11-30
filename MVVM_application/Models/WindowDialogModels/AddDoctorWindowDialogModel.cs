using MVVM_application.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_application.Models.WindowDialogModels
{
    public class AddDoctorWindowDialogModel
    {
        private Clinic _database;
        private IViewManager _viewManager;
        private List<string> _doctorNameList;

        public AddDoctorWindowDialogModel(IViewManager viewManager)
        {
            _viewManager = viewManager;
            _database = _viewManager.GetDatabase();
            _doctorNameList = new List<string>();
        }

        public List<string> FillSpecialisationList()
        {
            var specialisationList = _database.Specialisation
                .Select(s => s.Name)
                .ToList();

            return specialisationList;
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

    }
}
