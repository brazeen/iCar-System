using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    public class Admin
    {
        private int adminID;

        public int AdminID { get; set; }

        private string adminPassword;

        public string AdminPassword { get; set; }

        private string name;

        public string Name { get; set; }

        private List<User> authenticatedUsersList;

        //constructor
        public Admin() { }

        public Admin(int aid, string apw, string n)
        {
            AdminID = aid;
            AdminPassword = apw;
            Name = n;
            authenticatedUsersList = new List<User>();
        }

    }
}
