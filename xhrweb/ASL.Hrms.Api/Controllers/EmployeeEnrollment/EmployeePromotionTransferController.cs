using System;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeePromotionTransfer.Commands;
using EmployeeEnrollment.Application.EmployeePromotionTransfer.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeePromotionTransferController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeePromotionTransferListByCompany
            {
                CompanyId = companyId
            });
            return Ok(data);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeePromotionTransferListByEmployee
            {
                EmployeeId = employeeId
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetEmployeePromotionTransfer { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeePromotionTransfer command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeePromotionTransfer command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeePromotionTransfer command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("approve-employee-promotion-transfer")]
        public async Task<IActionResult> ApproveEmployeePromotionTransfer([FromBody] Commands.V1.ApproveEmployeePromotionTransferCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("decline-employee-promotion-transfer")]
        public async Task<IActionResult> DeclineEmployeePromotionTransfer([FromBody] Commands.V1.RejectEmployeePromotionTransferCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
