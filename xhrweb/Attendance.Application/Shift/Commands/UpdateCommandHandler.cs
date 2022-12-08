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
    public class UpdateCommandHandler: IRequestHandler<ShiftCommands.V1.UpdateShift, ShiftCommandVM>
    {
        private readonly IAsyncRepository<Core.Entities.Shift, Guid> _repository;

        public UpdateCommandHandler(IAsyncRepository<Core.Entities.Shift, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ShiftCommandVM> Handle(ShiftCommands.V1.UpdateShift request, CancellationToken cancellationToken)
        {
            var response = new ShiftCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);

                entity.UpdateShift
                    (    
                    request.CompanyId,
                    request.ShiftGroupID, 
                    request.ShiftName,
                    request.ShiftLocalizedName,
                    request.ShiftCode,
                    request.IsActive, 
                    request.ShiftIn,
                    request.ShiftOut,
                    request.ShiftLate, 
                    request.LunchBreakIn, 
                    request.LunchBreakOut, 
                    request.EarlyOut, 
                    request.RegHour,
                    request.RamadanIn,
                    request.RamadanOut,
                    request.RamadanLate,
                    request.RamadanEarlyOut,
                    request.RamadanLunchBreakIn, 
                    request.RamadanLunchBreakOut,
                    request.StartTime, 
                    request.EndTime, 
                    request.GraceIn, 
                    request.GraceOut, 
                    request.Range, 
                    request.IsRollingShift, 
                    request.IsRelaborShift,
                    request.IsDeleted
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
