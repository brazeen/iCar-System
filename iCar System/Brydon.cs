using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Brydon
    {
        static 
        static void Main(string[] args)
        {
            //create data
            //target car
            Car targetCar = new Car(1, "85 SX", 2007, 80, new List<string>() { }, "KTM", 4.5);
            targetCar.addUnavailabilityPeriod(
                new DateTime(2024, 11, 1, 0, 0, 0),
                new DateTime(2024, 11, 30, 23, 59, 59)
                );
            //target booking
            Booking reservationToModify = new Booking(
                1,
                new DateTime(2024, 10, 9, 10, 30, 45),
                new DateTime(2024, 10, 14, 15, 12, 9),
                new Tuple<string, string>("Deliver", "650534"),
                new Tuple<string, string>("Deliver", "650534"),
                10.5,
                3
                );
            //dummy existing reservation
            Booking reservation1 = new Booking(
                2,
                new DateTime(2024, 9, 12, 15, 30, 0),
                new DateTime(2024, 9, 18, 10, 12, 0),
                new Tuple<string, string>("Deliver", "200808"),
                new Tuple<string, string>("Deliver", "200808"),
                12.5,
                3
                );
            Booking reservation2 = new Booking(
                2,
                new DateTime(2024, 10, 1, 19, 43, 0),
                new DateTime(2024, 10, 8, 11, 12, 0),
                new Tuple<string, string>("Deliver", "605012"),
                new Tuple<string, string>("Deliver", "650534"),
                12.5,
                3
                );
            //add to "database"
            List<Booking> bookingList = new List<Booking>() { reservationToModify, reservation1, reservation2 };

            //controller functions

            //iterate through all bookings and find the one that matches the target param
            //else return null (would never be triggered in the context of this use case)
            Booking GetBooking(int bookingId)
            {
                foreach (var booking in bookingList) if (booking.BookingId == bookingId) return booking;
                return null;
            }
            //return the availability schedule
            //returns a tuple for the target booking obj, existing reservations and car's availibility schedule
            Tuple<List<Dictionary<string, DateTime>>, List<Dictionary<string, DateTime>>> modifyReservation(int bookingId)
            {

                Booking booking = GetBooking(bookingId);
                Car car = booking.getCarInBooking();

                return Tuple.Create(null, null);
            }

            //use case start
            while (true)
            {
                //display reservation and prompt user to enter a option
                Console.WriteLine($"{reservationToModify}\n\n[1] modify reservation\n[0] quit");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    modifyReservation(reservationToModify.BookingId);
                }
                else if (option == "0") 
                {
                    break;
                }
            }





        }
    }
}
