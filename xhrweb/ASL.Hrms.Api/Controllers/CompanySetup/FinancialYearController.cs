using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySetup.Application.FinancialYear.Commands;
using CompanySetup.Application.FinancialYear.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.CompanySetup
{
    
    public class FinancialYearController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetFinancialYearByCompanyId { CompanyId = companyId});
            return Ok(data);
        }

        [HttpGet("get-current-year/{companyId}")]
        public async Task<IActionResult> GetCurrentYearByCompanyId(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetFinancialYearByCompanyId { CompanyId = companyId });
            if(data != null && data.Count>0)
            {
                var d = data.FirstOrDefault(fr => fr.FinancialYearStartDate <= DateTime.Now.Date && fr.FinancialYearEndDate >= DateTime.Now.Date);
                return Ok(d);
            }
            return Ok(null);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetFinancialYearById { Id = id });
            return Ok(data);
        }

        [HttpGet("get-current-financialyearid/company/{companyId}/year/{financialYearName}")]
        public async Task<IActionResult> GetById(Guid? companyId, string financialYearName)
        {
            var data = await Mediator.Send(new Queries.GetFinancialYearByName { CompanyId = companyId, FinancialYearName = financialYearName });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FinancialYearCommands.V1.CreateFinancialYear command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FinancialYearCommands.V1.UpdateFinancialYear command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] FinancialYearCommands.V1.MarkFinancialYearAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}