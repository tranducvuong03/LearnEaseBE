using LearnEase.Repository.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly LearnEaseContext _context;
		public TransactionController(LearnEaseContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAllTransactions(
	      int page = 1,
	      int pageSize = 20,
	      string? username = null,
		  string? planType = null,
          string? status = null,
		  DateTime? startDate = null,
		  DateTime? endDate = null)
		{
			if (page <= 0) page = 1;
			if (pageSize <= 0 || pageSize > 100) pageSize = 20;

			var query = _context.TransactionLogs
				.Include(t => t.User)
				.AsQueryable();

			// Lọc nếu có truyền điều kiện
			if (!string.IsNullOrEmpty(username))
				query = query.Where(t => t.User != null && t.User.Username.Contains(username));

			if (!string.IsNullOrEmpty(status))
				query = query.Where(t => t.Status != null && t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

			if (!string.IsNullOrEmpty(planType))
				query = query.Where(t => t.PlanType != null && t.PlanType.Equals(planType, StringComparison.OrdinalIgnoreCase));

			if (startDate.HasValue)
				query = query.Where(t => t.CreatedAt >= startDate.Value);

			if (endDate.HasValue)
				query = query.Where(t => t.CreatedAt <= endDate.Value);

			var totalRecords = query.Count();

			var transactions = query
				.OrderByDescending(t => t.CreatedAt)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.Select(t => new
				{
					t.Id,
					t.UserId,
					Username = t.User != null ? t.User.Username : "Unknown",
					t.PlanType,
					t.Amount,
					t.OrderCode,
					t.PayOSOrderId,
					t.Status,
					t.Description,
					t.CreatedAt
				})
				.ToList();

			return Ok(new
			{
				TotalRecords = totalRecords,
				Page = page,
				PageSize = pageSize,
				TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
				Data = transactions
			});
		}
	}
}
