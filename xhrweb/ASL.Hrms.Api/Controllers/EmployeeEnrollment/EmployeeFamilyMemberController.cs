using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeFamilyMember.Commands;
using EmployeeEnrollment.Application.EmployeeFamilyMember.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeFamilyMemberController : BaseController
    {
        #region Queries

        [HttpGet("employeeId/{id}")]
        public async Task<IActionResult> GetByEmployee(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeFamilyMemberList { EmployeeId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeFamilyMember { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeFamilyMember command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeFamilyMember command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeFamilyMember command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
