using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using PayrollManagement.Core.ApplicationEvents;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PayrollManagement.Application.SalaryStructure.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteSalaryStructure, SalaryStructureCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayrollEntities.SalaryStructure, Guid> repository,
            IServiceBus serviceBus,
            IConfiguration configuration,
            ICurrentUserContext userContext)
        {
            _repository = repository;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _userContext = userContext;
        }

        private readonly IAsyncRepository<PayrollEntities.SalaryStructure, Guid> _repository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserContext _userContext;
        public async Task<SalaryStructureCommandVM>
            Handle(Commands.V1.MarkAsDeleteSalaryStructure request, CancellationToken cancellationToken)
        {
            var response = new SalaryStructureCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteSalaryStructure();

                await _repository.UpdateAsync(entity);

                var @event = new PayrollActivityLogEvent
                {
                    SystemKeyword = "Payroll",
                    Comment = $"Deleted salary {entity.StructureName} for {entity.StartDate} to {entity.EndDate}",
                    //Entity = employeeBonusProcessAggregate,
                    CommandPublished = DateTime.Now,
                    LoginUserId = _userContext.CurrentUserId,
                    CurrentUserCompanyId = _userContext.CurrentUserCompanyId
                };
                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);


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
