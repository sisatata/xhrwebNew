using Asl.Infrastructure.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using Payroll = PayrollManagement.Application.EmployeeSalary.Commands;
using EmployeeData = EmployeeEnrollment.Application.Employee.Commands;
using Attendance.Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asl.Infrastructure
{
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        public EmployeeSalaryService(IConfiguration configuration, IMediator mediator
            //,ILogger<PushNotificationService> logger
            )
        {
            _configuration = configuration;

            _mediator = mediator;
            //_logger = logger;
        }
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;
        //private readonly ILogger<PushNotificationService> _logger;
        public async Task<bool> AddNewEmployeeSalary(Guid? EmployeeId, Guid? BranchId,
            Guid? DepartmentId, Guid? PositionId,
            Guid? GradeId, Guid? SalaryStructureId,
            Int16? PaymentOptionId, decimal? GrossSalary,
            Guid? CompanyId,
            DateTime? StartDate, DateTime? EndDate,
            Boolean IsDeleted,
            Boolean isRequestFromPromotion = true)
        {
            var processingCommand = new Payroll.Commands.V1.CreateEmployeeSalary
            {
                CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                EmployeeId = (EmployeeId != null && EmployeeId != Guid.Empty) ? EmployeeId.Value : Guid.Empty,
                StartDate = StartDate.HasValue ? StartDate.Value.Date : DateTime.Now.Date,
                EndDate = EndDate.HasValue ? EndDate.Value.Date : DateTime.Now.Date.AddYears(1),
                BranchId = BranchId,
                DepartmentId = DepartmentId,
                PositionId = PositionId,
                GradeId = GradeId,
                PaymentOptionId = PaymentOptionId,
                SalaryStructureId = SalaryStructureId,
                GrossSalary = GrossSalary,
                IsDeleted = false,
                IsRequestFromPromotion = isRequestFromPromotion
            };
            await _mediator.Send(processingCommand);
            if (isRequestFromPromotion)
            {
                var employeeCommand = new EmployeeData.Commands.V1.UpdateEmployeeTransferData
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    Id = (EmployeeId != null && EmployeeId != Guid.Empty) ? EmployeeId.Value : Guid.Empty,
                    BranchId = BranchId,
                    DepartmentId = DepartmentId,
                    PositionId = (PositionId != null && PositionId != Guid.Empty) ? PositionId.Value : Guid.Empty

                };
                await _mediator.Send(employeeCommand);
            }

            return true;
        }
    }
}
