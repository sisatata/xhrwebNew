using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeStatus.Commands;
using EmployeeEnrollment.Application.EmployeeStatus.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeStatusController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{id}")]
        public async Task<IActionResult> GetByCompany(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeStatusList { CompanyId=id});
            return Ok(data);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeStatus { Id = id });
        //    return Ok(data);
        //}

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeStatus command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeStatus command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeStatus command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
