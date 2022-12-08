using System;
using System.Threading.Tasks;
using PayrollManagement.Application.BenefitDeductionConfig.Commands;
using PayrollManagement.Application.BenefitDeductionConfig.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class BenefitDeductionConfigController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{id}")]
        public async Task<IActionResult> GetByCompany(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBenefitDeductionConfigList { CompanyId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBenefitDeductionConfig { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateBenefitDeductionConfig command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateBenefitDeductionConfig command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteBenefitDeductionConfig command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
