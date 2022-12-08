using System;
using System.Threading.Tasks;
using PayrollManagement.Application.BankBranch.Commands;
using PayrollManagement.Application.BankBranch.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class BankBranchController : BaseController
    {
        #region Queries

        [HttpGet("getbybank/{bankid}")]
        public async Task<IActionResult> GetByBank(Guid bankId)
        {
            var data = await Mediator.Send(new Queries.GetBankBranchListByBank { BankId = bankId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBankBranch { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateBankBranch command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateBankBranch command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteBankBranch command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
