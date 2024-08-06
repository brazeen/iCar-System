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

        public int UserID { get { return userID; } set { userID = value; } }

        private string name;

        public string Name { get { return name; } set { name = value; } }

        private string contactNumber;

        public string ContactNumber { get { return contactNumber; } set { contactNumber = value; } }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }

        private string emailAddress;

        public string EmailAddress { get { return emailAddress; } set { emailAddress = value; } }

        private string homePostalCode;

        public string HomePostalCode { get { return homePostalCode; } set { homePostalCode = value; } }

        private Admin authenticatedAdmin;

        public Admin AuthenticatedAdmin { get { return authenticatedAdmin; } set { authenticatedAdmin = value; } }

        //constructor
        public User() { }

        public User(int uid, string n, string cn, DateTime dob, string ea, string ha)
        {
            UserID = uid;
            Name = n;
            ContactNumber = cn;
            DateOfBirth = dob;
            EmailAddress = ea;
            HomePostalCode = ha;
        }

        public void setAdmin(Admin admin) { AuthenticatedAdmin = admin; }
    }
}
