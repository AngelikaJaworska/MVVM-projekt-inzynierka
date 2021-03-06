﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models.Manager
{
    public class VisitManager
    {
        private string _patient;
        private string _specialisation;
        private string _doctor;
        private string _visitDate;

        #region Propertis
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
        public string Specialisation
        {
            get
            {
                return _specialisation;
            }
            set
            {
                _specialisation = value;
            }
        }
        public string Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
            }
        }
        public string VisitDate
        {
            get
            {
                return _visitDate;
            }
            set
            {
                _visitDate = value;
            }
        }
        #endregion

        public VisitManager(string patient, string specialisation, string doctor, string visitDate)
        {
            _patient = patient;
            _specialisation = specialisation;
            _doctor = doctor;
            _visitDate = visitDate;
        }

        public VisitManager(string specialisation, string doctor, string visitDate)
        {
            _specialisation = specialisation;
            _doctor = doctor;
            _visitDate = visitDate;
        }

        public VisitManager(string patient, string visitDate)
        {
            _patient = patient;
            _visitDate = visitDate;
        }
    }
}
