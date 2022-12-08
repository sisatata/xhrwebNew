using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySetup.Application.Department.Commands;
using Microsoft.AspNetCore.Mvc;
using CompanySetup.Application.Department.Queries;

namespace ASL.Hrms.Api.Controllers.CompanySetup
{
    public class DepartmentController : BaseController
    {
        #region Queries

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetByBranch(Guid branchId)
        {
            var data = await Mediator.Send(new Queries.GetDepartmentByBranch { BranchId = branchId });
            return Ok(data);
        }

        [HttpPost("get-by-branches")]
        public async Task<IActionResult> GetByBranchList([FromBody] Queries.GetDepartmentsByBranchList request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetDepartmentById { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCommands.V1.CreateDepartment command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DepartmentCommands.V1.UpdateDepartment command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DepartmentCommands.V1.MarkDepartmentAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}