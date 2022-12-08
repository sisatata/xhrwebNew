using System;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeBonusProcessedData.Commands;
using PayrollManagement.Application.EmployeeBonusProcessedData.Queries;
using Microsoft.AspNetCore.Mvc;
using ASL.Hrms.SharedKernel.Interfaces;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeBonusProcessedDataController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{companyId}/bonusYearId{bonusYearId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId, Guid bonusYearId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeBonusProcessedDataList
            {
                CompanyId = companyId,
                BonusYearId = bonusYearId
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeBonusProcessedData { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.StartEmployeeBonusProcess command)
        {
            var data = await Mediator.Send(command);

            return Ok(data);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeBonusProcessedData command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeBonusProcessedData command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        #endregion
    }
}
