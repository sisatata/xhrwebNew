using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Application.Shift.Commands;
using Attendance.Application.Shift.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.Attendance
{
   
    public class ShiftController : BaseController
    {

        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetShiftByCompany { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetShiftById { ShiftId = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShiftCommands.V1.ShiftConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ShiftCommands.V1.UpdateShift command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ShiftCommands.V1.MarkShiftAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}