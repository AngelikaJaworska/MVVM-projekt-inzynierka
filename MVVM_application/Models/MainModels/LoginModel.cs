using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MVVM_application.Models.MainModels
{
    class LoginModel
    {
        private Clinic _database;

        private bool _validator { get; set; }
        private string _hashPassword { get; set; }

        private Receptionist reception;

        public LoginModel()
        {
            //_database = database;
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
            if (CalculateMD5Hash(_password))
            {
                reception = _database.Receptionist
               .Select(r => r)
               .Where(l => l.Login.Equals(_login))
               .Single();

                if (_hashPassword.Equals(reception.Password))
                {
                    _validator = true;
                }

                if (_validator)
                {
                    return reception;
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
