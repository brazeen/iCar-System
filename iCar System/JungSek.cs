using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    internal class JungSek
    {
        static void Main(string[] args)
        {
            // Control methods
            bool ValidateBooking(DateTime startDateAndTime, DateTime endDateAndTime, string pickupLocation, string returnLocation)
            {
                if (endDateAndTime <= startDateAndTime)
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(pickupLocation) || string.IsNullOrWhiteSpace(returnLocation))
                {
                    return false;
                }
                return true;
            }

            // Test Data
            Renter loggedInRenter = new Renter(1, "Jung Sek", "94473979", new DateTime(2006, 4, 17), "jung@gmail.com", "1 Serangoon Road");

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
            Car car1 = new Car(1, "Toyota Camry", 2022, 5000, photos1, "Toyota", 29.99, new List<DateTime>());
            Car car2 = new Car(2, "Honda Civic", 2021, 10000, photos2, "Honda", 24.99, new List<DateTime>());

            List<Car> cars = new List<Car> { car1, car2 };

            // Use case: Book Car
            Console.WriteLine("Book Car Page");
            Console.Write("Enter start date and time (MM/DD/YYYY HH:MM AM/PM): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter end date and time (MM/DD/YYYY HH:MM AM/PM): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

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
                return;
            }

            Booking newBooking = new Booking(3, startDate, endDate, pickupLocation, dropoffLocation, selectedCar.Rate * (endDate - startDate).TotalHours)
            {
                CarInBooking = selectedCar,
                RenterInBooking = loggedInRenter
            };

            loggedInRenter.Bookings.Add(newBooking);
            selectedCar.Schedule.Add(newBooking);

            Console.WriteLine("Booking confirmed. Booking details:");
            Console.WriteLine($"Booking ID: {newBooking.BookingID}");
            Console.WriteLine($"Car: {selectedCar.Model}");
            Console.WriteLine($"Start Date and Time: {newBooking.StartDateAndTime}");
            Console.WriteLine($"End Date and Time: {newBooking.EndDateAndTime}");
            Console.WriteLine($"Pickup Location: {newBooking.PickupLocation}");
            Console.WriteLine($"Return Location: {newBooking.ReturnLocation}");
            Console.WriteLine($"Total Cost: ${newBooking.TotalCost}");
        }
    }
}
