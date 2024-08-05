using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Donovan
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing donovan");
            DateTime currentTime = DateTime.Now;
            //test data
            iCarStation station1 = new iCarStation(1, 600123);
            iCarStation station2 = new iCarStation(2, 600234);
            DateTime startTime = new DateTime(2024, 1, 1, 9, 0, 0);
            DateTime endTime = new DateTime(2024, 1, 1, 11, 0, 0);
            //Booking testbooking1 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 8, 4, 21, 0, 0), "600123", "600123", 20.00);
            Booking booking1 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 2, 2, 17, 0, 0), new Tuple<string, string>("Deliver", "239085"), new Tuple<string, string>("Deliver", "345676"), 20, 10);
            var addFees = 0.50;
            var bookingCost = 20; //assuming a 2hr session costs a standard of $20
            var totalCost = bookingCost + addFees;
            var LateFeePerMinute = 0.10;
            double returnBuffer = 15;
            TimeSpan lateDuration = currentTime - booking1.EndDateAndTime;
            double minutesDiff = lateDuration.TotalMinutes;

            void displayReturnLocations()
            {
                Console.WriteLine("Where do you want to return your car?\n" +
                    "[1] iCar Station\n" +
                    "[2] Desired Location");
                int option = Convert.ToInt32(Console.ReadLine());
            }

            void returnMenu()
            {
                while (true)
                {
                    Console.WriteLine("Where do you want to return your car?\n" +
                    "[1] iCar Station\n" +
                    "[2] Desired Location");
                    int option = Convert.ToInt32(Console.ReadLine());

                    if (option == 1)
                    {
                        Console.WriteLine($"Select an iCar station:\n" +
                            $"[1]{station1.PostalCode}\n" +
                            $"[2]{station2.PostalCode}");
                        int stationSelect = Convert.ToInt32(Console.ReadLine());

                        if (stationSelect == 1)
                        {
                            Console.WriteLine($"iCar Station{station1.StationID} has been selected. 15 Minutes is given for you to return vehicle to location.\n" +
                                "Enter 'D' when returned.");
                            string confirmation = Console.ReadLine();
                            if (confirmation == "D".ToLower())
                            {

                                if (minutesDiff <= returnBuffer)
                                {
                                    Console.WriteLine("Return is successful.");
                                    Console.WriteLine();
                                    Console.WriteLine($"Additonal/Penalty Fees: {addFees}\n" +
                                        $"Total Cost: {totalCost}");

                                }
                                else
                                {
                                    if (minutesDiff > returnBuffer)
                                    {
                                        var lateFee = (lateDuration.TotalMinutes * LateFeePerMinute);
                                        booking1.BookingFee += lateFee;
                                        Console.WriteLine($"You are {minutesDiff.ToString("F2")} minutes late. A late fee of ${lateFee.ToString("00.00")} will be applied.");
                                    }
                                    Console.WriteLine($"Returned Location: {booking1.DropOffDetails}\n" +
                                    $"Additional/Penalty Fees: {booking1.BookingFee.ToString("00.00")}\n" +
                                    $"Total Cost: {booking1.BookingFee.ToString("00.00")}");
                                    break;
                                }
                            }
                        }
                        else if (stationSelect == 2)
                        {
                            Console.WriteLine($"iCar Station{station2.StationID} has been selected. 15 Minutes is given for you to return vehicle to location.\n" +
                                "Enter 'D' when returned.");
                            string confirmation = Console.ReadLine();
                            if (confirmation == "D".ToLower())
                            {
                                Console.WriteLine("Return is successful.");
                                Console.WriteLine();
                                Console.WriteLine($"Additonal/Penalty Fees: {addFees}\n" +
                                    $"Total Cost: {totalCost}");
                            }
                        }
                    }
                    else if (option == 2) //desired location
                    {
                        Console.Write("Take note that returning to your desired location would cost additonal fees ($0.50).\n" +
                            "Continue? (Y/N)");
                        string confirm = Console.ReadLine();
                        if (confirm == "Y".ToLower())
                        {
                            booking1.BookingFee += addFees;
                            Console.Write("Please enter the postal code of your desired location:");
                            int postalCode = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine($"Checking location: {postalCode}");
                            Thread.Sleep(3000); //approving location
                            Console.WriteLine("Location Approved. 15 Minutes is given for you to return vehicle to location.\n" +
                                "Enter 'D' when returned.");
                            string returnConfirm = Console.ReadLine();
                            if (returnConfirm == "D".ToLower())
                            {
                                if (minutesDiff <= returnBuffer)
                                {
                                    Console.WriteLine("Return is successful.");
                                    Console.WriteLine();
                                    Console.WriteLine($"Returned Location:{station1.PostalCode}\n" +
                                        $"Additonal/Penalty Fees: {addFees}\n" +
                                        $"Total Cost: {totalCost}");
                                    break;
                                }
                                else
                                {
                                    if (minutesDiff > returnBuffer)
                                    {
                                        var lateFee = (lateDuration.TotalMinutes * LateFeePerMinute);
                                        booking1.BookingFee += lateFee;
                                        Console.WriteLine($"You are {minutesDiff.ToString("F2")} minutes late. A late fee of ${lateFee.ToString("00.00")} will be applied.");
                                    }
                                    Console.WriteLine($"Returned Location: {booking1.DropOffDetails}\n" +
                                    $"Additional/Penalty Fees: {booking1.BookingFee.ToString("00.00")}\n" +
                                    $"Total Cost: {booking1.BookingFee.ToString("00.00")}");
                                    break;
                                }
                            }
                        }
                        else if (confirm == "N".ToLower())
                        {
                            return;
                        }

                    }
                }

            }
            returnMenu();
        }
    }
}
