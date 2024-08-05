using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Brydon
    {
        //main program loop
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
            reservationToModify.setCar(targetCar);
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
            reservation1.setCar(targetCar);
            Booking reservation2 = new Booking(
                2,
                new DateTime(2024, 10, 1, 19, 43, 0),
                new DateTime(2024, 10, 8, 11, 12, 0),
                new Tuple<string, string>("Deliver", "605012"),
                new Tuple<string, string>("Deliver", "650534"),
                12.5,
                3
                );
            reservation2.setCar(targetCar);
            //add bookings to car
            targetCar.addBooking(reservationToModify);
            targetCar.addBooking(reservation1);
            targetCar.addBooking( reservation2 );
            //add to "database"
            List<Booking> bookingList = new List<Booking>() { reservationToModify, reservation1, reservation2 };

            /*
            ===========
            Controller
            ==========
            */

            //utility functions (self call)
            //accept a target booking id and a list of bookings
            //remove the booking that matches the target id
            static List<Booking> excludeReservation(int bookingId, List<Booking> bookings)
            {
                foreach (Booking booking in bookings)
                {
                    if (booking.BookingId == bookingId)
                    {
                        bookings.Remove(booking);
                        break;
                    }
                }
                return bookings;
            }

            //convert a date string and time string to a date time obj and return it
            static DateTime toDateTime(string date, string time) 
            {
                return DateTime.ParseExact($"{date} {time}", "dd/MM/yy h:mm tt", CultureInfo.InvariantCulture);
            }

            //iterate through all bookings and find the one that matches the target param
            //else return null (would never be triggered in the context of this use case)
            Booking GetBooking(int bookingId)
            {
                foreach (var booking in bookingList) if (booking.BookingId == bookingId) return booking;
                return null;
            }
            //return the availability schedule
            //returns a tuple for the target booking obj, existing reservations and car's availibility schedule
            Tuple<Booking, List<Booking>, List<Dictionary<string, DateTime>>> modifyReservation(int bookingId)
            {

                Booking booking = GetBooking(bookingId);
                Car car = booking.getCarInBooking();
                //get all reservations and exclude the one we want to modify
                List<Booking> listOfReservations = car.getReservations();
                List<Booking> otherReservations = excludeReservation(bookingId, listOfReservations);
                //also get the availability schedule
                List<Dictionary<string, DateTime>> availabilitySchedule = car.getAvailabilitySchedule();    
                return new Tuple<Booking, List<Booking>, List<Dictionary<string, DateTime>>>(booking, otherReservations, availabilitySchedule);
            }

            string setReservation(string startDate, string startTime, string endDate, string endTime, string pickUpDetails, string dropOffDetails, Booking booking, List<Booking> otherReservations, List<Dictionary<string, DateTime>> availabilitySchedule)
            {
                DateTime startDateAndTime = toDateTime(startDate, startTime);
                Console.WriteLine(startDateAndTime);
                return "";
            }
            /*
            ===========
             UI
            ==========
             */
            //format datetime to a string in the format
            String formatDateTime(DateTime dateTimeObj) 
            {
                return dateTimeObj.ToString("dd/MM/yy hh:mm tt");
            }
            void displayUnavailability(List<Booking> otherReservations, List<Dictionary<string, DateTime>> availabilitySchedule) 
            {
                Console.WriteLine("Times where the car is unavailable:\n");
                //display the other bookings
                Console.WriteLine("Other bookings:");
                foreach (Booking booking in otherReservations) 
                { 
                    (DateTime start, DateTime end) = booking.getBookingPeriod();
                    Console.WriteLine($"{formatDateTime(start)} to {formatDateTime(end)}");
                }
                Console.WriteLine("\nCar's availability schedule:");
                //display the car availability schedule
                foreach (Dictionary<string, DateTime> period in availabilitySchedule) {
                    Console.WriteLine($"{formatDateTime(period["startDateAndTime"])} to {formatDateTime(period["endDateAndTime"])}");
                }
            }

            void promptForReservation()
            {
                Console.WriteLine("\nEnter your new reservation details here:");
            }
            //use case start 
            while (true)
            {
                //display reservation and prompt user to enter a option
                Console.Write($"{reservationToModify}\n\n[1] modify reservation\n[0] quit\nEnter your option here: ");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    //call modifyReservation and get the unavailability schedule
                    (Booking booking, List<Booking> otherReservations, List<Dictionary<string, DateTime>> availabilitySchedule) = modifyReservation(reservationToModify.BookingId);
                    displayUnavailability(otherReservations, availabilitySchedule);
                    promptForReservation();
                    //get user input
                    Console.Write("Start date (DD/MM/YY): ");
                    string startDate = Console.ReadLine();
                    Console.Write("Start time (HH:MM PM/AM): ");
                    string startTime = Console.ReadLine();
                    Console.Write("End date (DD/MM/YY): ");
                    string endDate = Console.ReadLine();
                    Console.Write("End time (HH:MM PM/AM): ");
                    string endTime = Console.ReadLine();
                    Console.Write("Pick up details (station/delivery zipcode): ");
                    string pickUpDetails = Console.ReadLine();
                    Console.Write("Drop off details (station/delivery zipcode): ");
                    string dropOffDetails = Console.ReadLine();
                    //get the result of the reservation
                    string message = setReservation(startDate, startTime, endDate, endTime, pickUpDetails, dropOffDetails, booking, otherReservations, availabilitySchedule);
                }
                else if (option == "0") 
                {
                    break;
                }
            }





        }
    }
}
