using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagement.Application.LeaveTypes.Commands;
using LeaveManagement.Application.LeaveTypes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.LeaveManagement
{
    public class LeaveTypeController : BaseController
    {

        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetLeaveTypeByCompany { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("company-combo-box/{companyId}")]
        public async Task<IActionResult> GetByCompanyForComboBox(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetLeaveTypeByCompanyForComboBox { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetLeaveTypeByEmployee { EmployeeId = employeeId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetLeaveTypeById { LeaveTypeId = id });
            return Ok(data);
        }

        #endregion
        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeaveTypeCommands.V1.CreateLeaveType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LeaveTypeCommands.V1.UpdateLeaveType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] LeaveTypeCommands.V1.MarkLeaveTypeAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}