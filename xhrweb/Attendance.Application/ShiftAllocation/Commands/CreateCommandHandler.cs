using Attendance.Application.ShiftAllocation.Commands.Models;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using Attendance.Core.Interfaces;
using Attendance.Core.Specifications;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftAllocation.Commands
{
    public class CreateCommandHandler : IRequestHandler<ShiftAllocationCommands.V1.SetShiftAllocation, ShiftAllocationCommandVM>
    {


        public CreateCommandHandler(IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> repository)
        {
            _repository = repository;

        }
        private readonly IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> _repository;


        public async Task<ShiftAllocationCommandVM> Handle(ShiftAllocationCommands.V1.SetShiftAllocation request, CancellationToken cancellationToken)
        {
            var response = new ShiftAllocationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {

                var shiftAllocationFilter = new ShiftAllocationByEmployeeFilterSpecification(request.CompanyId, request.EmployeeId);
                var shiftAllocations = await _repository.ListAsync(shiftAllocationFilter).ConfigureAwait(false);


                var shiftAllocationAggregate = new ShiftAllocationAggregate(request.CompanyId, request.EmployeeId, shiftAllocations);

                shiftAllocationAggregate.NewShiftAllocation(request.FinancialYearId, request.DutyYear, request.MonthCycleId,
                    request.DutyMonth, request.PrimaryShiftId, request.RotationDay, request.C1, request.C2, request.C3, request.C4, request.C5,
                    request.C6, request.C7, request.C8, request.C9, request.C10, request.C11, request.C12, request.C13, request.C14, request.C15, request.C16
                    , request.C17, request.C18, request.C19, request.C20, request.C21, request.C22, request.C23, request.C24, request.C25, request.C26,
                    request.C27, request.C28, request.C29, request.C30, request.C31, request.IsDeleted);

                var allocation = await _repository.AddAsync(shiftAllocationAggregate.ShiftAllocation);
                response.Status = true;
                response.Message = "entity created successfully.";
                response.Id = allocation.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;



        }

    }
}
