using Attendance.Application.ShiftGroup.Commands;
using Attendance.Application.ShiftGroup.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.Controllers.Attendance
{
    public class ShiftGroupController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetShiftGroupByCompany { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetShiftGroupById { ShiftGroupId = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShiftGroupCommands.V1.CreateShiftGroup command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ShiftGroupCommands.V1.UpdateShiftGroup command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ShiftGroupCommands.V1.MarkShiftGroupAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion

    }
}
