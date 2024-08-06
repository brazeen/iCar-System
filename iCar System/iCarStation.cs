using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class iCarStation
    {
        private int stationID;

        public int StationID { get { return stationID; } set { stationID = value; } }

        private string postalCode;

        public string PostalCode { get { return postalCode; } set { postalCode = value; } }

        public iCarStation() { }

        public iCarStation(int sID, string pc)
        {
            stationID = sID;
            postalCode = pc;
        }
    }
}
