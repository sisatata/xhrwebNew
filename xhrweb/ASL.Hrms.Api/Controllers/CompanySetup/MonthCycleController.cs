using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySetup.Application.MonthCycle.Commands;
using CompanySetup.Application.MonthCycle.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.CompanySetup
{
    
    public class MonthCycleController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}/financialYear/{financialYearId}")]
        public async Task<IActionResult> GetByCompanyIdAndFinancialYearId(Guid companyId, Guid financialYearId)
        {
            var data = await Mediator.Send(new Queries.GetMonthCycleByCompanyIdandFinancialYear { CompanyId = companyId, FinancialYearId = financialYearId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetMonthCycleById { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonthCycleCommands.CreateMonthCycle command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]MonthCycleCommands.UpdateMonthCycle command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] MonthCycleCommands.MarkMonthCycleAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }


        [HttpPost("process-month-cycle-create")]
        public async Task<IActionResult> ProcessMonthCycleCreate([FromBody] MonthCycleCommands.ProcessMonthCycleCreate command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }
        #endregion
    }
}