using System;
using System.Threading.Tasks;
using ASL.AccessControl.Models;
using ASL.AccessControl.Services.Security;
using ASL.AccessControl.Utility;
using ASL.Hrms.Api.PermissionProvider;
using EmployeeEnrollment.Application.Employee.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeEnrollment.Application.AdminDashBoard.Queries;

namespace ASL.Hrms.Api.Controllers.EmployeeEnrollment
{

    public class AdminDashboardController : BaseController
    {
        [HttpGet("GetAdminDashboardByManagerAndDate/companyId/{companyId}/managerId/{managerId}/punchDate/{punchDate}")]
        //[Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> Get(Guid companyId, Guid managerId, DateTime punchDate)
        {
            var data = await Mediator.Send(new Queries.GetAdminDashBoard { CompanyId = companyId, ManagerId = managerId, PunchDate = punchDate });
            return Ok(data);
        }
    }
}
