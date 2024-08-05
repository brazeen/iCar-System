using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
	class MonthlyPayment
	{
		private int paymentID;

		public int PaymentID { get { return paymentID; } set { paymentID = value; } }

        private DateTime paymentDate;

		public DateTime PaymentDate { get { return paymentDate; } set { paymentDate = value; } }

        private double paymentAmount;

		public double PaymentAmount { get { return paymentAmount; } set { paymentAmount = value; } }

        private string paymentMethod;

		public string PaymentMethod { get { return paymentMethod; } set { paymentMethod = value; } }

        private List<Booking> bookingList;

        public List<Booking> BookingList { get { return bookingList; } set { bookingList = value; } }


        //constructor
        public MonthlyPayment() { }

		public MonthlyPayment(int pid, DateTime pd, double pa, string pm)
		{
			PaymentID = pid;
			PaymentDate = pd;
			PaymentAmount = pa;
			PaymentMethod = pm;
			BookingList = new List<Booking>();
		}

        public bool CheckForPrime(List<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                if (!booking.RenterInBooking.IsPrime)
                {
                    return false;
                }
            }
            return true;
        }

        public double ApplyDiscount(double monthlyBookingFee, double monthlyRoadSideFee)
        {
            double discount = 0.10; // 10% discount
            PaymentAmount = ((monthlyBookingFee/100)*90) + ((monthlyRoadSideFee / 100) * 50);
			return PaymentAmount;
        }

        public bool ValidateCardCredentials(string cardNumber, DateTime expiryDate, int cvc)
        {
            if (cardNumber.Length != 16)
            {
                return false;
            }


            if (expiryDate < DateTime.Now)
            {
                return false;
            }


            if (cvc < 100 || cvc > 999)
            {
                return false;
            }

            return true;
        }
    }
}
