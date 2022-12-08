using System;
using System.Threading.Tasks;

using Attendance.Application.AttendanceRequest.Commands;
using Attendance.Application.AttendanceRequest.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class AttendanceRequestController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetAttendanceRequestList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("GetBySearch/{employeeId:guid}/{requestTypeId:int?}/{startTime:datetime?}/{endTime:datetime?}")]
        public async Task<IActionResult> GetBySearch(Guid employeeId, int requestTypeId, DateTime? startTime = null, DateTime? endTime = null)
        {
            var data = await Mediator.Send(new Queries.GetAttendanceBySearchRequestList
            {
                EmployeeId = employeeId,
                RequestTypeId = requestTypeId,
                StartTime = startTime,
                EndTime = endTime
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetAttendanceRequest { Id = id });
            return Ok(data);
        }

        [HttpGet("pending-attendance-request/{managerId}/{startTime:datetime?}/{endTime:datetime?}")]
        public async Task<IActionResult> GetPendingAttendanceRequest(Guid managerId, DateTime? startTime = null, DateTime? endTime = null)
        {
            var data = await Mediator.Send(new Queries.GetPendingAttendanceReqNotificationByManager
            {
                ManagerId = managerId,
                StartTime = startTime,
                EndTime = endTime
            });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateAttendanceRequest command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateAttendanceRequest command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("approve")]
        public async Task<IActionResult> Approve([FromBody] Commands.V1.ApproveAttendanceRequest command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("decline-attendance")]
        public async Task<IActionResult> Decline([FromBody] Commands.V1.RejectAttendanceRequest command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteAttendanceRequest command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
