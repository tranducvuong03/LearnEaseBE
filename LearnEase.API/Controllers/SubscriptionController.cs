using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Request;
using LearnEase.Service.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearnEase.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class SubscriptionController : ControllerBase
	{
		private readonly LearnEaseContext _context;
		private readonly IPayOSService _payOSService;

		public SubscriptionController(LearnEaseContext context, IPayOSService payOSService)
		{
			_context = context;
			_payOSService = payOSService;
		}

		[Authorize]
		[HttpPost("pay")]
		public async Task<IActionResult> CreatePayment([FromBody] SubscriptionRequest request)
		{
			var userId = GetUserIdFromToken();

			if (request.PlanType != "monthly" && request.PlanType != "yearly")
				return BadRequest("Invalid plan type.");

			var url = await _payOSService.CreatePaymentUrlAsync(request.PlanType, userId);
			return Ok(new { checkoutUrl = url });
		}


		[HttpGet("me")]
		[Authorize]
		public IActionResult GetMySubscription()
		{
			var userId = GetUserIdFromToken();

			var subscription = _context.Subscriptions
				.Where(s => s.UserId == userId && s.EndDate >= DateTime.UtcNow)
				.OrderByDescending(s => s.EndDate)
				.FirstOrDefault();

			if (subscription == null)
			{
				return Ok(new { active = false });
			}

			return Ok(new SubscriptionResponse
			{
				PlanType = subscription.PlanType,
				Price = subscription.Price,
				StartDate = subscription.StartDate,
				EndDate = subscription.EndDate
			});
		}
		[AllowAnonymous]
		[HttpPost("webhook")]
		public async Task<IActionResult> PayOSWebhook([FromBody] PayOSWebhookWrapper wrapper)
		{
			Console.WriteLine("=== [PAYOS WEBHOOK RECEIVED] ===");
			Console.WriteLine(JsonConvert.SerializeObject(wrapper));

			var data = wrapper.data;

			if (!wrapper.success || wrapper.code != "00")
			{
				Console.WriteLine("[!] Webhook báo không thành công");
				return Ok(); // không lưu DB nếu không thành công
			}

			var desc = data.description?.Trim();
			if (string.IsNullOrWhiteSpace(desc) || !desc.StartsWith("Sub", StringComparison.OrdinalIgnoreCase))
				return BadRequest("Invalid description format");

			// Strip prefix "Sub" → lấy phần còn lại
			var payload = desc.Substring(3); // e.g., "monthly32c3f3d3"
			string planType = null;
			string userPrefix = null;

			if (payload.StartsWith("monthly", StringComparison.OrdinalIgnoreCase))
			{
				planType = "monthly";
				userPrefix = payload.Substring(7);
			}
			else if (payload.StartsWith("yearly", StringComparison.OrdinalIgnoreCase))
			{
				planType = "yearly";
				userPrefix = payload.Substring(6);
			}
			else
			{
				return BadRequest("Invalid plan type in description");
			}

			if (string.IsNullOrWhiteSpace(userPrefix))
				return BadRequest("Missing user ID prefix");

			var user = _context.Users
				.AsEnumerable()
				.FirstOrDefault(u => u.UserId.ToString().StartsWith(userPrefix, StringComparison.OrdinalIgnoreCase));

			if (user == null)
				return BadRequest("User not found");

			var now = DateTime.UtcNow;
			var endDate = planType == "monthly" ? now.AddMonths(1) : now.AddYears(1);
			var price = planType == "monthly" ? 39000 : 397800;

			var subscription = new Subscription
			{
				UserId = user.UserId,
				PlanType = planType,
				Price = price,
				StartDate = now,
				EndDate = endDate,
				CreatedAt = now
			};

			var transaction = new TransactionLogs
			{
				Id = Guid.NewGuid(),
				UserId = user.UserId,
				PlanType = planType,
				Amount = price,
				OrderCode = data.orderCode,
				PayOSOrderId = data.reference,
				Status = wrapper.desc,
				Description = data.description,
				CreatedAt = now
			};

			_context.Subscriptions.Add(subscription);
			_context.TransactionLogs.Add(transaction);

			try
			{
				await _context.SaveChangesAsync();
				Console.WriteLine($"[✓] Subscription & Transaction saved for {user.UserId}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[ERROR] Failed to save DB: {ex.Message}");
				return StatusCode(500, "DB save error");
			}

			return Ok();
		}






		private Guid GetUserIdFromToken()
		{
			var claim = User.FindFirst(ClaimTypes.NameIdentifier); // dùng "nameid" thay vì "sub"
			if (claim == null) throw new Exception("User ID not found in token.");
			return Guid.Parse(claim.Value);
		}


	}
}
