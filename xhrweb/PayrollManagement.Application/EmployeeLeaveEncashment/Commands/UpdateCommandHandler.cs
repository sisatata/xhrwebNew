using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeLeaveEncashment.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeLeaveEncashment, EmployeeLeaveEncashmentCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeLeaveEncashment, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeLeaveEncashment, Guid> _repository;

        public async Task<EmployeeLeaveEncashmentCommandVM>
            Handle(Commands.V1.UpdateEmployeeLeaveEncashment request, CancellationToken cancellationToken)
        {
            var response = new EmployeeLeaveEncashmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateEmployeeLeaveEncashment(
                         request.EmployeeId,
                         request.LeaveTypeId,
                         request.EncashDate,
                         request.FinancialYearId,
                         request.MonthCycleId,
                         request.ELEncashedDays,
                         request.EncashedAmount,
                         request.Remarks

    );

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
                response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

