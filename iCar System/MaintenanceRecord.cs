using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    public class MaintenanceRecord
    {
        private int recordID;

        public int RecordID { get; set; }

        private int carID;

        public int CarID { get; set; }

        private DateTime serviceDate;

        public DateTime ServiceDate { get; set; }

        private string serviceType;

        public string ServiceType { get; set; }

        private string serviceProvider;

        public string ServiceProvider { get; set; }

        private double cost;

        public double Cost { get; set; }

        private Car maintainedCar;

        public Car MaintainedCar { get; set; }

        //constructor
        public MaintenanceRecord() { }

        public MaintenanceRecord(int rid, int cid, DateTime sd, string st, string sp, double c, Car mc)
        {
            RecordID = rid;
            CarID = cid;
            ServiceDate = sd;
            ServiceType = st;
            ServiceProvider = sp;
            Cost = c;
            MaintainedCar = mc;
        }
    }
}
