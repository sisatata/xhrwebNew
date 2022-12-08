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
  public   class MarkAsDeleteCommandHandler: IRequestHandler<ShiftAllocationCommands.V1.MarkShiftAllocationAsDelete, ShiftAllocationCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> _repository;

        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftAllocationCommandVM> Handle(ShiftAllocationCommands.V1.MarkShiftAllocationAsDelete request, CancellationToken cancellationToken)
        {
            var response = new ShiftAllocationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkShiftAllocationAsDeleted();
                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity deleted successfully.";
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
