using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace iCar_System
{
    class Renter : User
    {
        private bool isPrime;

        public bool IsPrime { get { return isPrime; } set { isPrime = value; } }

        private List<Booking> bookings;

        private List<Review> reviews;

        private DriverLicense license;

        public DriverLicense License { get { return license; } set { license = value; } }

        public Renter() { }

        public Renter(int uid, string n, string cn, DateTime dob, string ea, string ha) : base(uid, n, cn, dob, ea, ha) 
        {
            IsPrime = false; // by default false since you must spend money to become a prime renter
            bookings = new List<Booking>();
            reviews = new List<Review>();
        }

        public bool hasReviewed(Car car)
        {
            foreach (Review review in reviews)
            {
                if (review.getCar()  == car)
                {
                    return true;
                }
            }
            return false;
        }

        public void addReview(Review review)
        {
            reviews.Add(review);
        }

        public List<Booking> getBookingHistory()
        {
            List<Booking> bookingHistory = new List<Booking>();
            foreach (Booking booking in bookings)
            {
                if (booking.EndDateandTime < DateTime.Now)
                {
                    bookingHistory.Add(booking);
                }
            }
            return bookingHistory;
        }
    }
}
