using System;
using System.Threading.Tasks;
using PayrollManagement.Application.SalaryStructure.Commands;
using PayrollManagement.Application.SalaryStructure.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class SalaryStructureController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetSalaryStructureList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetSalaryStructure { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateSalaryStructure command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateSalaryStructure command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteSalaryStructure command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
