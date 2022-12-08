using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = Attendance.Core.Entities;

namespace Attendance.Application.Holiday.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateHoliday, HolidayCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.Holiday, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.Holiday, Guid> _repository;

        public async Task<HolidayCommandVM>
            Handle(Commands.V1.UpdateHoliday request, CancellationToken cancellationToken)
        {
            var response = new HolidayCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateHoliday(
                         request.HolidayDate,
                         request.StartDate,
                         request.EndDate,
                         request.Reason,
                         request.ReasonLocalized,
                         request.CompanyId

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

