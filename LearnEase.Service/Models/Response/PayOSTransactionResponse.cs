using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
    public class PayOSTransactionResponse
    {
		public string TransactionId { get; set; }
		public long Amount { get; set; }
		public string Status { get; set; }
		public long CreatedAt { get; set; }
		public string Description { get; set; }
	}
}
