using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.Services
{
    public class CurrentUserContext : ICurrentUserContext
    {
        private readonly IHttpContextAccessor _httpContext;
        public CurrentUserContext(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public bool? IsAuthenticated => _httpContext.HttpContext.User?.Identity.IsAuthenticated;
        public string CurrentUserId => _httpContext.HttpContext?.User?.Claims
                                .Where(c => c.Type == "UserId")
                                .Select(x => x.Value).FirstOrDefault();

        public string CurrentUserCompanyId => _httpContext.HttpContext?.User?.Claims
                                .Where(c => c.Type == "CompanyId")
                                .Select(x => x.Value).FirstOrDefault();

        public string EmployeeId => _httpContext.HttpContext?.User?.Claims
                                .Where(c => c.Type == "EmployeeId")
                                .Select(x => x.Value).FirstOrDefault();

        public string EmployeeCode => _httpContext.HttpContext?.User?.Claims
                                .Where(c => c.Type == "EmployeeCode")
                                .Select(x => x.Value).FirstOrDefault();

        public string EmployeeName => _httpContext.HttpContext?.User?.Claims
                                .Where(c => c.Type == "EmployeeName")
                                .Select(x => x.Value).FirstOrDefault();
    }
}