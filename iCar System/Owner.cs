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

        private Car ownedCar;

        public Car OwnedCar { get { return ownedCar; } set { ownedCar = value; } }

        public Owner() { }
        public Owner(int uid, string n, string cn, DateTime dob, string ea, string hpc) : base(uid, n, cn, dob, ea, hpc)
        {
            TotalEarnings = 0; //by default total earnings start at 0
        }
    }
}
