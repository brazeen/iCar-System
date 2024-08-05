using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Booking
    {
        private int bookingId;

        public int BookingId { get { return bookingId; } set { bookingId = value; } }

        private DateTime startDateAndTime;

        public DateTime StartDateAndTime { get { return startDateAndTime; } set { startDateAndTime = value; } }

        private DateTime endDateAndTime;

        public DateTime EndDateAndTime { get { return endDateAndTime; } set { endDateAndTime = value; } }

        private Tuple<string, string> pickUpDetails;

        public Tuple<string, string> PickUpDetails { get { return pickUpDetails; } set { pickUpDetails = value; } }

        private Tuple<string, string> dropOffDetails;

        public Tuple<string, string> DropOffDetails { get { return dropOffDetails; } set { dropOffDetails = value; } }

        private double bookingFee;

        public double BookingFee { get { return bookingFee; } set { bookingFee = value; } }
        
        private double roadSideFee;

        public double RoadSideFee { get { return roadSideFee; } set { roadSideFee = value; } }

        private Car carInBooking;

        public Car CarInBooking { get {  return carInBooking; } set {  carInBooking = value; } }

        private Renter renterInBooking;

        public Renter RenterInBooking { get { return renterInBooking; } set { renterInBooking = value; } }

        public Booking() { }

        public Booking(int bid, DateTime sd, DateTime ed, Tuple<string, string> pl, double bf, double rf)
        {
            bookingId = bid;    
            StartDateAndTime = sd;
            EndDateAndTime = ed;
            PickUpDetails = pl;
            BookingFee = bf;
            RoadSideFee = rf;
        }

        public Car getCarInBooking() { return CarInBooking; }

        public void setCar(Car car) { CarInBooking = car; }

        public void setRenter(Renter renter) { RenterInBooking = renter; }

        public void updateBooking(DateTime newStartDateAndTime, DateTime newEndDateAndTime, Tuple<string, string> newPickUpDetails)
        { 
            startDateAndTime = newStartDateAndTime;
            endDateAndTime = newEndDateAndTime;
            pickUpDetails = newPickUpDetails;
        }
        public Tuple<DateTime, DateTime> getBookingPeriod() { return new Tuple<DateTime, DateTime>(startDateAndTime, endDateAndTime); }
        public override string ToString(){
            return $"" +
                $"Booking start: {StartDateAndTime.ToString("dd/MM/yy hh:mm tt")}" +
                $"\nBooking end: {EndDateAndTime.ToString("dd/MM/yy hh:mm tt")}" +
                $"\nPick up details: {PickUpDetails}\nDrop off details: {DropOffDetails}" +
                $"\nBooking fee: {BookingFee}" +
                $"\nCar: {CarInBooking.Model}";
    }   }
}
