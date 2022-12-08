using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = Attendance.Core.Entities;

namespace Attendance.Application.Holiday.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateHoliday, HolidayCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.Holiday, Guid> repository,
            IServiceBus serviceBus,
            IConfiguration configuration)
        {
            _repository = repository;
            _serviceBus = serviceBus;
            _configuration = configuration;
        }

        private readonly IAsyncRepository<EmployeeEntities.Holiday, Guid>
        _repository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        public async Task<HolidayCommandVM> Handle(Commands.V1.CreateHoliday request, CancellationToken cancellationToken)
        {
            var response = new HolidayCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.Holiday.Create(

                         request.HolidayDate,
                         request.StartDate,
                         request.EndDate,
                         request.Reason,
                         request.ReasonLocalized,
                         request.CompanyId
                );
                var data = await _repository.AddAsync(entity);
                if (request.HolidayDate.Date <= DateTime.Now.AddDays(2).Date)
                {
                    var @event = new Core.Events.HolidayPushNotificationProcessEvent
                    {
                        CompanyId = request.CompanyId,
                        HolidayDate = request.HolidayDate,
                        CommandPublished = DateTime.Now
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);
                }
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
