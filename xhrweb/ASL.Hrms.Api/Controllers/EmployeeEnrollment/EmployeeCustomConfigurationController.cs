using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeCustomConfiguration.Commands;
using EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeCustomConfigurationController : BaseController
    {
        #region Queries

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeCustomConfigurationList { EmployeeId = employeeId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeCustomConfiguration { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeCustomConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeCustomConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeCustomConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
