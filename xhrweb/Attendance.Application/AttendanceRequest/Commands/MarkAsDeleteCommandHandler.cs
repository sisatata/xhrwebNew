using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.AttendanceRequest.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteAttendanceRequest, AttendanceRequestCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> _repository;

        public async Task<AttendanceRequestCommandVM>
            Handle(Commands.V1.MarkAsDeleteAttendanceRequest request, CancellationToken cancellationToken)
        {
            var response = new AttendanceRequestCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteAttendanceRequest();

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
