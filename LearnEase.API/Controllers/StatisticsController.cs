using LearnEase.Repository.EntityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
		private readonly LearnEaseContext _context;

		public StatisticsController(LearnEaseContext context)
		{
			_context = context;
		}

		[HttpGet("profit-today")]
		public async Task<IActionResult> GetTodayProfit()
		{
			var today = DateTime.UtcNow.Date;
			var yesterday = today.AddDays(-1);

			var todayProfit = await _context.Subscriptions
				.Where(s => s.CreatedAt.Date == today)
				.SumAsync(s => (decimal?)s.Price) ?? 0;

			var yesterdayProfit = await _context.Subscriptions
				.Where(s => s.CreatedAt.Date == yesterday)
				.SumAsync(s => (decimal?)s.Price) ?? 0;

			double changePercent;
			if (yesterdayProfit == 0)
			{
				changePercent = todayProfit == 0 ? 0 : 100;
			}
			else
			{
				changePercent = (double)(todayProfit - yesterdayProfit) / (double)yesterdayProfit * 100;
			}

			return Ok(new { profit = todayProfit, changePercent });
		}

		[HttpGet("new-users")]
		public async Task<IActionResult> GetNewUsers()
		{
			var today = DateTime.UtcNow;
			var thisWeekStart = today.AddDays(-(int)today.DayOfWeek).Date;
			var lastWeekStart = thisWeekStart.AddDays(-7);

			var thisWeekUsers = await _context.Users
				.Where(u => u.CreatedAt >= thisWeekStart)
				.CountAsync();

			var lastWeekUsers = await _context.Users
				.Where(u => u.CreatedAt >= lastWeekStart && u.CreatedAt < thisWeekStart)
				.CountAsync();

			double changePercent;
			if (lastWeekUsers == 0)
			{
				changePercent = thisWeekUsers == 0 ? 0 : 100;
			}
			else
			{
				changePercent = ((double)(thisWeekUsers - lastWeekUsers) / lastWeekUsers) * 100;
			}

			return Ok(new { newUsers = thisWeekUsers, changePercent });
		}

		[HttpGet("revenue-month")]
		public async Task<IActionResult> GetMonthlyRevenue()
		{
			var now = DateTime.UtcNow;
			var startOfMonth = new DateTime(now.Year, now.Month, 1);
			var startOfLastMonth = startOfMonth.AddMonths(-1);

			var thisMonthRevenue = await _context.Subscriptions
				.Where(s => s.CreatedAt >= startOfMonth)
				.SumAsync(s => (decimal?)s.Price) ?? 0;

			var lastMonthRevenue = await _context.Subscriptions
				.Where(s => s.CreatedAt >= startOfLastMonth && s.CreatedAt < startOfMonth)
				.SumAsync(s => (decimal?)s.Price) ?? 0;

			double changePercent;
			if (lastMonthRevenue == 0)
			{
				changePercent = thisMonthRevenue == 0 ? 0 : 100;
			}
			else
			{
				changePercent = (double)(thisMonthRevenue - lastMonthRevenue) / (double)lastMonthRevenue * 100;
			}

			return Ok(new { revenue = thisMonthRevenue, changePercent });
		}
		[HttpGet("annual-revenue")]
		public async Task<IActionResult> GetAnnualRevenue()
		{
			var now = DateTime.UtcNow;
			var currentYear = now.Year;
			var lastYear = currentYear - 1;

			var currentYearRevenue = new decimal[12];
			var lastYearRevenue = new decimal[12];

			for (int month = 1; month <= 12; month++)
			{
				var startThis = new DateTime(currentYear, month, 1);
				var endThis = startThis.AddMonths(1);

				var startLast = new DateTime(lastYear, month, 1);
				var endLast = startLast.AddMonths(1);

				currentYearRevenue[month - 1] = await _context.Subscriptions
					.Where(s => s.CreatedAt >= startThis && s.CreatedAt < endThis)
					.SumAsync(s => (decimal?)s.Price) ?? 0;

				lastYearRevenue[month - 1] = await _context.Subscriptions
					.Where(s => s.CreatedAt >= startLast && s.CreatedAt < endLast)
					.SumAsync(s => (decimal?)s.Price) ?? 0;
			}

			decimal sumCurrent = currentYearRevenue.Sum();
			decimal sumLast = lastYearRevenue.Sum();

			double changePercent;
			if (sumLast == 0)
				changePercent = sumCurrent == 0 ? 0 : 100;
			else
				changePercent = (double)(sumCurrent - sumLast) / (double)sumLast * 100;

			var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
						 "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

			var monthlyRevenue = months.Select((m, i) => new {
				month = m,
				value = Math.Round(currentYearRevenue[i], 2)
			});

			return Ok(new
			{
				monthlyRevenue,
				changePercent = Math.Round(changePercent, 2),
				totalRevenue = Math.Round(sumCurrent, 2)
			});
		}


	}
}
