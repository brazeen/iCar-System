using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    internal class Yangyi
    {
        static void Main(string[] args)
        {
            List<Booking> primeBookingList = new List<Booking>();
            List<Booking> regbookingList = new List<Booking>();
            Renter regRenter = new Renter(false,1, "Yangyi", "92338869", new DateTime(1985, 11, 23), "yangyi@gmail.com", "2 Clementi Road");
            Renter primeRenter = new Renter(true, 2, "Donovan", "92778840", new DateTime(2001, 12, 21), "donovan@gmail.com", "1 Clementi Road");
            Booking regBooking1 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 2, 2, 17, 0, 0), new Tuple<string, string>("Deliver", "239085"), 199.99, 100);
            Booking regBooking2 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 2, 2, 17, 0, 0), new Tuple<string, string>("Deliver", "239085"), 199.99, 100);
            Booking primeBooking1 = new Booking(2, new DateTime(2024, 4, 1, 10, 0, 0), new DateTime(2024, 5, 5, 15, 0, 0), new Tuple<string, string>("Deliver", "665432"), 100.99, 99);
            Booking primeBooking2 = new Booking(2, new DateTime(2024, 4, 1, 10, 0, 0), new DateTime(2024, 5, 5, 15, 0, 0), new Tuple<string, string>("Deliver", "665432"), 100.99, 99);

            primeBookingList.Append(primeBooking1);
            primeBookingList.Append(primeBooking2);
            regbookingList.Append(regBooking1);
            regbookingList.Append(regBooking2);

            Console.WriteLine("[1] regular renter");
            Console.WriteLine("[2] prime renter");
            Console.Write("choose renter:");
            string choice = Console.ReadLine();

            Renter selectedRenter = null;

            if (choice == "1")
            {
                selectedRenter = regRenter;
            }
            else if (choice == "2")
            {
                selectedRenter = primeRenter;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            if (selectedRenter != null)
            {
                Console.WriteLine($"Selected Renter: {selectedRenter.Name}, Email: {selectedRenter.Email}");
            }

            double totalBookingFee = 0;
            double totalRoadSideFee = 0;
            double totalPayment = 0;
            foreach (Booking bookings in primeBookingList)
            {
                totalBookingFee += bookings.BookingFee;
                totalRoadSideFee += bookings.RoadSideFee;
                totalPayment += totalRoadSideFee + totalBookingFee;
            }
            Console.WriteLine("Total payment:", totalPayment);
            Console.WriteLine("Choose payment method:");
            Console.WriteLine("[1] Debit card");
            Console.WriteLine("[2] Credit card");
            Console.WriteLine("[3] Digital wallet");
            string option = Console.ReadLine();
            string paymentMethod = "";
            if (option == "1")
            {
                paymentMethod = "Debit Card";
            }
            else if (option == "2")
            {
                paymentMethod = "Credit card";
            }
            else if (option == "3")
            {
                paymentMethod = "Digital wallet";
            }
            if (selectedRenter.IsPrime == true )
            {
                Console.WriteLine("Prime renter detected.");
                if ( totalPayment > 300 )
                {
                    MonthlyPayment monthlyPayment = new MonthlyPayment(1, DateTime.Now, 0, paymentMethod);
                    double discountedAmount = monthlyPayment.ApplyDiscount(totalBookingFee, totalRoadSideFee);
                    Console.WriteLine("Total Payment more than minimum requirement (300 dollars)");
                    Console.WriteLine("Discount applied");
                    Console.WriteLine("Discounted payment:", discountedAmount);
                }
            }
        }
    }
}
