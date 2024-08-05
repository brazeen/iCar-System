using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Donovan
    {
        private static double AdditionalLocationFee = 0.50;
        private static double LateFeePerMinute = 0.10;
        private static TimeSpan ReturnBuffer = TimeSpan.FromMinutes(15);
        private static List<Booking> bookings = new List<Booking>(); // Store all bookings
        private static List<Car> carlist = new List<Car>();

        //Controllers
        private static void SelectReturnLocation(int option, Booking booking)
        {
            if (option == 1)
            {
                ReturnToiCarStation(booking);
            }
            else if (option == 2)
            {
                ReturnByDelivery(booking);
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
                displayReturnLocations(booking);
            }
        }

        private static iCarStation GetSelectedStation(int stationSelect)
        {
            iCarStation station1 = new iCarStation(1, "600123");
            iCarStation station2 = new iCarStation(2, "600234");
            switch (stationSelect)
            {
                case 1:
                    return station1;
                case 2:
                    return station2;
                default:
                    // Handle invalid input
                    Console.WriteLine("Invalid station selection. Please try again.");

                    return null;
            }
        }

        //UI
        private static void ReturnCar(int bookingid, int carid)
        {
            Booking booking = bookings.FirstOrDefault(b => b.BookingId == bookingid); //check for bookingID
            if (booking == null)
            {
                Console.WriteLine("Invalid booking ID.");
                return;
            }
            Car car = carlist.FirstOrDefault(c => c.CarID == carid);
            if (car == null) //check if car is found
            {
                Console.WriteLine("Error: Car not found for this booking.");
                return;
            }
            //Booking booking1 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 8, 6, 0, 13, 0), new Tuple<string, string>("Deliver", "239085"), 20, 10);
            //Car car1 = new Car(1, "Honda Civic", 2021, 10000, new List<string>() { }, "Honda", 10);
            booking.setCar(car);
            //Car car = booking.getCarInBooking();
            //booking.DropOffDetails = new Tuple<string, string>();
            displayReturnLocations(booking);
        }

        private static void displayReturnLocations(Booking booking)
        {
            Console.WriteLine("Where do you want to return your car?\n" +
                "[1] iCar Station\n" +
                "[2] Desired Location");
            int option = Convert.ToInt32(Console.ReadLine());
            SelectReturnLocation(option, booking);
        }


        private static void ReturnToiCarStation(Booking booking)
        {
            TimeSpan lateDuration = DateTime.Now - booking.EndDateAndTime;
            double minutesDiff = lateDuration.TotalMinutes;
            var totalCost = booking.BookingFee + booking.RoadSideFee;
            iCarStation station1 = new iCarStation(1, "600123");
            iCarStation station2 = new iCarStation(2, "600234");
            Console.WriteLine($"Select an iCar station:\n" +
                            $"[1]{station1.PostalCode}\n" +
                            $"[2]{station2.PostalCode}");
            int stationSelect = Convert.ToInt32(Console.ReadLine());
            iCarStation selectedStation = GetSelectedStation(stationSelect); // Retrieve station details

            // Start 15-minute timer

            Console.WriteLine("You have 15 minutes to return your car to the chosen location. Enter 'D' when returned.");
            string confirmation = Console.ReadLine();
            if (confirmation == "D".ToLower())
            {
                booking.DropOffDetails = new Tuple<string, string>("iCarStation", selectedStation.PostalCode);
                if (DateTime.Now - booking.EndDateAndTime <= ReturnBuffer)
                {
                    Console.WriteLine("Return is successful.");
                    Console.WriteLine($"Additonal/Penalty Fees: $0\n" +
                                        $"Total Cost: ${totalCost.ToString("00.00")}");
                }
                else
                {
                    var lateFee = (lateDuration.TotalMinutes * LateFeePerMinute);
                    booking.BookingFee += lateFee;
                    Console.WriteLine($"You are {minutesDiff.ToString("F2")} minutes late. A late fee of ${lateFee.ToString("F2")} will be applied.");
                    Console.WriteLine($"Additonal/Penalty Fees: ${lateFee.ToString("F2")}\n" +
                                        $"Total Cost: ${(totalCost + lateFee).ToString("F2")}");
                }
            }
            Console.WriteLine(booking);
        }

        private static void ReturnByDelivery(Booking booking)
        {
            TimeSpan lateDuration = DateTime.Now - booking.EndDateAndTime;
            double minutesDiff = lateDuration.TotalMinutes;
            var totalCost = booking.BookingFee + booking.RoadSideFee + AdditionalLocationFee;

            Console.Write("Take note that returning to your desired location would cost additonal fees ($0.50).\n" +
                            "Continue? (Y/N)");
            string confirm = Console.ReadLine();
            if (confirm == "Y".ToLower())
            {
                Console.Write("Enter the postal code for delivery pickup: ");
                string pc = Console.ReadLine();

                // Input Validation: Ensure a 6-digit postal code is entered
                if (!int.TryParse(pc, out int postalCode) || pc.Length != 6)
                {
                    Console.WriteLine("Invalid postal code. Please enter a 6-digit number.");
                    ReturnByDelivery(booking); // Re-prompt for valid input
                    return;
                }
                Console.WriteLine($"Checking Location {pc}...");
                Thread.Sleep(2000); //simulate checking process
                Console.WriteLine("Location Verified!");
                // Start 15-minute timer

                Console.WriteLine("You have 15 minutes to return your car to the chosen location. Enter 'D' when returned.");
                string confirmation = Console.ReadLine();
                if (confirmation == "D".ToLower())
                {
                    booking.DropOffDetails = new Tuple<string, string>("Delivery", pc);
                    if (DateTime.Now - booking.EndDateAndTime <= ReturnBuffer)
                    {
                        booking.BookingFee += AdditionalLocationFee;
                        Console.WriteLine("Return is successful.");
                        Console.WriteLine($"Additonal/Penalty Fees: ${AdditionalLocationFee}\n" +
                                            $"Total Cost: ${totalCost.ToString("F2")}");
                    }
                    else
                    {
                        var lateFee = (lateDuration.TotalMinutes * LateFeePerMinute);
                        booking.BookingFee += lateFee + AdditionalLocationFee;
                        Console.WriteLine($"You are {minutesDiff.ToString("F2")} minutes late. A late fee of ${lateFee.ToString("F2")} will be applied.");
                        Console.WriteLine($"Additonal/Penalty Fees: ${(AdditionalLocationFee + lateFee).ToString("F2")}\n" +
                                            $"Total Cost: ${(totalCost + lateFee).ToString("F2")}");
                    }
                }



            }
            else if (confirm == "N".ToLower())
            {
                displayReturnLocations(booking);
            }
            Console.WriteLine(booking);
        }

        //use case begin
        static void Main(string[] args)
        {
            //test data
            iCarStation station1 = new iCarStation(1, "600123");
            iCarStation station2 = new iCarStation(2, "600234");
            Car car = new Car(1, "Honda Civic", 2021, 10000, new List<string>() { }, "Honda", 10);
            Booking booking1 = new Booking(1, new DateTime(2024, 8, 5, 9, 0, 0), new DateTime(2024, 8, 6, 9, 0, 0), new Tuple<string, string>("Deliver", "239085"), 20, 10);
            bookings.Add(booking1);
            carlist.Add(car);
            ReturnCar(booking1.BookingId, car.CarID);
        }
    }
}
