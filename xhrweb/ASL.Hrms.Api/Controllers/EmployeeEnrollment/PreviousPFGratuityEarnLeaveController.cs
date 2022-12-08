using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Commands;
using EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class PreviousPFGratuityEarnLeaveController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetPreviousPFGratuityEarnLeaveList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> Get(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetPreviousPFGratuityEarnLeave { EmployeeId = employeeId });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreatePreviousPFGratuityEarnLeave command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdatePreviousPFGratuityEarnLeave command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeletePreviousPFGratuityEarnLeave command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
