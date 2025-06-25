using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
	public class PayOSResponse
	{
		public string code { get; set; }
		public string desc { get; set; }
		public PayOSResponseData data { get; set; }
	}

	public class PayOSResponseData
	{
		public string checkoutUrl { get; set; }
	}
}
