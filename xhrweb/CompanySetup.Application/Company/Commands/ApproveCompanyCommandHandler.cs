using CompanySetup.Application.Company.Commands.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities.CompanyAggregate;
using CompanySetup.Core.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CompanySetup.Application.Company.Commands
{
    public class ApproveCompanyCommandHandler : IRequestHandler<CompanyCommands.V1.ApproveCompanyCommand, CompanyCommandVM>
    {
        public ApproveCompanyCommandHandler(IAsyncRepository<CompanyEntities.Company, Guid> repository,
            IServiceBus serviceBus, IConfiguration configuration)
        {
            _repository = repository;
            _serviceBus = serviceBus;
            _configuration = configuration;
        }

        private readonly IAsyncRepository<CompanyEntities.Company, Guid> _repository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        public async Task<CompanyCommandVM> Handle(CompanyCommands.V1.ApproveCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new CompanyCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.ApproveCompany
                    (
                        request.Notes ?? entity.Notes
                    );

                await _repository.UpdateAsync(entity).ConfigureAwait(false);

                // publish the event in message queue.

                var @event = new Core.Events.ApproveCompanyEvent
                {
                    CompanyId = entity.Id,
                    CompanyName = entity.CompanyName,
                    CommandPublished = DateTime.Now
                };

                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                response.Status = true;
                response.Message = "company approved successfully.";
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
