using System;
using System.Threading.Tasks;
using PayrollManagement.Application.BenefitBillClaim.Commands;
using PayrollManagement.Application.BenefitBillClaim.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ASL.Hrms.SharedKernel.Models;
using Microsoft.AspNetCore.Authorization;
using ASL.Hrms.Api.PermissionProvider;

namespace ASL.Hrms.Api.Controllers
{
    public class BenefitBillClaimController : BaseController
    {
        public BenefitBillClaimController(IOptions<PlanetHRSettings> settings)
        {
            _settings = settings;
        }
        private readonly IOptions<PlanetHRSettings> _settings;
        #region Queries

        [HttpGet("GetByCompanyAndDateRange/companyId/{companyId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetByCompanyAndDateRange(Guid companyId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetBenefitBillClaimListByCompany
            {
                CompanyId = companyId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }


        [HttpGet("GetByEmployeeAndDateRange/employeeId/{employeeId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetByEmployeeAndDateRange(Guid employeeId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetBenefitBillClaimListByEmployee
            {
                EmployeeId = employeeId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetBenefitBillClaim { Id = id });
            return Ok(data);
        }

        [HttpGet("pending-bill-request/{managerId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetPendingBillApplication(Guid managerId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetPendingBillClaimNotificationByManager
            {
                ManagerId = managerId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }

        [HttpPost("pending-bill-request")]
        public async Task<IActionResult> GetBenefirBillClaim([FromBody] Queries.GetPendingBillClaimNotification input)
        {
             var data = await Mediator.Send(input);  
            return Ok(data);
        }


        [HttpGet("unpaid-approved-bill-request/{managerId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetUnpaidApprovedBillApplication(Guid managerId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetUnpaidApprovedBillClaimNotificationByManager
            {
                ManagerId = managerId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }

        [HttpGet("paid-approved-bill-request/{managerId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetPaidApprovedBillApplication(Guid managerId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetPaidApprovedBillClaimNotificationByManager
            {
                ManagerId = managerId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }
        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Commands.V1.CreateBenefitBillClaim command)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files[0].Length > 0)
            {
                var billAttachment = Request.Form.Files?[0];

                var settings = _settings.Value;

                command.BillAttachment = billAttachment;
                command.Settings = settings;
            }
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] Commands.V1.UpdateBenefitBillClaim command)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files[0].Length > 0)
            {
                var billAttachment = Request.Form.Files?[0];

                var settings = _settings.Value;

                command.BillAttachment = billAttachment;
                command.Settings = settings;
            }
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteBenefitBillClaim command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveBillClaim([FromBody] Commands.V1.ApproveBenefitBillClaim command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("decline-bill-claim")]
        public async Task<IActionResult> DeclineBillClaim([FromBody] Commands.V1.RejectBenefitBillClaim command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("mark-paid")]
        //[Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> MarkAsPaid([FromBody] Commands.V1.MarkAsPaidBenefitBillClaim command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("mark-settled")]
        //[Authorize(StandardPermissionProvider.ViewEmployeePermission)]
        public async Task<IActionResult> MarkAsSettled([FromBody] Commands.V1.MarkAsSettledBenefitBillClaim command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }
        #endregion
    }
}
