using ASL.Hrms.SharedKernel.Enums;
using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.AttendanceRequest.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateAttendanceRequest, AttendanceRequestCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> _repository;

        public async Task<AttendanceRequestCommandVM>
            Handle(Commands.V1.UpdateAttendanceRequest request, CancellationToken cancellationToken)
        {
            var response = new AttendanceRequestCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateAttendanceRequest(
                         request.EmployeeId,
                         request.RequestTypeId,
                         request.StartTime,
                         request.EndTime,
                         request.Reason,
                         request.CompanyId,
                          ((int)ApprovalStatuses.Pending).ToString()

    );

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
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

