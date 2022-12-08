using System;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeSalaryComponent.Commands;
using PayrollManagement.Application.EmployeeSalaryComponent.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeSalaryComponentController : BaseController
    {
        #region Queries

        [HttpGet("parent/{employeeSalaryId}")]
        public async Task<IActionResult> GetByParent(Guid employeeSalaryId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeSalaryComponentListByParent { EmployeeSalaryId = employeeSalaryId });
            return Ok(data);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeSalaryComponent { Id = id });
        //    return Ok(data);
        //}

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeSalaryComponent command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeSalaryComponent command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeSalaryComponent command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
