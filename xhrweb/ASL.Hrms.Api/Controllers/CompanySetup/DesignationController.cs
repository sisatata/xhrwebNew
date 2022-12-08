using System;
using System.Threading.Tasks;
using CompanySetup.Application.Designation.Queries;
using CompanySetup.Application.Designation.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASL.Hrms.Api.Controllers.CompanySetup
{
    public class DesignationController : BaseController
    {

        #region Queries

        [HttpGet("entity/{entityId}")]
        public async Task<IActionResult> GetByBranch(Guid entityId)
        {
            var data = await Mediator.Send(new Queries.GetDesignationByEntity { EntityId = entityId });
            return Ok(data);
        }

        [HttpPost("get-by-departments")]
        public async Task<IActionResult> GetByDepartmentList([FromBody] Queries.GetDesignationListByEntities request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetDesignationById { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DesignationCommands.V1.CreateDesignation command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DesignationCommands.V1.UpdateDesignation command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DesignationCommands.V1.MarkDesignationAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}