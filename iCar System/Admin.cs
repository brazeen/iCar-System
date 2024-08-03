using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Admin
    {
        private int adminID;

        public int AdminID { get { return adminID; } set { adminID = value; } }

        private string adminPassword;

        public string AdminPassword { get { return adminPassword; } set { adminPassword = value; } }

        private string name;

        public string Name { get { return name; } set { name = value; } }

        private List<User> authenticatedUsersList;

        public List<User> AuthenticatedUsersList { get { return authenticatedUsersList; } set { authenticatedUsersList = value; } }

        //constructor
        public Admin() { }

        public Admin(int aid, string apw, string n)
        {
            AdminID = aid;
            AdminPassword = apw;
            Name = n;
            AuthenticatedUsersList = new List<User>();
        }

    }
}
