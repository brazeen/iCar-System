using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Brydon
    {
        static Tuple<List<Dictionary<string, DateTime>>,List<Dictionary<string, DateTime>>> modifyReservation(int bookingId) {
            return Tuple.Create(null, null);
        }

        //iterate through all bookings and find the one that matches the target param
        //else return null (would never be triggered in the context of this use case)
        static Booking GetBooking(int bookingId, List<Booking> bookingList)
        {
            foreach (var booking in bookingList)
            {
                if (booking.BookingId == bookingId) { 
                    return booking;
                }
            }
            return null;
        }
        
        static void Main(string[] args)
        {
            //create data
            Booking reservationToModify = new Booking(1, new DateTime(2024,10,9,10,30,45), new DateTime(2024,10,14,15,12,9));


        }
    }
}
