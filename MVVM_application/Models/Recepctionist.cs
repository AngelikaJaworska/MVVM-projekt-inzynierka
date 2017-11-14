using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models
{
    class Recepctionist: User
    {
        private string login;
        private string password;

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                password = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public Recepctionist(string name, string surname, string login, string password) :base(name,surname)
        {
            this.login = login;
            this.password = password;
        }


    }
}
