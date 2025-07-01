using LearnEase.Service.IServices;
using LearnEase.Service.Models.Request;
using LearnEase.Service.Models.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Services
{
	public class PayOSService : IPayOSService
	{
		private readonly IConfiguration _config;
		private readonly HttpClient _http;

		public PayOSService(IConfiguration config)
		{
			_config = config;
			_http = new HttpClient();
			_http.DefaultRequestHeaders.Add("x-client-id", _config["PayOS:ClientId"]);
			_http.DefaultRequestHeaders.Add("x-api-key", _config["PayOS:ApiKey"]);
		}

		public async Task<string> CreatePaymentUrlAsync(string planType, Guid userId)
		{
			var orderCode = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			var amount = planType == "yearly" ? 397800 : 39000;
			var userShort = userId.ToString().Substring(0, 8);
			var description = $"Sub-{planType}-{userShort}";
			var returnUrl = _config["PayOS:ReturnUrl"];
			var cancelUrl = _config["PayOS:CancelUrl"];
			var checksumKey = _config["PayOS:ChecksumKey"];

			// Chuỗi cần ký
			var rawSignatureData = $"amount={amount}&cancelUrl={cancelUrl}&description={description}&orderCode={orderCode}&returnUrl={returnUrl}";

			// Tạo chữ ký (signature)
			string signature;
			using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey)))
			{
				byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawSignatureData));
				signature = BitConverter.ToString(hash).Replace("-", "").ToLower();
			}

			// Log debug
			Console.WriteLine("=== PayOS - DEBUG LOGS ===");
			Console.WriteLine($"[Amount] {amount}");
			Console.WriteLine($"[OrderCode] {orderCode}");
			Console.WriteLine($"[Description] {description}");
			Console.WriteLine($"[ReturnUrl] {returnUrl}");
			Console.WriteLine($"[CancelUrl] {cancelUrl}");
			Console.WriteLine($"[Raw Signature Data] {rawSignatureData}");
			Console.WriteLine($"[Generated Signature] {signature}");

			// Tạo request
			var request = new PayOSRequest
			{
				orderCode = orderCode,
				amount = amount,
				description = description,
				returnUrl = returnUrl,
				cancelUrl = cancelUrl,
				signature = signature
			};

			var json = JsonConvert.SerializeObject(request);
			Console.WriteLine($"[Final Request JSON] {json}");

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			// Gửi yêu cầu
			var response = await _http.PostAsync(_config["PayOS:CreateUrl"], content);
			var body = await response.Content.ReadAsStringAsync();

			Console.WriteLine($"[PayOS Response StatusCode] {response.StatusCode}");
			Console.WriteLine($"[PayOS Response Body] {body}");

			if (!response.IsSuccessStatusCode)
				throw new Exception($"PayOS Error: {body}");

			var result = JsonConvert.DeserializeObject<PayOSResponse>(body);
			var checkoutUrl = result?.data?.checkoutUrl;

			Console.WriteLine($"[Parsed Checkout URL] {checkoutUrl}");

			if (!response.IsSuccessStatusCode || checkoutUrl == null)
				throw new Exception($"PayOS Error: {body}");

			return checkoutUrl;

		}


	}


}
