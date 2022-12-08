using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateAttendanceProcessedData, AttendanceProcessedDataCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<AttendanceEntities.AttendanceProcessedData, Guid>
        _repository;

        public async Task<AttendanceProcessedDataCommandVM> Handle(Commands.V1.CreateAttendanceProcessedData request, CancellationToken cancellationToken)
        {
            var response = new AttendanceProcessedDataCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = AttendanceEntities.AttendanceProcessedData.Create(
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
                         request.Status,
                         request.Status_V2,
                         request.BuyerShiftIn,
                         request.BuyerShiftOut,
                         request.BuyerOTTime,
                         request.Remarks,
                         request.IsLunchOut,
                         request.FinancialYearId,
                         request.MonthCycleId,
                         null, null, null, null,
                         "","",""
                );
                var data = await _repository.AddAsync(entity);

                response.Status = true;
                response.Message = "entity created successfully.";
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
