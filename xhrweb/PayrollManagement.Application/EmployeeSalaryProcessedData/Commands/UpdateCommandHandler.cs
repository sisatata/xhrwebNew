using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeSalaryProcessedData, EmployeeSalaryProcessedDataCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedData, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedData, Guid> _repository;

        public async Task<EmployeeSalaryProcessedDataCommandVM>
            Handle(Commands.V1.UpdateEmployeeSalaryProcessedData request, CancellationToken cancellationToken)
        {
            var response = new EmployeeSalaryProcessedDataCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateEmployeeSalaryProcessedData(

                         request.EmployeeId,
                         request.FinancialYearId,
                         request.MonthCycleId,
                         request.PaymentOptionId,
                         request.CompanyId,
                         request.BranchId,
                         request.DepartmentId,
                         request.PositionId,
                         request.GradeId,
                         request.TotalDaysInMonth,
                         request.TotalWorkingDays,
                         request.TotalPresentDays,
                         request.TotalAbsentDays,
                         request.TotalLeaveDays,
                         request.WeeklyOffDays,
                         request.GovernmentOffDays,
                         request.TotalWorkingHoliday,
                         request.OTHour.ToString(),
                         request.OTRate,
                         request.GrossSalary,
                         request.PayableSalary,
                         request.TotalDeductionAmount,
                         request.NetPayableAmount,
                         request.ProcessDate,
                         request.StampCost,
                         request.IsDeleted,
                         request.EmloyeeSalaryId,
                         request.IsApproved,
                         request.ApprovedBy,
                         request.ApprovedTime,
                         request.Remarks

    );

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
                //response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

