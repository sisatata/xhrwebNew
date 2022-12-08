using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteAttendanceProcessedData, AttendanceProcessedDataCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid> _repository;

        public async Task<AttendanceProcessedDataCommandVM>
            Handle(Commands.V1.MarkAsDeleteAttendanceProcessedData request, CancellationToken cancellationToken)
        {
            var response = new AttendanceProcessedDataCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteAttendanceProcessedData();

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
