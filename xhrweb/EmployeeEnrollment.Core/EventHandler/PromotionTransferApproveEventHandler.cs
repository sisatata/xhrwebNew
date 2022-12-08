using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Core.EventHandler
{
    public class PromotionTransferApproveEventHandler : ICommandHandler<PromotionTransferApproveEvent>
    {
        public PromotionTransferApproveEventHandler(IConfiguration configuration,
            IEmployeeSalaryService employeeSalaryService)
        {
            _configuration = configuration;
            _employeeSalaryService = employeeSalaryService;
        }
        private readonly IConfiguration _configuration;
        private readonly IEmployeeSalaryService _employeeSalaryService;

        public async Task Handle(PromotionTransferApproveEvent command)
        {
            await _employeeSalaryService.AddNewEmployeeSalary(command.EmployeeId, command.BranchId, command.DepartmentId,
                command.PositionId, command.GradeId, command.SalaryStructureId, command.PaymentOptionId, command.GrossSalary,
                command.CompanyId, command.StartDate, command.EndDate, false,command.IsRequestFromPromotion);
        }
    }
}
