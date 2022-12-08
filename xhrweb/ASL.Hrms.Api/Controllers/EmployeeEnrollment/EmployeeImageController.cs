using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeImage.Commands;
using EmployeeEnrollment.Application.EmployeeImage.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeImageController : BaseController
    {
        #region Queries

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeImageList());
        //    return Ok(data);
        //}

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Get(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeImage { EmployeeId = employeeId });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeImage command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeImage command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeImage command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
