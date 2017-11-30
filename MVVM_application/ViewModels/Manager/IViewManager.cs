using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.ViewModels.Manager
{
    public interface IViewManager
    {
        void ChangeView(TypesOfViews view);
        
        ViewModelBase GetView(TypesOfViews view);

        Clinic GetDatabase();

        Receptionist GetReceptionist();

        void SetReceptionist(Receptionist reception);

        Doctor GetDoctor();

        void SetDoctor(Doctor doctor);

        void RefreshViewModel(TypesOfViews view);
        void RefreshAll(TypesOfViews view);

        void SetUnchangedView(bool unchangedView);
        bool GetUnchangedView();
    }
}