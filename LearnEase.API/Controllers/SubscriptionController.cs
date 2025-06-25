using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Request;
using LearnEase.Service.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> PayOSWebhook([FromBody] PayOSWebhookRequest request)
		{
			if (request.status != "SUCCESS")
				return Ok(); // Không xử lý nếu thanh toán thất bại

			// Lấy userId từ description đã tạo lúc gọi đơn
			var desc = request.description;
			if (!desc.Contains("User")) return BadRequest("Invalid description");

			var userIdStr = desc.Split("User").Last().Trim();
			if (!Guid.TryParse(userIdStr, out var userId))
				return BadRequest("Invalid userId");

			var planType = desc.Contains("yearly") ? "yearly" : "monthly";
			var now = DateTime.UtcNow;
			var endDate = planType == "monthly" ? now.AddMonths(1) : now.AddYears(1);
			var price = planType == "monthly" ? 66000 : 673200;

			var subscription = new Subscription
			{
				UserId = userId,
				PlanType = planType,
				Price = price,
				StartDate = now,
				EndDate = endDate,
				CreatedAt = now
			};

			_context.Subscriptions.Add(subscription);
			await _context.SaveChangesAsync();

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
