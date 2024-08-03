using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Car
    {
        private int carID;

        public int CarID { get { return carID; } set { carID = value; } }

        private string model;

        public string Model { get { return model; } set { model = value; } }

        private int year;

        public int Year { get { return year; } set { year = value; } }

        private int mileage;

        public int Mileage { get { return mileage; } set { mileage = value; } }

        private List<string> photos;

        public List<string> Photos { get { return photos; } set { photos = value; } }

        private string make;

        public string Make { get { return make; } set { make = value; } }

        private double rate;

        public double Rate { get { return rate; } set { rate = value; } }

        private List<DateTime> schedule;

        public List<DateTime> Schedule { get { return schedule; } set { schedule = value; } }

        private List<Review> reviews;

        public List<Review> Reviews { get { return reviews; } set { reviews = value; } }

        //constructor
        public Car() { }

        public Car(int id, string mdl, int yr, int mil, List<string> ph, string mk, double rt, List<DateTime> sch)
        {
            CarID = id;
            Model = mdl;
            Year = yr;
            Mileage = mil;
            Photos = ph;
            Make = mk;
            Rate = rt;
            Schedule = sch;
            Reviews = new List<Review>();

        }

        public void addReview(Review review)
        {
            Reviews.Add(review);
        }
    }
}
