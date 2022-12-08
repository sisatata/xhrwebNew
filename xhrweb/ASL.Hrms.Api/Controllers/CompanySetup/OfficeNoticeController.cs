using System;
using System.Threading.Tasks;
using CompanySetup.Application.OfficeNotice.Commands;
using CompanySetup.Application.OfficeNotice.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class OfficeNoticeController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{id}")]
        public async Task<IActionResult> GetByCompany(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetOfficeNoticeList { CompanyId = id });
            return Ok(data);
        }

        [HttpGet("activeListCompanyId/{id}")]
        public async Task<IActionResult> GetActiveAndPulishedByCompany(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetOfficeNoticeActiveAndPublishedList { CompanyId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetOfficeNotice { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateOfficeNotice command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateOfficeNotice command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteOfficeNotice command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
