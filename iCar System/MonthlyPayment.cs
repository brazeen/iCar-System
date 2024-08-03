using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
	class MonthlyPayment
	{
		private int paymentID;

		public int PaymentID { get { return paymentID; } set { paymentID = value; } }

        private DateTime paymentDate;

		public DateTime PaymentDate { get { return paymentDate; } set { paymentDate = value; } }

        private double paymentAmount;

		public double PaymentAmount { get { return paymentAmount; } set { paymentAmount = value; } }

        private string paymentMethod;

		public string PaymentMethod { get { return paymentMethod; } set { paymentMethod = value; } }

        private List<Booking> bookingList;

		//constructor
		public MonthlyPayment() { }

		public MonthlyPayment(int pid, DateTime pd, double pa, string pm)
		{
			PaymentID = pid;
			PaymentDate = pd;
			PaymentAmount = pa;
			PaymentMethod = pm;
			bookingList = new List<Booking>();
		}
	}
}
