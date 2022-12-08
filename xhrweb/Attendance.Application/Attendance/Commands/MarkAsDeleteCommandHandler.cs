using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.Attendance.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteAttendance, AttendanceCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<AttendanceEntities.Attendance1, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<AttendanceEntities.Attendance1, Guid> _repository;

        public async Task<AttendanceCommandVM>
            Handle(Commands.V1.MarkAsDeleteAttendance request, CancellationToken cancellationToken)
        {
            var response = new AttendanceCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteAttendance1();

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
