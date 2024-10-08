﻿using System;
using System.Collections.Generic;

namespace iCar_System
{
    internal class Yangyi
    {
        static void Main(string[] args)
        {
            //prime booking list
            List<Booking> primeBookingList = new List<Booking>();
            //regular booking list
            List<Booking> regBookingList = new List<Booking>();

            Renter regRenter = new Renter(false, 1, "Yangyi", "92338869", new DateTime(1985, 11, 23), "yangyi@gmail.com", "2 Clementi Road");
            Renter primeRenter = new Renter(true, 2, "Donovan", "92778840", new DateTime(2001, 12, 21), "donovan@gmail.com", "1 Clementi Road");

            Booking regBooking1 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 2, 2, 17, 0, 0),
                new Tuple<string, string>("Deliver", "239085"), 100);
            Booking regBooking2 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 2, 2, 17, 0, 0),
                new Tuple<string, string>("Deliver", "239085"), 100);

            regBooking1.BookingFee = 50;
            regBooking2.BookingFee = 50;   

            Booking primeBooking1 = new Booking(2, new DateTime(2024, 4, 1, 10, 0, 0), new DateTime(2024, 5, 5, 15, 0, 0),
                new Tuple<string, string>("Deliver", "665432"), 120);
            Booking primeBooking2 = new Booking(2, new DateTime(2024, 4, 1, 10, 0, 0), new DateTime(2024, 5, 5, 15, 0, 0),
                new Tuple<string, string>("Deliver", "665432"), 120);

            primeBooking1.BookingFee = 100;
            primeBooking2.BookingFee = 100;

            primeBookingList.Add(primeBooking1);
            primeBookingList.Add(primeBooking2);
            regBookingList.Add(regBooking1);
            regBookingList.Add(regBooking2);

            //method to makePayment object
            MonthlyPayment makePayment(string paymentMethod, double totalPayment)
            {
                MonthlyPayment payment = new MonthlyPayment(1, DateTime.Now, totalPayment, paymentMethod);
                return payment;
            }

            //method to prompt for card details during payment
            bool promptForCard(MonthlyPayment payment)
            {
                Console.Write("Enter card number: ");
                string cardNumber = Console.ReadLine();

                Console.Write("Enter expiry date (mm/yy): ");
                string expiryDateInput = Console.ReadLine();
                DateTime expiryDate;
                //formatting datetime to MM/yy format
                bool isValidExpiryDate = DateTime.TryParseExact(expiryDateInput, "MM/yy", null, System.Globalization.DateTimeStyles.None, out expiryDate);

                if (!isValidExpiryDate)
                {
                    Console.WriteLine("Invalid date format. Please enter the date in mm/yy format.");
                    return false;
                }

                Console.Write("Enter CVC: ");
                if (!int.TryParse(Console.ReadLine(), out int cvc))
                {
                    Console.WriteLine("Invalid CVC. Please enter a numeric value.");
                    return false;
                }

                //call method from class to validate card credentials
                bool validateCard = payment.ValidateCardCredentials(cardNumber, expiryDate, cvc);
                return validateCard;
            }

            //method to prompt for payment method (debit, credit or digital wallet)
            string promptForPaymentMethod()
            {
                Console.WriteLine("Choose payment method:");
                Console.WriteLine("[1] Debit card");
                Console.WriteLine("[2] Credit card");
                Console.WriteLine("[3] Digital wallet");

                string option = Console.ReadLine();
                string paymentMethod = option switch
                {
                    "1" => "Debit Card",
                    "2" => "Credit Card",
                    "3" => "Digital Wallet",
                    _ => "Unknown"
                };
                return paymentMethod;
            }

            //method to confirm payment
            string confirmPayment()
            {
                Console.WriteLine("Confirm payment? [Y/N]");
                string confirm = Console.ReadLine();
                return confirm;
            }

            bool checkForPrime(Renter selectedRenter)
            {
                return selectedRenter.IsPrime;
            }

            void displayPayment(double totalPayment, double totalBookingFee, double totalRoadSideFee) {
                Console.WriteLine($"Total Booking Fee: ${totalBookingFee}");
                Console.WriteLine($"Total Roadside Fee: ${totalRoadSideFee}");
                Console.WriteLine($"Total Payment: ${totalPayment:F2}");
                Console.WriteLine();
            }

            void displayDiscountedPayment(MonthlyPayment monthlyPayment) {
                Console.WriteLine("Total Payment exceeds the minimum requirement (300 dollars).");
                Console.WriteLine("Discount applied.");
                Console.WriteLine($"Total payment after discount: ${monthlyPayment.PaymentAmount:F2}");
            }

            void displayMessage(MonthlyPayment monthlyPayment)
            {
                Console.WriteLine($"${monthlyPayment.PaymentAmount:F2} successfully deducted from {monthlyPayment.PaymentMethod}");
            }



            //Use case starts here
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("[1] regular renter");
                Console.WriteLine("[2] prime renter");
                Console.WriteLine("[3] exit");
                Console.Write("Choose renter: ");
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
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }

                double totalBookingFee = 0;
                double totalRoadSideFee = 0;
                bool checkRenter = checkForPrime(selectedRenter);
                if (selectedRenter != null && checkRenter)
                {
                    foreach (Booking booking in primeBookingList)
                    {
                        totalBookingFee += booking.BookingFee;
                        totalRoadSideFee += booking.RoadSideFee;
                    }

                    double totalPayment = totalBookingFee + totalRoadSideFee;
                    displayPayment(totalPayment, totalBookingFee, totalRoadSideFee);

                    if (totalPayment > 300)
                    {
                        string paymentMethod = promptForPaymentMethod();
                        MonthlyPayment monthlyPayment = makePayment(paymentMethod, totalPayment);
                        bool valid = promptForCard(monthlyPayment);
                        while (valid == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please renter credentials.");
                            valid = promptForCard(monthlyPayment);
                        }
                        double discountedAmount = monthlyPayment.ApplyDiscount(totalBookingFee, totalRoadSideFee);
                        monthlyPayment.UpdateAmount(discountedAmount);
                        displayDiscountedPayment(monthlyPayment);
                        string confirm = confirmPayment();
                        if (confirm == "Y")
                        {
                            displayMessage(monthlyPayment);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else if (selectedRenter != null && !checkRenter)
                {
                    foreach (Booking booking in regBookingList)
                    {
                        totalBookingFee += booking.BookingFee;
                        totalRoadSideFee += booking.RoadSideFee;
                    }

                    double totalPayment = totalBookingFee + totalRoadSideFee;

                    displayPayment(totalPayment, totalBookingFee, totalRoadSideFee);
                    string paymentMethod = promptForPaymentMethod();
                    MonthlyPayment monthlyPayment = makePayment(paymentMethod, totalPayment);
                    monthlyPayment.UpdateAmount(totalPayment);
                    bool valid = promptForCard(monthlyPayment);
                    while (valid == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter credentials again.");
                        valid = promptForCard(monthlyPayment);
                    }
                    string confirm = confirmPayment();
                    if (confirm == "Y")
                    {
                        displayMessage(monthlyPayment);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            
        }
    }
}