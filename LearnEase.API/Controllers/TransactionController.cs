using LearnEase.Repository.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult GetAllTransactions()
		{
			var transactions = _context.TransactionLogs
				.OrderByDescending(t => t.CreatedAt)
				.Select(t => new
				{
					t.Id,
					t.UserId,
					Username = t.User.Username, // nếu có navigation
					t.PlanType,
					t.Amount,
					t.OrderCode,
					t.PayOSOrderId,
					t.Status,
					t.Description,
					t.CreatedAt
				})
				.ToList();

			return Ok(transactions);
		}
	}
}
