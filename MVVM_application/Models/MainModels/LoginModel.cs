using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MVVM_application.Manager;

namespace MVVM_application.Models.MainModels
{
    public class LoginModel 
    {
        private Clinic _database;
        private IManager _manager;

        private bool _validator { get; set; }
        private string _hashPassword { get; set; }

        private Receptionist _reception;

        public LoginModel(IManager manager)
        {
            _manager = manager;
            _database = _manager.GetDatabase();
        }

        public List<Receptionist> FillReceptionsList()
        {
            var receptionList = _database.Receptionist
                .ToList();

            return receptionList;
        }

        public Receptionist Login(string _login, string _password)
        {
            _validator = false;
            if(_login != null && _password != null)
            {
                if (CalculateMD5Hash(_password))
                {
                    _reception = _database.Receptionist
                   .Select(r => r)
                   .Where(l => l.Login.Equals(_login))
                   .Single();

                    if (_hashPassword.Equals(_reception.Password))
                    {
                        _validator = true;
                    }

                    if (_validator)
                    {
                        return _reception;
                    }
                }
            }

            return null;
        }
        private bool CalculateMD5Hash(string password)
        {
            if (password != null)
            {
                MD5 md5 = MD5.Create();

                byte[] inputBytes = Encoding.ASCII.GetBytes(password);

                byte[] hash = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                _hashPassword = sb.ToString().ToLower();
                return true;
            }
            return false;
        }

    }
}
