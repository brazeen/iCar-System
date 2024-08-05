using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace iCar_System
{
    class JungSek
    {
        // Utility methods
        static bool ValidateBooking(DateTime startDateAndTime, DateTime endDateAndTime)
        {
            return endDateAndTime > startDateAndTime;
        }

        static DateTime ToDateTime(string dateTime)
        {
            return DateTime.ParseExact(dateTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }

        static DateTime PromptForDateTime(string prompt)
        {
            DateTime dateTime;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                try
                {
                    dateTime = ToDateTime(input);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid date/time format. Please use MM/DD/YYYY HH:MM AM/PM.");
                }
            }
            return dateTime;
        }

        static Tuple<DateTime, DateTime> PromptForBookingPeriod()
        {
            DateTime startDate;
            DateTime endDate;
            while (true)
            {
                startDate = PromptForDateTime("Enter start date and time (MM/DD/YYYY HH:MM AM/PM): ");
                endDate = PromptForDateTime("Enter end date and time (MM/DD/YYYY HH:MM AM/PM): ");
                if (ValidateBooking(startDate, endDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("End date must be later than start date. Please enter the dates again.");
                }
            }
            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        static string PromptForPickupMethod()
        {
            string pickupMethod;
            while (true)
            {
                Console.Write("Enter pick-up method (iCarStation / delivery): ");
                pickupMethod = Console.ReadLine().Trim();
                if (pickupMethod.Equals("iCarStation", StringComparison.OrdinalIgnoreCase) || pickupMethod.Equals("delivery", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid pickup method. Please enter 'iCarStation' or 'delivery'.");
                }
            }
            return pickupMethod;
        }

        static string PromptForPostalCode(string prompt)
        {
            string postalCode;
            while (true)
            {
                Console.Write(prompt);
                postalCode = Console.ReadLine().Trim();
                if (postalCode.Length == 6 && postalCode.All(char.IsDigit))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid postal code. Please enter a 6-digit postal code.");
                }
            }
            return postalCode;
        }

        static int PromptForCarId(List<Car> availableCars)
        {
            int carId;
            while (true)
            {
                Console.Write("Enter Car ID to book: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out carId) && availableCars.Any(car => car.CarID == carId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Car ID. Please select a valid Car ID from the list.");
                }
            }
            return carId;
        }

        static string PromptForConfirmation()
        {
            string confirmation;
            while (true)
            {
                Console.WriteLine("Confirm booking? (yes/no)");
                confirmation = Console.ReadLine().Trim().ToLower();
                if (confirmation == "yes" || confirmation == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            }
            return confirmation;
        }

        static List<Car> CreateTestData()
        {
            List<string> photos1 = new List<string>
            {
                "photo1.jpg",
                "photo2.jpg"
            };

            List<string> photos2 = new List<string>
            {
                "photo3.jpg",
                "photo4.jpg"
            };

            Car car1 = new Car(1, "Toyota Camry", 2022, 5000, photos1, "Toyota", 29.99);
            Car car2 = new Car(2, "Honda Civic", 2021, 10000, photos2, "Honda", 24.99);
            Car car3 = new Car(3, "85 SX", 2007, 80, new List<string>() { }, "KTM", 4.5);
            car3.addUnavailabilityPeriod(
                new DateTime(2024, 11, 1, 0, 0, 0),
                new DateTime(2024, 11, 30, 23, 59, 59)
            );

            return new List<Car> { car1, car2, car3 };
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("==== iCar System ====");
            Console.WriteLine("1. Book a Car");
            Console.WriteLine("2. Exit");
            Console.Write("Select an option: ");
        }

        static void BookCar(Renter renter, List<Car> cars)
        {
            Console.WriteLine("==== Book Car Page ====");
            var Schedule = new List<Dictionary<string, DateTime>>();
            Schedule = cars[2].Schedule;
            foreach (var sched in Schedule)
            {
                foreach (var item in sched)
                {
                    Console.WriteLine($"{item}");
                }
            }
            var bookingPeriod = PromptForBookingPeriod();
            DateTime startDate = bookingPeriod.Item1;
            DateTime endDate = bookingPeriod.Item2;

            Console.WriteLine("Checking for available cars...");
            List<Car> availableCars = cars.Where(car => car.IsAvailable(startDate, endDate)).ToList();

            if (availableCars.Count > 0)
            {
                Console.WriteLine("Available Cars:");
                foreach (Car car in availableCars)
                {
                    Console.WriteLine($"Car ID: {car.CarID}, Model: {car.Model}, Brand: {car.Make}");
                }
            }
            else
            {
                Console.WriteLine("No available cars for the selected period.");
                return;
            }

            int selectedCarId = PromptForCarId(availableCars);
            var selectedCar = availableCars.First(c => c.CarID == selectedCarId);

            string pickupMethod = PromptForPickupMethod();
            string pickupLocation = PromptForPostalCode("Enter pick-up location (6-digit postal code): ");

            string confirmation = PromptForConfirmation();
            if (confirmation == "no")
            {
                Console.WriteLine("Booking not confirmed.");
                Console.WriteLine("Car Not Booked");
                return;
            }

            Booking newBooking = new Booking(3, startDate, endDate, new Tuple<string, string>(pickupMethod, pickupLocation), 0);
            newBooking.setCar(selectedCar);
            newBooking.setRenter(loggedInRenter);

            renter.Bookings.Add(newBooking);
            selectedCar.addBooking(newBooking);

            Console.WriteLine("Booking confirmed. Booking details:");
            Console.WriteLine($"Booking ID: {newBooking.BookingId}");
            Console.WriteLine($"Car ID: {selectedCar.CarID}, Model: {selectedCar.Model}, Brand: {selectedCar.Make}");
            Console.WriteLine($"Start Date and Time: {newBooking.StartDateAndTime}");
            Console.WriteLine($"End Date and Time: {newBooking.EndDateAndTime}");
            Console.WriteLine($"Pickup Method: {newBooking.PickUpDetails.Item1}");
            Console.WriteLine($"Pickup Location: {newBooking.PickUpDetails.Item2}");
            Console.WriteLine($"Booking Fee: ${newBooking.BookingFee}");
            Console.WriteLine("Car Booked Successfully");
        }

        static void Main(string[] args)
        {
            Renter loggedInRenter = new Renter(false, 1, "Jung Sek", "94473979", new DateTime(2006, 4, 17), "jung@gmail.com", "1 Serangoon Road");
            List<Car> cars = CreateTestData();

            while (true)
            {
                DisplayMainMenu();
                string option = Console.ReadLine();

                if (option == "1")
                {
                    BookCar(loggedInRenter, cars);
                }
                else if (option == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }
    }
}
