using Attendance.Application.Shift.Commands.Models;
using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.Shift.Commands
{
   public  class MarkAsDeleteCommandHandler : IRequestHandler<ShiftCommands.V1.MarkShiftAsDelete, ShiftCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.Shift, Guid> _repository;

        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.Shift, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftCommandVM> Handle(ShiftCommands.V1.MarkShiftAsDelete request, CancellationToken cancellationToken)
        {
            var response = new ShiftCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkShiftAsDeleted();
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
