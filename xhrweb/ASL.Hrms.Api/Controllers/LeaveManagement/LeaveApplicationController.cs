using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagement.Application.LeaveApplications.Commands.V1;
using LeaveManagement.Application.LeaveApplications.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.LeaveManagement
{
    //[Authorize]
    public class LeaveApplicationController : BaseController
    {


        #region Queries

        [HttpGet("company/{companyId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetByCompany(Guid companyId, DateTime? startDate, DateTime? endDate)
        {
            var data = await Mediator.Send(new Queries.GetLeaveApplicationByCompany { CompanyId = companyId,
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(data);
        }

        [HttpPost("leave-application")]
        public async Task<IActionResult> GetLeaveApplication([FromBody] Queries.GetLeaveApplication input)
        {
            var data = await Mediator.Send(input);
            return Ok(data);
        }


        [HttpGet]
        public async Task<IActionResult> GetByParameter(Guid companyId, DateTime? startDate, DateTime? endDate, string employeeName=null, string leaveTypeName=null, string status=null)
        {
            var data = await Mediator.Send(new Queries.GetLeaveApplicationByParameter
            {
                CompanyId = companyId,
                StartDate = startDate,
                EndDate = endDate,
                EmployeeName= employeeName,
                LeaveTypeName = leaveTypeName,
                ApprovalStatusText = status

            });
            return Ok(data);
        }

        [HttpGet("employee/{employeeId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId, DateTime? startDate, DateTime? endDate)
        {
            var data = await Mediator.Send(new Queries.GetLeaveApplicationByEmployee
            {
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetLeaveApplicationById { LeaveApplicationId = id });
            return Ok(data);
        }

        [HttpGet("pending-leave-application/{managerId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetPendingLeaveApplication(Guid managerId, DateTime? startDate, DateTime? endDate)
        {
            var data = await Mediator.Send(new Queries.GetPendingLeaveNotificationByManager { ManagerId = managerId,
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(data);
        }

        #endregion



        [HttpPost("apply-leave")]
        public async Task<IActionResult> ApplyLeave([FromBody] ApplyLeaveCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut("update-leave")]
        public async Task<IActionResult> UpdateLeave([FromBody] UpdateLeaveCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("approve-leave")]
        public async Task<IActionResult> ApproveLeave([FromBody] ApproveLeaveCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("decline-leave")]
        public async Task<IActionResult> DeclineLeave([FromBody] RejectLeaveCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }
    }
}