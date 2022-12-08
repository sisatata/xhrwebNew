using System;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeSalary.Commands;
using PayrollManagement.Application.EmployeeSalary.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeSalaryController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeSalaryListByCompany { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeSalaryListByEmployee { EmployeeId = employeeId });
            return Ok(data);
        }

        [HttpGet("employee-salary-history-by-employee/{employeeId}")]
        public async Task<IActionResult> GetSalaryHistoryByEmployee(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeSalaryHistoryByEmployee { EmployeeId = employeeId });
            return Ok(data);
        }

        [HttpGet("employee-salary-history-by-company/{companyId}")]
        public async Task<IActionResult> GetSalaryHistoryByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeSalaryHistoryByCompany { CompanyId = companyId });
            return Ok(data);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeSalary { Id = id });
        //    return Ok(data);
        //}

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeSalary command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }
        [HttpPost("currentSalary")]
        public async Task<IActionResult> GetCurrentSalary([FromBody] Queries.GetEmployeeSalaryWithFilter input)
        {
            var data = await Mediator.Send(input);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeSalary command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeSalary command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
