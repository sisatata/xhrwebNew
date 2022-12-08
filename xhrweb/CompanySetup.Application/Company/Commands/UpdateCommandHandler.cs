using CompanySetup.Application.Company.Commands.Models;
using CompanySetup.Core.Entities.CompanyAggregate;
using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities.CompanyAggregate;

namespace CompanySetup.Application.Company.Commands
{
    public class UpdateCommandHandler : IRequestHandler<CompanyCommands.V1.UpdateCompany, CompanyCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<CompanyEntities.Company, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.Company, Guid> _repository;

        public async Task<CompanyCommandVM> Handle(CompanyCommands.V1.UpdateCompany request, CancellationToken cancellationToken)
        {
            var response = new CompanyCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var companyFilterSpecification = new CompanyFilterSpecification();
                var companyList = await _repository.ListAsync(companyFilterSpecification).ConfigureAwait(false);

                var companyAggregate = new CompanyAggregate(companyList);

                var entity = await _repository.GetByIdAsync(request.Id);
                companyAggregate.Company = entity;

                companyAggregate.StartCompanyUpdate
                    (
                        request.CompanyName ?? entity.CompanyName, 
                        request.ShortName ?? entity.ShortName, 
                        request.CompanyLocalizedName ?? entity.CompanyLocalizedName, 
                        request.CompanySlogan ?? entity.CompanySlogan, 
                        request.LicenseKey ?? entity.LicenseKey, 
                        request.SortOrder == 0 ? entity.SortOrder : request.SortOrder,
                        request.CompanyWebsite?? entity.CompanyWebsite,
                        request.FacebookLink?? entity.FacebookLink
                    );
                await _repository.UpdateAsync(companyAggregate.Company);

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
