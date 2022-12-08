using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using EmployeeEnrollment.Core.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployee, EmployeeDeleteCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.Employee, Guid>
            repository, IServiceBus serviceBus,
            IConfiguration configuration)
        {
            _repository = repository;
            _serviceBus = serviceBus;
            _configuration = configuration;
        }

        private readonly IAsyncRepository<EmployeeEntities.Employee, Guid>
            _repository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        public async Task<EmployeeDeleteCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployee request, CancellationToken cancellationToken)
        {
            var response = new EmployeeDeleteCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDelete();

                await _repository.UpdateAsync(entity);

                //var @event = new Core.Events.EmployeeDeleteEvent
                //{
                //    LoginId = entity.LoginId.Value,
                //    CommandPublished = DateTime.Now,
                //    UserId = ""
                //};

                //await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                response.Status = true;
                response.Message = "entity deleted successfully.";
                response.Id = entity.Id;
                response.LoginId = entity.LoginId.HasValue ? entity.LoginId.Value : Guid.Empty;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}




