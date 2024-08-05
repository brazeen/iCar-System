using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Brandon
    {
        static void Main(string[] args)
        {
            //control methods
            bool validateReview(int rating, string description)
            {
                if ((rating < 1 || rating > 5) || (description.Length > 200))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            bool hasReviewed(Renter renter, Car car)
            {
                if (renter.hasReviewed(car))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //test data
            Renter loggedInRenter = new Renter(1, "Brandon Koh", "99345673", new DateTime(1985, 11, 23), "brandon@gmail.com", "2 Clementi Road");
            Booking testBooking1 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 2, 2, 17, 0, 0), "123 Main St", "456 Elm St", 199.99, 100);
            Booking testBooking2 = new Booking(2, new DateTime(2024, 4, 1, 10, 0, 0), new DateTime(2024, 5, 5, 15, 0, 0), "789 Pine St", "123 Main St", 100.99, 99);
            List<DateTime> schedule1 = new List<DateTime>
            {
                new DateTime(2024, 8, 10, 9, 0, 0),
                new DateTime(2024, 8, 17, 17, 0, 0)
            };

            List<DateTime> schedule2 = new List<DateTime>
            {
                new DateTime(2024, 9, 1, 10, 0, 0),
                new DateTime(2024, 9, 5, 15, 0, 0)
            };

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

            Car car1 = new Car(1, "Toyota Camry", 2022, 5000, photos1, "Toyota", 29.99, schedule1);
            Car car2 = new Car(2, "Honda Civic", 2021, 10000, photos2, "Honda", 24.99, schedule2);
            testBooking1.setCar(car1);
            testBooking2.setCar(car2);
            loggedInRenter.Bookings.Add(testBooking1);
            loggedInRenter.Bookings.Add(testBooking2);







            //use case start
            List<Booking> bookingHistory = loggedInRenter.getBookingHistory();
            string bookingDetails;
            foreach (Booking booking in bookingHistory)
            {
                bookingDetails = $"Booking ID: {booking.BookingId}\n" +
                                                $"Start Date and Time: {booking.StartDateAndTime}\n" +
                                                $"End Date and Time: {booking.EndDateAndTime}\n" +
                                                $"Pickup Location: {booking.PickupLocation}\n" +
                                                $"Return Location: {booking.ReturnLocation}\n" +
                                                $"Total Cost: ${booking.BookingFee+booking.RoadSideFee}\n\n";

                Console.WriteLine(bookingDetails);
            }

            //for the loop to run at least once
            bool pastReview = true;
            //variable to track if option is in range (actual use case will be in a GUI so will not have that logic)
            bool foundbookingid = false; 
            Car car = new Car();
            while (pastReview == true)
            {
                Console.Write("Select a booking ID to view: ");
                int inputbookingid = Convert.ToInt32(Console.ReadLine());
                foreach (Booking booking in bookingHistory)
                {
                    if (booking.BookingID == inputbookingid)
                    {
                        foundbookingid = true;
                        car = booking.getCarInBooking();
                    }
                }
                if (car.CarID != null)
                {
                    string carDetails = "Car Details:\n" +
                                                $"Model: {car.Model}\n" +
                                                $"Year: {car.Year}\n" +
                                                $"Make: {car.Make}\n" +
                                                $"Mileage: {car.Mileage} kilometers\n" +
                                                $"Rate: ${car.Rate} per hour\n";
                    Console.WriteLine(carDetails);
                    Console.WriteLine("1: Make Review, 2: Exit");
                    Console.Write("Enter option: ");
                    int option = Convert.ToInt32(Console.ReadLine());
                    bool inrangeoption = false;
                    while (inrangeoption == false)
                    {
                        if (option == 1)
                        {
                            inrangeoption = true;
                            pastReview = hasReviewed(loggedInRenter, car);
                            if (pastReview)
                            {
                                //display error message
                                Console.WriteLine("A review has already been made for this car.");
                            }
                            else
                            {
                                pastReview = false;
                            }
                        }
                        else if (option == 2)
                        {
                            inrangeoption = true;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option");
                        }
                    }
                }
                if (foundbookingid == false)
                {
                    Console.WriteLine("Booking ID not found.");
                }

            }

            bool valid = false;
            int rating = 0;
            string description = "";
            while (valid == false)
            {
                Console.Write("Enter rating from 1 to 5: ");
                rating = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter description (max 200 characters): ");
                description = Console.ReadLine();
                valid = validateReview(rating, description);
                if (valid == false)
                {
                    //display error message
                    Console.WriteLine("Invalid rating and description. Please re-enter.");
                }
            }

            //create review
            Review review = new Review(1, rating, description); // to change later as this ID is hardcode
            review.setRenter(loggedInRenter);
            review.setCar(car);
            loggedInRenter.addReview(review);
            car.addReview(review);
            Console.WriteLine("Review created successfully.");
        }
    }
}


