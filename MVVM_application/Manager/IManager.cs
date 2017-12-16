using GalaSoft.MvvmLight;
using MVVM_application.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Manager
{
    public interface IManager
    {
        void ChangeView(TypesOfViews view);
        
        ViewModelBase GetView(TypesOfViews view);

        Clinic GetDatabase();

        Receptionist GetReceptionist();
        void SetReceptionist(Receptionist reception);

        Doctor GetDoctor();
        void SetDoctor(Doctor doctor);

        Patient GetPatient();
        void SetPatient(Patient patient);

        List<Patient> GetPatientList();
        void SetPatientList(List<Patient> patientList);

        void RefreshViewModel();
        void RefreshAll(TypesOfViews view);

        void SetUnchangedView(bool unchangedView);
        bool GetUnchangedView();

        VisitManager GetVisitManager();
        void SetVisitManager(VisitManager visitManager);
    }
}