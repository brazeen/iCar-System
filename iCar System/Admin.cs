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

        public int AdminID { get { return adminID; } set { adminID = value; } }

        private string adminPassword;

        public string AdminPassword { get { return adminPassword; } set { adminPassword = value; } }

        private string name;

        public string Name { get { return name; } set { name = value; } }

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
