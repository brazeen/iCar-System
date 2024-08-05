using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class JungSek
    {
        // Utility methods
        static bool ValidateBooking(DateTime startDateAndTime, DateTime endDateAndTime, string pickupLocation, string dropoffLocation)
        {
            if (endDateAndTime <= startDateAndTime)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(pickupLocation) || string.IsNullOrWhiteSpace(dropoffLocation))
            {
                return false;
            }
            return true;
        }
        static DateTime ToDateTime(string dateTime)
        {
            return DateTime.ParseExact(dateTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }

        static void Main(string[] args)
        {
            // Test Data
            Renter loggedInRenter = new Renter(false,1, "Jung Sek", "94473979", new DateTime(2006, 4, 17), "jung@gmail.com", "1 Serangoon Road");

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
            Car car3 = new Car(1, "85 SX", 2007, 80, new List<string>() { }, "KTM", 4.5);
            car3.addUnavailabilityPeriod(
                new DateTime(2024, 11, 1, 0, 0, 0),
                new DateTime(2024, 11, 30, 23, 59, 59)
                );
            List<Car> cars = new List<Car> { car1, car2, car3 };

            // Use case: Book Car
            Console.WriteLine("Book Car Page");
            Console.Write("Enter start date and time (MM/DD/YYYY HH:MM AM/PM): ");
            DateTime startDate = ToDateTime(Console.ReadLine());
            Console.Write("Enter end date and time (MM/DD/YYYY HH:MM AM/PM): ");
            DateTime endDate = ToDateTime(Console.ReadLine());

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

            Console.Write("Enter Car ID to book: ");
            int selectedCarId = Convert.ToInt32(Console.ReadLine());

            var selectedCar = availableCars.FirstOrDefault(c => c.CarID == selectedCarId);
            if (selectedCar == null)
            {
                Console.WriteLine("Invalid Car ID");
                return;
            }

            Console.Write("Enter pick-up method (iCarStation / delivery): ");
            string pickupMethod = Console.ReadLine();
            Console.Write("Enter pick-up location: ");
            string pickupLocation = Console.ReadLine();
            Console.Write("Enter drop-off method (iCarStation / delivery): ");
            string dropoffMethod = Console.ReadLine();
            Console.Write("Enter drop-off location: ");
            string dropoffLocation = Console.ReadLine();

            if (!ValidateBooking(startDate, endDate, pickupLocation, dropoffLocation))
            {
                Console.WriteLine("Invalid booking details.");
                return;
            }

            Console.WriteLine("Confirm booking? (yes/no)");
            string confirmation = Console.ReadLine();
            if (confirmation.ToLower() != "yes")
            {
                Console.WriteLine("Booking not confirmed.");
                Console.WriteLine("Car Not Booked");
                return;
            }

            Booking newBooking = new Booking(3, startDate, endDate, new Tuple<string, string>(pickupMethod, pickupLocation), selectedCar.Rate * (endDate - startDate).TotalHours, 0)
            {
                CarInBooking = selectedCar,
                RenterInBooking = loggedInRenter
            };

            loggedInRenter.Bookings.Add(newBooking);
            selectedCar.addBooking(newBooking);

            Console.WriteLine("Booking confirmed. Booking details:");
            Console.WriteLine($"Booking ID: {newBooking.BookingId}");
            Console.WriteLine($"Car: {selectedCar.Model}");
            Console.WriteLine($"Start Date and Time: {newBooking.StartDateAndTime}");
            Console.WriteLine($"End Date and Time: {newBooking.EndDateAndTime}");
            Console.WriteLine($"Pickup Location: {newBooking.PickUpDetails.Item2}");
            Console.WriteLine($"Dropoff Location: {newBooking.DropOffDetails.Item2}");
            Console.WriteLine($"Total Cost: ${newBooking.BookingFee}");
            Console.WriteLine("Car Booked Successfully");
        }
    }
}
