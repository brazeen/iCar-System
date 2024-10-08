﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
    class Review
    {
        private int reviewID;

        public int ReviewID { get { return reviewID; } set { reviewID = value; } }

        private int rating;

        public int Rating { get { return rating; } set { rating = value; } }

        private string description;

        public string Description { get { return description; } set { description = value; } }

        private DateTime reviewDateAndTime;

        public DateTime ReviewDateAndTime { get { return reviewDateAndTime; } set { reviewDateAndTime = value; } }

        private Car bookedCar;

        public Car BookedCar { get { return bookedCar; } set { bookedCar = value; } }

        private Renter renterOfReview;

        public Renter RenterOfReview { get { return renterOfReview; } set { renterOfReview = value; } }

        public Review() { }

        public Review(int rid, int r, string d)
        {
            reviewID = rid;
            rating = r;
            description = d;
            ReviewDateAndTime = DateTime.Now;
        }

        public void setCar(Car car) { BookedCar = car; }

        public void setRenter(Renter renter) { RenterOfReview = renter; }

        public Car getCar() { return BookedCar; }
    }
}
