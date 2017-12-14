using MVVM_application.Manager;
using MVVM_application.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.WindowDialogModels
{
    public class PatientListWindowDialogModel
    {
        private IManager _manager;
        private Clinic _database;
        private List<PatientManager> _patientList;

        public PatientListWindowDialogModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
            _patientList = new List<PatientManager>();
        }

        public  List<PatientManager> GetAllPatient()
        {
            if(_manager.GetPatientList() != null)
            {
                var patientList = _manager.GetPatientList();
                foreach (Patient patient in patientList)
                {
                    _patientList.Add(CreatePatientList(patient));
                }
                return _patientList;
            }
            return null;
        }

        public PatientManager CreatePatientList(Patient patient)
        {
            var patientName = patient.First_Name.ToString() + " " + patient.Last_Name.ToString();
            var pesel = patient.PESEL;
            return new PatientManager(patientName, pesel);
        }

        public void SetPatientFromList(PatientManager patient)
        {
            var _patientToSet = _database.Patient
                .Where(p => p.PESEL.Equals(patient.Pesel))
                .Single();

            _manager.SetPatient(_patientToSet);
        }
    }
}
