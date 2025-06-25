using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
	public class Subscription
	{
		[Key]
		public Guid SubscriptionId { get; set; } = Guid.NewGuid();

		[Required]
		public Guid UserId { get; set; }

		[Required]
		[MaxLength(20)]
		public string PlanType { get; set; } // "monthly" or "yearly"

		[Required]
		public int Price { get; set; }

		public DateTime StartDate { get; set; } = DateTime.UtcNow;

		public DateTime EndDate { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Navigation property
		public User User { get; set; }
	}
}
