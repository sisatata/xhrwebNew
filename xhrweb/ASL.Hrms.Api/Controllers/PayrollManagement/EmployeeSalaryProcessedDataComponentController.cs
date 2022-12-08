using System;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Commands;
using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeSalaryProcessedDataComponentController : BaseController
    {
        //#region Queries

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeSalaryProcessedDataComponentList());
        //    return Ok(data);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeSalaryProcessedDataComponent { Id = id });
        //    return Ok(data);
        //}

        //#endregion

        //#region Commands

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeSalaryProcessedDataComponent command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeSalaryProcessedDataComponent command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeSalaryProcessedDataComponent command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //#endregion
    }
}
