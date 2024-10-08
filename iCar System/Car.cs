﻿using System;
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

        private List<Dictionary<string, DateTime>> schedule;

        public List<Dictionary<string, DateTime>> Schedule { get { return schedule; } set { schedule = value; } }

        private List<Review> reviews;

        public List<Review> Reviews { get { return reviews; } set { reviews = value; } }

        private List<Booking> reservations;

        public List<Booking> Reservations { get { return reservations; } set { reservations = value; } }

        private Owner carOwner;

        public Owner CarOwner { get { return carOwner; } set { carOwner = value; } }

        private Insurance insurance;

        public Insurance Insurance { get { return insurance; } set { insurance = value; } }

        private List<MaintenanceRecord> maintenanceRecords;

        public List<MaintenanceRecord> MaintenanceRecords { get { return maintenanceRecords; } set { maintenanceRecords = value; } }

        //constructor
        public Car() { }

        public Car(int id, string mdl, int yr, int mil, List<string> ph, string mk, double rt)
        {
            CarID = id;
            Model = mdl;
            Year = yr;
            Mileage = mil;
            Photos = ph;
            Make = mk;
            Rate = rt;
            Reviews = new List<Review>();
            Schedule = new List<Dictionary<string, DateTime>>();
            Reservations = new List<Booking>();

        }

        public void addReview(Review review)
        {
            Reviews.Add(review);
        }

        public void addUnavailabilityPeriod(DateTime start, DateTime end)
        {
            Dictionary<string, DateTime> unavailabilityPeriod = new Dictionary<string, DateTime>();
            unavailabilityPeriod.Add("startDateAndTime", start);
            unavailabilityPeriod.Add("endDateAndTime", end);
            Schedule.Add(unavailabilityPeriod);
        }
        public void addBooking(Booking booking)
        {
            Reservations.Add(booking);
        }

        public List<Booking> getReservations()
        {
            return Reservations;
        }

        public List<Dictionary<string,DateTime>> getAvailabilitySchedule()
        { 
            return Schedule;
        }

        public bool IsAvailable(DateTime startDate, DateTime endDate)
        {
            foreach (var booking in Reservations)
            {
                if ((startDate < booking.EndDateAndTime) && (endDate > booking.StartDateAndTime))
                {
                    return false;
                }
            }
            foreach (var period in Schedule)
            {
                if ((startDate < period["endDateAndTime"]) && (endDate > period["startDateAndTime"]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}