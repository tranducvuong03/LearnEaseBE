using Hangfire.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    public class AllowAllDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true; //  Cho phép tất cả mọi người truy cập dashboard
        }
    }

}
