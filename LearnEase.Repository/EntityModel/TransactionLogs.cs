using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class TransactionLogs
    {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
		public string PlanType { get; set; }
		public int Amount { get; set; }
		public long OrderCode { get; set; }
		public string PayOSOrderId { get; set; }
		public string Status { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
