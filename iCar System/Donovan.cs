﻿using System;
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
        private static void ReturnCar(int bookingid)
        {
            Booking booking = bookings.FirstOrDefault(b => b.BookingId == bookingid); //check for bookingID
            if (booking == null)
            {
                Console.WriteLine("Invalid booking ID.");
                return;
            }
            Car car = booking.getCarInBooking();
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

            // Time check for late return

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

            Console.Write("Take note that returning from your desired location would cost additonal fees ($0.50).\n" +
                            "Continue? (Y/N)");
            string confirm = Console.ReadLine();
            if (confirm == "Y".ToLower())
            {
                Console.Write("Enter the postal code where your vehicle will be for delivery pickup: ");
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
                // Time check for late return

                Console.WriteLine("You have 15 minutes to return your car to the chosen location before delivery pickup service arrives. Enter 'D' when returned.");
                string confirmation = Console.ReadLine();
                if (confirmation == "D".ToLower())
                {
                    booking.DropOffDetails = new Tuple<string, string>("Deliver", pc);
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

                Console.WriteLine(booking);

            }
            else if (confirm == "N".ToLower())
            {
                displayReturnLocations(booking);
            }
            
        }

        //use case begin
        static void Main(string[] args)
        {
            //test data
            iCarStation station1 = new iCarStation(1, "600123");
            iCarStation station2 = new iCarStation(2, "600234");
            Car car = new Car(1, "Honda Civic", 2021, 10000, new List<string>() { }, "Honda", 10);
            //change EndDateandTime to 15 minutes before current time for testing of late return
            //e.g Current Time is 9.15am. Set EndDateandTime to 9am. No late fee should be given.
            //If current time is 9.16am, EndDateandTime is 9am. 15Minutes buffer time to return car is over, and late fee is applied accordingly.
            Booking booking1 = new Booking(1, new DateTime(2024, 8, 5, 9, 0, 0), new DateTime(2024, 8, 6, 9, 0, 0), new Tuple<string, string>("Deliver", "239085"), 10);
            booking1.setCar(car);
            bookings.Add(booking1);
            carlist.Add(car);
            ReturnCar(booking1.BookingId);
        }
    }
}
