using System;
using System.Threading.Tasks;
using PayrollManagement.Application.SalaryStructureComponent.Commands;
using PayrollManagement.Application.SalaryStructureComponent.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class SalaryStructureComponentController : BaseController
    {
        #region Queries

        [HttpGet("salarystructure/{structureId}")]
        public async Task<IActionResult> GetByStructure(Guid structureId)
        {
            var data = await Mediator.Send(new Queries.GetSalaryStructureComponentList { StructureId = structureId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetSalaryStructureComponent { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateSalaryStructureComponent command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateSalaryStructureComponent command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteSalaryStructureComponent command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
