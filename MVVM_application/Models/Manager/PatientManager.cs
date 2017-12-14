using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.Manager
{
    public class PatientManager
    {
        private string _patient;
        private string _pesel;

        public string Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }
        public string Pesel
        {
            get
            {
                return _pesel;
            }
            set
            {
                _pesel = value;
            }
        }

        public PatientManager(string patient, string pesel)
        {
            _patient = patient;
            _pesel = pesel;
        }
    }
}
