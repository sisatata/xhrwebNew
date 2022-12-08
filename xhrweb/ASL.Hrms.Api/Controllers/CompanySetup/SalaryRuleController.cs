using System;
using System.Threading.Tasks;
using CompanySetup.Application.SalaryRule.Commands;
using CompanySetup.Application.SalaryRule.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class SalaryRuleController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetSalaryRuleList { CompanyId = companyId });
            return Ok(data);
        }
        [HttpGet("activeListByCompany/{companyId}")]
        public async Task<IActionResult> GetActiveListByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetSalaryRuleActiveList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetSalaryRule { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateSalaryRule command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateSalaryRule command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteSalaryRule command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
