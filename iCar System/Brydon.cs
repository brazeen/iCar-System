using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iCar_System.Brydon;

namespace iCar_System
{
    class Brydon
    {
        static List<Booking> createData() 
        {
            //create data
            //target car
            Car targetCar = new Car(1, "85 SX", 2007, 80, new List<string>() { }, "KTM", 20);
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
                0
                );
            reservationToModify.setCar(targetCar);
            //dummy existing reservation
            Booking reservation1 = new Booking(
                2,
                new DateTime(2024, 9, 12, 15, 30, 0),
                new DateTime(2024, 9, 18, 10, 12, 0),
                new Tuple<string, string>("Deliver", "200808"),
                0
                );
            reservation1.setCar(targetCar);
            Booking reservation2 = new Booking(
                3,
                new DateTime(2024, 10, 1, 19, 43, 0),
                new DateTime(2024, 10, 8, 11, 12, 0),
                new Tuple<string, string>("Deliver", "605012"),
                0
                );
            reservation2.setCar(targetCar);
            //add bookings to car
            targetCar.addBooking(reservationToModify);
            targetCar.addBooking(reservation1);
            targetCar.addBooking(reservation2);
            //return "database"
            return new List<Booking>() { reservationToModify, reservation1, reservation2 };

        }

        public class UI
        {
            private Controller controller { get; set; }
            //"global variables"
            private Booking booking { get; set; }
            private int bookingId { get; set; } //the booking id of the booking to modify
            private List<Booking> otherReservations { get; set; }
            private List<Dictionary<string, DateTime>> availabilitySchedule { get; set; }
            public UI() { }
            public UI(Controller c, int b) 
            {
                controller = c;
                bookingId = b;
            }
            //format datetime to a string in the format
            private static string formatDateTime(DateTime dateTimeObj)
            {
                return dateTimeObj.ToString("dd/MM/yy hh:mm tt");
            }
            private static void displayUnavailability(List<Booking> otherReservations, List<Dictionary<string, DateTime>> availabilitySchedule)
            {
                Console.WriteLine("Times where the car is unavailable:\n");
                //display the other bookings
                Console.WriteLine("Other bookings:");
                foreach (Booking booking in otherReservations)
                {
                    (DateTime start, DateTime end) = booking.getBookingPeriod();
                    Console.WriteLine($"{formatDateTime(start)} to {formatDateTime(end)}");
                }
                Console.WriteLine("\nCar's schedule:");
                //display the car availability schedule
                foreach (Dictionary<string, DateTime> period in availabilitySchedule)
                {
                    Console.WriteLine($"{formatDateTime(period["startDateAndTime"])} to {formatDateTime(period["endDateAndTime"])}");
                }
            }
            private void promptForReservation()
            {
                Console.WriteLine("\nEnter your new reservation details here:");
                
            }
            private void displayMessage(string message)
            {
                Console.WriteLine($"{message}\n\n");
            }

            //functions that can be triggered by the user
            public void modifyReservation(int bookingId)
            {
                (booking, otherReservations, availabilitySchedule) = controller.modifyReservation(bookingId);
                displayUnavailability(otherReservations, availabilitySchedule);
                promptForReservation();
            }

            //UI 
            public void run() {
                //call modifyReservation in the UI
                modifyReservation(bookingId);
                //after prompting for reservation, get reservation info here
                string message = "";
                while (message != "Update successful")
                {
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
                    //get the result of the reservation
                    message = controller.setReservation(startDate, startTime, endDate, endTime, pickUpDetails, booking, otherReservations, availabilitySchedule);
                    displayMessage(message);
                }
            }
        }
        public class Controller
        {

            private List<Booking> bookingList { get; set; }
            public Controller() { }
            //accept the data
            public Controller(List<Booking> bL) 
            { 
                bookingList = bL;
            }
            //utility functions (self call)
            //accept a target booking id and a list of bookings
            //remove the booking that matches the target id
            private static List<Booking> excludeReservation(int bookingId, List<Booking> bookings)
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
            private static DateTime toDateTime(string date, string time)
            {
                return DateTime.ParseExact($"{date} {time}", "dd/MM/yy h:mm tt", CultureInfo.InvariantCulture);
            }
            //validate that the end time is ahead of the start time
            private static bool validateEndTime(DateTime end, DateTime start)
            {
                return DateTime.Compare(start, end) >= 0;
            }
            //check if the car's timing overlaps with availability schedule or bookings
            private static bool isCarAvailable(DateTime startTimeAndDate, DateTime endTimeAndDate, List<Booking> otherReservations, List<Dictionary<string, DateTime>> availabilitySchedule)
            {
                //validate other reservations
                foreach (Booking booking in otherReservations)
                {
                    Tuple<DateTime, DateTime> bookingPeriod = booking.getBookingPeriod();
                    //check if they overlap
                    if (bookingPeriod.Item1 < endTimeAndDate && startTimeAndDate < bookingPeriod.Item2) return false;
                }
                //validate availability schedule
                foreach (Dictionary<string, DateTime> unavailablePeriod in availabilitySchedule)
                {
                    //check if they overlap
                    if (unavailablePeriod["startDateAndTime"] < endTimeAndDate && startTimeAndDate < unavailablePeriod["endDateAndTime"]) return false;
                }
                return true;
            }

            //iterate through all bookings and find the one that matches the target param
            //else return null (would never be triggered in the context of this use case)
            private Booking GetBooking(int bookingId)
            {
                foreach (var booking in bookingList) if (booking.BookingId == bookingId) return booking;
                return null;
            }

            //functions that can be accessed by other classes

            //return the availability schedule
            //returns a tuple for the target booking obj, existing reservations and car's availibility schedule
            public Tuple<Booking, List<Booking>, List<Dictionary<string, DateTime>>> modifyReservation(int bookingId)
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

            public string setReservation(string startDate, string startTime, string endDate, string endTime, string pickUpDetails, Booking booking, List<Booking> otherReservations, List<Dictionary<string, DateTime>> availabilitySchedule)
            {
                DateTime startDateAndTime = toDateTime(startDate, startTime);
                DateTime endDateAndTime = toDateTime(endDate, endTime);
                //verify the data
                bool validEnd = validateEndTime(startDateAndTime, endDateAndTime);
                if (!validEnd) return "The end time must be ahead of the start time. Please try again.";
                bool isAvailable = isCarAvailable(startDateAndTime, endDateAndTime, otherReservations, availabilitySchedule);
                if (!isAvailable) return "The car is not available at this time. Please try again.";
                //success, update booking
                string[] splitArray = pickUpDetails.Split(' ');
                Tuple<string, string> pickUpDetailsFormatted = new Tuple<string, string>(splitArray[0], splitArray[1]);
                booking.updateBooking(startDateAndTime, endDateAndTime, pickUpDetailsFormatted);
                return "Update successful";
            }
        }
        //main program loop
        static void Main(string[] args)
        {
            //get the test data
            List<Booking> testData = createData();
            Booking reservationToModify = testData[0];
            //create the UI and controller classes
            Controller controller = new Controller(testData);
            UI ui = new UI(controller, reservationToModify.BookingId);
            //this simulates the "track rental" page where users can choose to modify a reservation
            while (true)
            {
                //display reservation and prompt user to enter a option
                Console.Write($"{reservationToModify}\n\n[1] modify reservation\n[0] quit\nEnter your option here: ");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    //run the use case
                    ui.run();
          
                }
                else if (option == "0") 
                {
                    break;
                }
            }





        }
    }
}
