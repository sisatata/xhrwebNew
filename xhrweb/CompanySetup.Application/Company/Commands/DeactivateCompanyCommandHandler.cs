using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Application.Company.Commands.Models;
using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities.CompanyAggregate;

namespace CompanySetup.Application.Company.Commands
{
    public class DeactivateCompanyCommandHandler : IRequestHandler<CompanyCommands.V1.DeactivateCompany, CompanyCommandVM>
    {
        public DeactivateCompanyCommandHandler(IAsyncRepository<CompanyEntities.Company, Guid> repository,
            IActivityLogService activityLogService)
        {
            _repository = repository;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<CompanyEntities.Company, Guid> _repository;
        private readonly IActivityLogService _activityLogService;

        public async Task<CompanyCommandVM> Handle(CompanyCommands.V1.DeactivateCompany request, CancellationToken cancellationToken)
        {
            var response = new CompanyCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id).ConfigureAwait(false);
                entity.DeactivateCompany();
                await _repository.UpdateAsync(entity).ConfigureAwait(false);

                response.Status = true;
                response.Message = "company deactivated successfully.";
                response.Id = entity.Id;
                await _activityLogService.InsertActivity("Company", $"{entity.CompanyName} has been deactivated", entity).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
