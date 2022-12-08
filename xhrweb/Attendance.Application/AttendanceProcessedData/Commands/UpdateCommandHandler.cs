using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateAttendanceProcessedData, AttendanceProcessedDataCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid> _repository;

        public async Task<AttendanceProcessedDataCommandVM>
            Handle(Commands.V1.UpdateAttendanceProcessedData request, CancellationToken cancellationToken)
        {
            var response = new AttendanceProcessedDataCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateAttendanceProcessedData(

                         request.EmployeeId,
                         request.PunchDate,
                         request.PunchYear,
                         request.PunchMonth,
                         request.TimeIn,
                         request.TimeOut,
                         request.ShiftIn,
                         request.ShiftOut,
                         request.BreakIn,
                         request.BreakOut,
                         request.BreakLate,
                         request.Late,
                         request.ShiftId,
                         request.ShiftCode,
                         request.RegularHour,
                         request.OTHour,
                         request.Status_V2,
                         request.Status,
                         request.BuyerShiftIn,
                         request.BuyerShiftOut,
                         request.BuyerOTTime,
                         request.Remarks,
                         request.IsLunchOut,
                         request.FinancialYearId,
                         request.MonthCycleId,
                         null,null,null,null,"","",""

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

