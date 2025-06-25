using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
	public class SubscriptionResponse
	{
		public string PlanType { get; set; }
		public int Price { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool Active => EndDate >= DateTime.UtcNow;
	}

}
