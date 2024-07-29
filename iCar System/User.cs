using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    abstract class User
    {
        private int userID;

        public int UserID { get; set; }

        private string name;

        public string Name { get; set; }

        private string contactNumber;

        public string ContactNumber { get; set; }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth { get; set; }

        private string emailAddress;

        public string EmailAddress { get; set; }

        private string homeAddress;

        public string HomeAddress { get; set; }

        private Admin authenticatedAdmin;

        public Admin AuthenticatedAdmin { get; set; }

        //constructor
        public User() { }

        public User(int uid, string n, string cn, DateTime dob, string ea, string ha, Admin aa)
        {
            UserID = uid;
            Name = n;
            ContactNumber = cn;
            DateOfBirth = dob;
            EmailAddress = ea;
            HomeAddress = ha;
            AuthenticatedAdmin = aa;
        }
    }
}
