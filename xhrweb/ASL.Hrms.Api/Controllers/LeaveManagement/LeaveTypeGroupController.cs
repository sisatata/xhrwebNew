using System;
using System.Threading.Tasks;
using LeaveManagement.Application.LeaveTypeGroup.Commands;
using LeaveManagement.Application.LeaveTypeGroup.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class LeaveTypeGroupController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetLeaveTypeGroupList
            {
                CompanyId = companyId
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await Mediator.Send(new Queries.GetLeaveTypeGroup { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateLeaveTypeGroup command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateLeaveTypeGroup command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteLeaveTypeGroup command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
