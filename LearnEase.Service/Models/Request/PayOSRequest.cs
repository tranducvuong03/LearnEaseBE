using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Request
{
	public class PayOSRequest
	{
		[JsonProperty("orderCode")]
		public long orderCode { get; set; }

		[JsonProperty("amount")]
		public int amount { get; set; }

		[JsonProperty("description")]
		public string description { get; set; }

		[JsonProperty("returnUrl")]
		public string returnUrl { get; set; }

		[JsonProperty("cancelUrl")]
		public string cancelUrl { get; set; }

		[JsonProperty("signature")]
		public string signature { get; set; }
	}



}
