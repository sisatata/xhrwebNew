using System;
using System.Threading.Tasks;
using LeaveManagement.Application.LeaveBalances;
using LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.LeaveManagement
{
    public class LeaveBalanceController : BaseController
    {
        [HttpGet("employee/{employeeId}/calendar/{leaveCalendar}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId, string leaveCalendar)
        {
            var data = await Mediator.Send(
                new LeaveBalanceSummary.GetLeaveBalanceSummaryByEmployee
                {
                    EmployeeId = employeeId,
                    LeaveCalendar = leaveCalendar
                });
            return Ok(data);
        }
        [HttpPost("leave-balance")]
        public async Task<IActionResult> GetLeaveBalance([FromBody] LeaveBalanceSummary.GetLeaveBalanceSummary  input) 
        {
            var data = await Mediator.Send(input);    
            return Ok(data);
        }

        [HttpPost("process-balance")]
        public async Task<IActionResult> Create([FromBody] LeaveBalanceCommands.V1.ProcessLeaveBalanceCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }
    }
}