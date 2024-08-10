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


            //test data------------------------------------------------------------------------------------------------------------
            Renter testrenter = new Renter(1, "Brandon Koh", "99345673", new DateTime(1985, 11, 23), "brandon@gmail.com", "2 Clementi Road");
            Booking testBooking1 = new Booking(1, new DateTime(2024, 1, 1, 9, 0, 0), new DateTime(2024, 1, 1, 17, 0, 0), new Tuple<string, string>("Deliver", "239085"), 10);
            Booking testBooking2 = new Booking(2, new DateTime(2024, 4, 4, 10, 0, 0), new DateTime(2024, 4, 5, 15, 0, 0), new Tuple<string, string>("Deliver", "665432"), 15);

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

            Car car1 = new Car(1, "Camry", 2022, 5000, photos1, "Toyota", 29.99);
            Car car2 = new Car(2, "Civic Type R", 2021, 10000, photos2, "Honda", 24.99);
            testBooking1.setCar(car1);
            testBooking2.setCar(car2);
            testrenter.Bookings.Add(testBooking1);
            testrenter.Bookings.Add(testBooking2);

            //the car in testbooking2 will have a previous review for simulation + testing purposes
            Review testreview = new Review(1, 4, "nice car");
            testreview.setRenter(testrenter);
            testreview.setCar(car2);
            testrenter.addReview(testreview);
            car2.addReview(testreview);

            //control methods------------------------------------------------------------------------------------------------------
            bool validateReview(string rating, string description)
            {
                try
                {
                    if ((Convert.ToInt32(rating) < 1 || Convert.ToInt32(rating) > 5) || (description.Length > 200))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
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

            List<Booking> getRenterBookingHistory(Renter r)
            {
                return r.getBookingHistory();
            }

            Car selectCarInBooking(List<Booking> bookingHistory)
            {
                while (true)
                {
                    //all this validation logic will not be in the actual iCar system as that would be done through a GUI
                    Console.Write("Select a booking ID to view: ");
                    int? inputbookingid;
                    try
                    {
                        inputbookingid = Convert.ToInt32(Console.ReadLine());
                        foreach (Booking booking in bookingHistory)
                        {
                            if (booking.BookingId == inputbookingid)
                            {
                                Car car = booking.getCarInBooking();
                                return car;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter a valid booking ID.");
                    }
                    Console.WriteLine("Booking not found. Please re-input the booking ID.");

                }

            }
            //UI methods-------------------------------------------------------------------------------------------------------------
            //for UI methods, I only included the self message methods, as the rest of the methods (coming from the Renter actor to the UI) would be handled by the control layer
            Renter findLoggedInRenter()
            {
                //to simulate finding a logged in renter, return the test renter object 
                return testrenter;
            }
 
            void displayBooking(Booking booking)
            {
                string bookingDetails = $"Booking ID: {booking.BookingId}\n" +
                                                $"Start Date and Time: {booking.StartDateAndTime}\n" +
                                                $"End Date and Time: {booking.EndDateAndTime}\n" +
                                                $"Pickup Location: {booking.PickUpDetails}\n" +
                                                $"Return Location: {booking.DropOffDetails}\n" +
                                                $"Total Cost: ${(booking.BookingFee + booking.RoadSideFee).ToString("F2")}\n\n";

                Console.WriteLine(bookingDetails);
            }

            void displayCarDetails(Car car)
            {
                string carDetails = "Car Details:\n" +
                                                $"Model: {car.Model}\n" +
                                                $"Year: {car.Year}\n" +
                                                $"Make: {car.Make}\n" +
                                                $"Mileage: {car.Mileage} kilometers\n" +
                                                $"Rate: ${car.Rate} per hour\n";
                Console.WriteLine(carDetails);
            }

            void displayErrorMessage(string error)
            {
                Console.WriteLine(error);
            }

            void displayConfirmationPopup(Review r)
            {
                string reviewDetails = "\nReview Details:\n" +
                                                $"Car: {r.BookedCar.Year + " " + r.BookedCar.Make + " " + r.BookedCar.Model}\n" +
                                                $"Rating: {r.Rating}\n" +
                                                $"Description: {r.Description}\n" +
                                                "Review created successfully.\n";
                Console.WriteLine(reviewDetails);
            }


            //use case start-----------------------------------------------------------------------------------------------------------
            Renter loggedInRenter = findLoggedInRenter();
            List<Booking> bookingHistory = getRenterBookingHistory(loggedInRenter);
            foreach (Booking booking in bookingHistory)
            {
                displayBooking(booking);
            }

            //for the loop to run at least once
            bool pastReview = true;
            //car has to be defined first as defining it in the loop would cause the createReview method to have a null car variable
            Car car = new Car();
            while (pastReview == true)
            {
                car = selectCarInBooking(bookingHistory);
                displayCarDetails(car);
                Console.WriteLine("1: Make Review, 2: Exit");
                //option and option validation will not be in actual iCar system as it will be a GUI based system)
                int? option;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter option: ");
                        option = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter a valid option.");
                    }
                    
                }
                
                //variable to track if option is in range (actual use case will be in a GUI so will not have that logic)
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
                            displayErrorMessage("A review has already been made for this car.");
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

            bool valid = false;
            string strrating = "";
            string description = "";
            int rating = 0;
            while (valid == false)
            {
                //displayInputFields +inputReview method would work differently in a GUI as both fields can be displayed together without getting input first
                Console.Write("Enter rating from 1 to 5: ");
                strrating = Console.ReadLine();
                Console.Write("Enter description (max 200 characters): ");
                description = Console.ReadLine();
                valid = validateReview(strrating, description);
                if (valid == false)
                {
                    //display error message
                    Console.WriteLine("Invalid rating and description. Please re-enter.");
                }
                else
                {
                    rating = Convert.ToInt32(strrating);
                }
            }

            //createReview method in sequence diagram is represented by using the constructor to create a new review in line 191
            Review newReview = new Review(1, rating, description); 
            newReview.setRenter(loggedInRenter);
            newReview.setCar(car);
            loggedInRenter.addReview(newReview);
            car.addReview(newReview);
            displayConfirmationPopup(newReview);
        }
    }
}


