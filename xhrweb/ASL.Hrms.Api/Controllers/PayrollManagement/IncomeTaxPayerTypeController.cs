using System;
using System.Threading.Tasks;
using PayrollManagement.Application.IncomeTaxPayerType.Commands;
using PayrollManagement.Application.IncomeTaxPayerType.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class IncomeTaxPayerTypeController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetIncomeTaxPayerTypeList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetIncomeTaxPayerType { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateIncomeTaxPayerType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateIncomeTaxPayerType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteIncomeTaxPayerType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
