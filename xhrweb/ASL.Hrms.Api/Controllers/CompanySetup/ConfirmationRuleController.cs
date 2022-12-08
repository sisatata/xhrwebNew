using System;
using System.Threading.Tasks;
using CompanySetup.Application.ConfirmationRule.Commands;
using CompanySetup.Application.ConfirmationRule.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class ConfirmationRuleController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetConfirmationRuleList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("activeListByCompany/{companyId}")]
        public async Task<IActionResult> GetActiveListByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetConfirmationRuleActiveList { CompanyId = companyId });
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetConfirmationRule { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateConfirmationRule command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateConfirmationRule command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteConfirmationRule command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
