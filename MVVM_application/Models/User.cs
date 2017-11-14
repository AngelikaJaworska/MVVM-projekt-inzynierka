using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_application.Models
{
    class User
    {
        private string name;
        private string surname;
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }

        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }

        }
       
        public User(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }
    }
}
