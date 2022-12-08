using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.Filters
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public HangfireDashboardAuthorizationFilter()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        private readonly HttpContextAccessor _httpContextAccessor;

        // Not working properly. Need to inject _httpContextAccessor from startup.
        public bool Authorize(DashboardContext context)
        {
            var e = _httpContextAccessor.HttpContext.Request.Cookies;
            var _sessionService = context.GetHttpContext();
            var aa = _sessionService.User;

            var ac = new HttpContextAccessor() { HttpContext = new DefaultHttpContext() };


            var aaa = new HttpContextAccessor();
            // claims has been assigned on XtraTokenHandler class
            var currentUser = _httpContextAccessor.HttpContext?.User;


            var loggedInUserIdClaims = currentUser.Claims
                                .Where(c => c.Type == "LoggedInUserId")
                                .Select(x => x.Value).FirstOrDefault();


            //return !string.IsNullOrWhiteSpace(loggedInUserIdClaims);
            return true;
        }
    }
}
