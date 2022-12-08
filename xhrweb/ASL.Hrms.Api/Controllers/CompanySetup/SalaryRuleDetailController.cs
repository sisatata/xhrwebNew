using System;
using System.Threading.Tasks;
using CompanySetup.Application.SalaryRuleDetail.Commands;
using CompanySetup.Application.SalaryRuleDetail.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class SalaryRuleDetailController : BaseController
    {
        #region Queries

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new Queries.GetSalaryRuleDetailList());
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetSalaryRuleDetail { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateSalaryRuleDetail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateSalaryRuleDetail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteSalaryRuleDetail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
