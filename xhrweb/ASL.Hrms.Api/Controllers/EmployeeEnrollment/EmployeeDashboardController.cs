using System;
using System.Threading.Tasks;
using ASL.AccessControl.Models;
using ASL.AccessControl.Services.Security;
using ASL.AccessControl.Utility;
using ASL.Hrms.Api.PermissionProvider;
using EmployeeEnrollment.Application.Employee.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeEnrollment.Application.EmployeeDashboard.Queries;

namespace ASL.Hrms.Api.Controllers.EmployeeEnrollment
{

    public class EmployeeDashboardController : BaseController
    {
        [HttpGet("{id}")]
        //[Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeDashboard { Id = id });
            return Ok(data);
        }

        [HttpGet("employeeId/{id}")]
        //[Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> GetEmployeeProfile(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeProfileById { EmployeeId = id });
            return Ok(data);
        }
    }
}
