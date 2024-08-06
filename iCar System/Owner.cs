using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Owner : User
    {
        private int totalEarnings;

        public int TotalEarnings { get { return totalEarnings; } set { totalEarnings = value; } }

        private List<Car> ownedCars;

        public List<Car> OwnedCars { get { return ownedCars; } set { ownedCars = value; } }

        public Owner() { }
        public Owner(int uid, string n, string cn, DateTime dob, string ea, string hpc) : base(uid, n, cn, dob, ea, hpc)
        {
            TotalEarnings = 0; //by default total earnings start at 0
            OwnedCars = new List<Car>();
        }
    }
}
