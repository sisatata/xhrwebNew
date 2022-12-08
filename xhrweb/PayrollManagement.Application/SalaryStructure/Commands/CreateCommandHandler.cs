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
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateSalaryStructure, SalaryStructureCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.SalaryStructure, Guid>
        repository,
            IServiceBus serviceBus,
            IConfiguration configuration,
            ICurrentUserContext userContext)
        {
            _repository = repository;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _userContext = userContext;

        }

        private readonly IAsyncRepository<PayrollEntities.SalaryStructure, Guid>
        _repository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserContext _userContext;

        public async Task<SalaryStructureCommandVM> Handle(Commands.V1.CreateSalaryStructure request, CancellationToken cancellationToken)
        {
            var response = new SalaryStructureCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.SalaryStructure.Create(
                         request.StructureName,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted,
                         request.CompanyId
                );
                var data = await _repository.AddAsync(entity);

                var @event = new PayrollActivityLogEvent
                {
                    SystemKeyword = "Payroll",
                    Comment = $"New salary {request.StructureName} for {request.StartDate} to {request.EndDate} created",
                    //Entity = employeeBonusProcessAggregate,
                    CommandPublished = DateTime.Now,
                    LoginUserId = _userContext.CurrentUserId,
                    CurrentUserCompanyId = _userContext.CurrentUserCompanyId
                };
                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);


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
