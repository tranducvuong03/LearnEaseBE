﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.IServices
{
	public interface IPayOSService
	{
		Task<string> CreatePaymentUrlAsync(string planType, Guid userId);
	}

}
