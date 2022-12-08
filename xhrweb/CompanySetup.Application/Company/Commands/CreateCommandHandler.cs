using CompanySetup.Application.Company.Commands.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities.CompanyAggregate;
using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using CompanySetup.Core.Entities.CompanyAggregate;

namespace CompanySetup.Application.Company.Commands
{
    public class CreateCommandHandler : IRequestHandler<CompanyCommands.V1.CreateCompany, CompanyCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.Company, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.Company, Guid> _repository;
        public async Task<CompanyCommandVM> Handle(CompanyCommands.V1.CreateCompany request, CancellationToken cancellationToken)
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

                companyAggregate.NewCompanyCreate(request.CompanyName, request.ShortName, 
                                    request.CompanyLocalizedName, request.CompanySlogan, request.LicenseKey, request.SortOrder, request.CompanyWebsite, request.FacebookLink);

                var data = await _repository.AddAsync(companyAggregate.Company);

                response.Status = true;
                response.Message = "entity created successfully.";
                response.Id = data.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
