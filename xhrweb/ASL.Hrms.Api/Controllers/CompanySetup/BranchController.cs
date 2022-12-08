using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySetup.Application.Branch.Commands;
using CompanySetup.Application.Branch.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.CompanySetup
{
    //[Authorize]
    public class BranchController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetBranchByCompany { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBranchById { BranchId = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BranchCommands.V1.CreateBranch command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BranchCommands.V1.UpdateBranch command)
        {
            var data = await Mediator.Send(command);
             return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] BranchCommands.V1.MarkBranchAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion

    }
}