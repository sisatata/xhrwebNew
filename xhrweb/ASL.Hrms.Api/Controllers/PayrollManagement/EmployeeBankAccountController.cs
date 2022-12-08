using System;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeBankAccount.Commands;
using PayrollManagement.Application.EmployeeBankAccount.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeBankAccountController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeBankAccountList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeBankAccount { Id = id });
            return Ok(data);
        }
        [HttpGet("company/{companyId}/employee/{employeeId}")]
        public async Task<IActionResult> GetEmployeeBankAccountByEmployeeId(Guid companyId, Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeBankAccountListByEmployee { CompanyId = companyId, EmployeeId= employeeId });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeBankAccount command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeBankAccount command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeBankAccount command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
