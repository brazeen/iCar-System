using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    public class Insurance
    {
        private int insuranceNo; // should it be a string ah

        public int InsuranceNo { get; set; }

        private string company;

        public string Company { get; set; }

        private DateTime expirationDate;

        public DateTime ExpirationDate { get; set; }

        private string coverage;

        public string Coverage { get; set; }

        private Car insuredCar;

        public Car InsuredCar { get; set; }

        //constructor
        public Insurance() { }

        public Insurance(int ino, string c, DateTime ed, string cvg, Car ic)
        {
            InsuranceNo = ino;
            Company = c;
            ExpirationDate = ed;
            Coverage = cvg;
            InsuredCar = ic;
        }
    }
}
