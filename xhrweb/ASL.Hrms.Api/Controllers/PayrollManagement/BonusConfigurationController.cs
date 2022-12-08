using System;
using System.Threading.Tasks;
using PayrollManagement.Application.BonusConfiguration.Commands;
using PayrollManagement.Application.BonusConfiguration.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class BonusConfigurationController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{id}")]
        public async Task<IActionResult> GetByCompany(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBonusConfigurationList { CompanyId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBonusConfiguration { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateBonusConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateBonusConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteBonusConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
