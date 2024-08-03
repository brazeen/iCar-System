using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class DriverLicense
    {
        private string licenseNo;

        public string LicenseNo { get { return licenseNo; } set { licenseNo = value; } }

        private string fullName;

        public string FullName { get { return fullName; } set { fullName = value; } }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }

        private DateTime issueDate;

        public DateTime IssueDate { get { return issueDate; } set { issueDate = value; } }

        private DateTime expirationDate;

        public DateTime ExpirationDate { get { return expirationDate; } set { expirationDate = value; } }

        private string licenseClass;

        public string LicenseClass { get { return licenseClass; } set { licenseClass = value; } }

        private string countryOfIssue;

        public string CountryOfIssue { get { return countryOfIssue; } set { countryOfIssue = value; } }

        private Renter renter;

        public Renter Renter { get { return renter; } set { renter = value; } }

        //constructor
        public DriverLicense() { }

        public DriverLicense(string ln, string fn, DateTime dob, DateTime id, DateTime ed, string lc, string coi)
        {
            LicenseNo = ln;
            FullName = fn;
            DateOfBirth = dob;
            IssueDate = id;
            ExpirationDate = ed;
            LicenseClass = lc;
            CountryOfIssue = coi;
        }
    }
}
