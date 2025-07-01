using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Request
{
	public class PayOSWebhookRequest
	{
		public long orderCode { get; set; }
		public long? transactionId { get; set; }
		public int amount { get; set; }
		public string description { get; set; }
		public string status { get; set; } // SUCCESS/FAILED
	}

}
