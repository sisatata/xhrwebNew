using System;
using System.Threading.Tasks;
using PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Commands;
using PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class BenefitDeductionEmployeeAssignedController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{id}")]
        public async Task<IActionResult> GetByCompany(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBenefitDeductionEmployeeAssignedList { CompanyId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBenefitDeductionEmployeeAssigned { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateBenefitDeductionEmployeeAssigned command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateBenefitDeductionEmployeeAssigned command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteBenefitDeductionEmployeeAssigned command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
