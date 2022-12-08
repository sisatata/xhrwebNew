using System;
using System.Threading.Tasks;
using Attendance.Application.Holiday.Commands;
using Attendance.Application.Holiday.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class HolidayController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetHolidayList { CompanyId = companyId });
            return Ok(data);
        }


        [HttpGet("company/{companyId}/financialYear/{financialYear}")]
        public async Task<IActionResult> GetByFinancialYear(Guid companyId, string financialYear)
        {
            var data = await Mediator.Send(new Queries.GetHolidayListByFinancialYear { CompanyId = companyId, Financialyear = financialYear });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetHoliday { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateHoliday command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateHoliday command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteHoliday command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
