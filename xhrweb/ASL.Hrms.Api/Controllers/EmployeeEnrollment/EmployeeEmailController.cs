using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeEmail.Commands;
using EmployeeEnrollment.Application.EmployeeEmail.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeEmailController : BaseController
    {
        #region Queries

        [HttpGet("employeeId/{id}")]
        public async Task<IActionResult> GetByEmployee(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeEmailListByEmployeeId { EmployeeId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeEmailByID { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeEmail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeEmail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeEmail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
