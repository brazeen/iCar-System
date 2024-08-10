using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class MaintenanceRecord
    {
        private int recordID;

        public int RecordID { get { return recordID; } set { recordID = value; } }

        private DateTime serviceDateAndTime;

        public DateTime ServiceDateAndTime { get { return serviceDateAndTime; } set { serviceDateAndTime = value; } }

        private string serviceType;

        public string ServiceType { get { return serviceType; } set { serviceType = value; } }

        private string serviceProvider;

        public string ServiceProvider { get { return serviceProvider; } set { serviceProvider = value; } }

        private double cost;

        public double Cost { get { return cost; } set { cost = value; } }

        private Car maintainedCar;

        public Car MaintainedCar { get { return maintainedCar; } set { maintainedCar = value; } }

        //constructor
        public MaintenanceRecord() { }

        public MaintenanceRecord(int rid, int cid, DateTime sd, string st, string sp, double c)
        {
            RecordID = rid;
            ServiceDateAndTime = sd;
            ServiceType = st;
            ServiceProvider = sp;
            Cost = c;
        }

        public void setCar(Car car) { MaintainedCar = car; }
    }
}
