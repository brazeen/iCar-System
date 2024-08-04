using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Booking
    {
        private int bookingID;

        public int BookingID { get { return bookingID; } set { bookingID = value; } }

        private DateTime startDateAndTime;

        public DateTime StartDateAndTime { get { return startDateAndTime; } set { startDateAndTime = value; } }

        private DateTime endDateAndTime;

        public DateTime EndDateAndTime { get { return endDateAndTime; } set { endDateAndTime = value; } }

        private string pickupLocation;

        public string PickupLocation { get { return pickupLocation; } set { pickupLocation = value; } }

        private string returnLocation;

        public string ReturnLocation { get { return returnLocation; } set { returnLocation = value; } }

        private double bookingFee;

        public double BookingFee { get { return bookingFee; } set { bookingFee = value; } }
        
        private double roadSideFee;

        public double RoadSideFee { get { return roadSideFee; } set { roadSideFee = value; } }

        private Car carInBooking;

        public Car CarInBooking { get {  return carInBooking; } set {  carInBooking = value; } }

        private Renter renterInBooking;

        public Renter RenterInBooking { get { return renterInBooking; } set { renterInBooking = value; } }

        public Booking() { }

        public Booking(int bid, DateTime sd, DateTime ed, string pl, string rl, double bf, double rf)
        {
            bookingID = bid;    
            StartDateAndTime = sd;
            EndDateAndTime = ed;
            PickupLocation = pl;
            ReturnLocation = rl;
            BookingFee = bf;
            RoadSideFee = rf;
        }

        public Car getCarInBooking() { return CarInBooking; }

        public void setCar(Car car) { CarInBooking = car; }

        public void setRenter(Renter renter) { RenterInBooking = renter; }
    }
}
