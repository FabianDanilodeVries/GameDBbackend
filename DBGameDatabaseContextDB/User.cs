using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBGameDatabaseContextDB
{
    public class User
    {
        public int Id { get; }
        public string Name { get; set; }
        private string _Password; //Accessed via property
        public string Email { get; set; }
        public DateTime dateOfBirth { get; set; }


        public string Password //Password property
        {
            get 
            {
                return Password;
            }
            set
            {
                Password = value;
            }
        }
    }
}
