using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCar_System
{
	public class MonthlyPayment
	{
		private int paymentID;

		public int PaymentID { get; set; }

		//meow meow mmeow meow

		private DateTime paymentDate;

		public DateTime PaymentDate { get; set; }

		private double paymentAmount;

		public double PaymentAmount { get; set; }

		private string paymentMethod;

		public string PaymentMethod { get; set; }

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
