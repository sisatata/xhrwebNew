using Attendance.Application.ShiftGroup.Commands.Models;
using Attendance.Application.ShiftGroup.Queries.Models;
using Attendance.Core.Entities;
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
    public class CreateCommandHandler : IRequestHandler<ShiftGroupCommands.V1.CreateShiftGroup, ShiftGroupCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.ShiftGroup, Guid> _repository;

        public CreateCommandHandler(IAsyncRepository<Core.Entities.ShiftGroup,Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftGroupCommandVM> Handle(ShiftGroupCommands.V1.CreateShiftGroup request, CancellationToken cancellationToken)
        {
            var response = new ShiftGroupCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = Core.Entities.ShiftGroup.Create(request.CompanyId, request.ShiftGroupName, request.ShiftGroupNameLC);

                await _repository.AddAsync(entity);
                response.Status = true;
                response.Message = "entity created successfully.";
                response.Id = entity.Id;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
