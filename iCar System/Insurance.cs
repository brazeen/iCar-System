using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Insurance
    {
        private int insuranceNo; // should it be a string ah

        public int InsuranceNo { get { return insuranceNo; } set { insuranceNo = value; } }

        private string company;

        public string Company { get { return company; } set { company = value; } }

        private DateTime expirationDate;

        public DateTime ExpirationDate { get { return expirationDate; } set { expirationDate = value; } }

        private string coverage;

        public string Coverage { get { return coverage; } set { coverage = value; } }

        private Car insuredCar;

        public Car InsuredCar { get { return insuredCar; } set { insuredCar = value; } }

        //constructor
        public Insurance() { }

        public Insurance(int ino, string c, DateTime ed, string cvg)
        {
            InsuranceNo = ino;
            Company = c;
            ExpirationDate = ed;
            Coverage = cvg;
        }

        public void setCar(Car car) { InsuredCar = car; }
    }
}
