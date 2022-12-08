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
    public class UpdateCommandHandler : IRequestHandler<ShiftGroupCommands.V1.UpdateShiftGroup, ShiftGroupCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.ShiftGroup, Guid> _repository;

        public UpdateCommandHandler(IAsyncRepository<Core.Entities.ShiftGroup, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftGroupCommandVM> Handle(ShiftGroupCommands.V1.UpdateShiftGroup request, CancellationToken cancellationToken)
        {
            var response = new ShiftGroupCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);

                entity.UpdateShiftGroup
                    (
                        (request.CompanyId == Guid.Empty || request.CompanyId == null) ? entity.CompanyId : request.CompanyId,
                        request.ShiftGroupName ?? entity.ShiftGroupName,
                        request.ShiftGroupNameLC ?? entity.ShiftGroupNameLC
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
