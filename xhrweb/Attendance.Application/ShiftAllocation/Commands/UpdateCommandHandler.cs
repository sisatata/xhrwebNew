using Attendance.Application.ShiftAllocation.Commands.Models;
using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftAllocation.Commands
{
   public  class UpdateCommandHandler: IRequestHandler<ShiftAllocationCommands.V1.UpdateShiftAllocation, ShiftAllocationCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> _repository;

        public UpdateCommandHandler(IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftAllocationCommandVM> Handle(ShiftAllocationCommands.V1.UpdateShiftAllocation request, CancellationToken cancellationToken)
        {
            var response = new ShiftAllocationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);

                entity.UpdateShiftAllocation
                    (
                        
                        request.FinancialYearId,
                        request.DutyYear,
                        request.MonthCycleId,
                        request.DutyMonth,
                       (request.EmployeeId==Guid.Empty || request.EmployeeId==null ) ? entity.EmployeeId : request.EmployeeId,
                       (request.CompanyId == Guid.Empty || request.CompanyId == null) ? entity.CompanyId : request.CompanyId,
                       request.PrimaryShiftId, request.RotationDay,
                       request.C1, request.C2,request.C3,request.C4,request.C5,
                       request.C6, request.C7, request.C8, request.C9, request.C10,
                       request.C11, request.C12, request.C13, request.C14, request.C15,
                       request.C16, request.C17, request.C18, request.C19, request.C20,
                       request.C21, request.C22, request.C23, request.C24, request.C25,
                       request.C26, request.C27, request.C28, request.C29, request.C30, 
                       request.C31, request.IsDeleted
                    );

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
                response.Id = entity.Id;
            }
            catch (Exception ex) { response.Message = ex.Message; }
            return response;
        }
    }
}
