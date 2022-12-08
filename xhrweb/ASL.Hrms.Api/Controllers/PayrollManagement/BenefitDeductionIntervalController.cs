using System;
using System.Threading.Tasks;
using PayrollManagement.Application.BenefitDeductionInterval.Commands;
using PayrollManagement.Application.BenefitDeductionInterval.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class BenefitDeductionIntervalController : BaseController
    {
        #region Queries

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Mediator.Send(new Queries.GetBenefitDeductionIntervalList());
            return Ok(data);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetBenefitDeductionInterval { Id = id });
        //    return Ok(data);
        //}

        #endregion

        //#region Commands

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Commands.V1.CreateBenefitDeductionInterval command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateBenefitDeductionInterval command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteBenefitDeductionInterval command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //#endregion
    }
}
