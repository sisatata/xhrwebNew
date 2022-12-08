using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeePhone.Commands;
using EmployeeEnrollment.Application.EmployeePhone.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeePhoneController : BaseController
    {
        #region Queries

        [HttpGet("employeeId/{id}")]
        public async Task<IActionResult> GetByEmployee(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeePhoneList { EmployeeId =id});
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeePhone { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeePhone command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeePhone command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeePhone command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
