using Attendance.Application.Shift.Commands.Models;
using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.Shift.Commands
{
  public   class CreateCommandHandler: IRequestHandler<ShiftCommands.V1.ShiftConfiguration, ShiftCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.Shift, Guid> _repository;

        public CreateCommandHandler(IAsyncRepository<Core.Entities.Shift, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftCommandVM> Handle(ShiftCommands.V1.ShiftConfiguration request, CancellationToken cancellationToken)
        {
            var response = new ShiftCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = Core.Entities.Shift.ShiftConfiguration(request.CompanyId,request.ShiftGroupID,request.ShiftName,request.ShiftLocalizedName,request.ShiftCode,
                    request.IsActive,request.ShiftIn,request.ShiftOut,request.ShiftLate,request.LunchBreakIn,request.LunchBreakOut,request.EarlyOut,request.RegHour,
                    request.RamadanIn,request.RamadanOut,request.RamadanLate,request.RamadanEarlyOut,request.RamadanLunchBreakIn,request.RamadanLunchBreakOut,request.StartTime
                    ,request.EndTime,request.GraceIn,request.GraceOut,request.Range,request.IsRollingShift,request.IsRelaborShift,request.IsDeleted);

                await _repository.AddAsync(entity);
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
