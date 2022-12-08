using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.Attendance.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateAttendance, AttendanceCommandVM>
    {
        private readonly IAsyncRepository<AttendanceEntities.Attendance1, Guid> _repository1;
        public UpdateCommandHandler(IAsyncRepository<AttendanceEntities.Attendance1, Guid> repository) { _repository1 = repository; }

        private readonly IAsyncRepository<AttendanceEntities.Attendance2, Guid> _repository2;
        public UpdateCommandHandler(IAsyncRepository<AttendanceEntities.Attendance2, Guid> repository) { _repository2 = repository; }

        public async Task<AttendanceCommandVM>
            Handle(Commands.V1.UpdateAttendance request, CancellationToken cancellationToken)
        {
            var response = new AttendanceCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                switch (request.AttendanceDate.Value.Month)
                {
                    case 1:
                        var entity1 = await _repository1.GetByIdAsync(request.Id);
                        entity1.UpdateAttendance1(
                                 request.CardId,
                                 request.EmployeeId,
                                 request.AttendanceYear,
                                 request.AttendanceDate,
                                 request.Punchtype,
                                 request.OverNightMark,
                                 request.Latitude,
                                 request.Longitude,
                                 request.IsDeleted,
                         request.ClockTimeType,
                         request.Remarks,
                         request.ClockTimeAddress

            );

                        await _repository1.UpdateAsync(entity1);
                        response.Id = entity1.Id;
                        break;

                    case 2:
                        var entity2 = await _repository2.GetByIdAsync(request.Id);
                        entity2.UpdateAttendance2(
                                 request.CardId,
                                 request.EmployeeId,
                                 request.AttendanceYear,
                                 request.AttendanceDate,
                                 request.Punchtype,
                                 request.OverNightMark,
                                 request.Latitude,
                                 request.Longitude,
                                 request.IsDeleted,
                         request.ClockTimeType,
                         request.Remarks,
                         request.ClockTimeAddress

            );

                        await _repository2.UpdateAsync(entity2);
                        response.Id = entity2.Id;
                        break;

                    default:
                        break;
                }


                response.Status = true;
                response.Message = "entity updated successfully.";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

