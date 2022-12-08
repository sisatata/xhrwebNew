using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeExperience.Commands;
using EmployeeEnrollment.Application.EmployeeExperience.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeExperienceController : BaseController
    {
        #region Queries

        [HttpGet("employeeId/{id}")]
        public async Task<IActionResult> GetByEmployee(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeExperienceList { EmployeeId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeExperience { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeExperience command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeExperience command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeExperience command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
