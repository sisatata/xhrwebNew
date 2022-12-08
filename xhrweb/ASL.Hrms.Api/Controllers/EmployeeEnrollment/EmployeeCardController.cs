using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeCard.Commands;
using EmployeeEnrollment.Application.EmployeeCard.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeCardController : BaseController
    {
        #region Queries

        [HttpGet("Employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId)
        {
            
            
            var data = await Mediator.Send(new Queries.GetEmployeeCardList { EmployeeId = employeeId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeCard { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeCard command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeCard command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeCard command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
