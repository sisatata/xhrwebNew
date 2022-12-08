using Attendance.Application.ShiftGroup.Commands.Models;
using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftGroup.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<ShiftGroupCommands.V1.MarkShiftGroupAsDelete, ShiftGroupCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.ShiftGroup, Guid> _repository;

        public MarkAsDeleteCommandHandler(IAsyncRepository<Core.Entities.ShiftGroup,Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftGroupCommandVM> Handle(ShiftGroupCommands.V1.MarkShiftGroupAsDelete request, CancellationToken cancellationToken)
        {
            var response = new ShiftGroupCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkShiftGroupAsDeleted();
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
