using CompanySetup.Application.Company.Commands.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities.CompanyAggregate;
using CompanySetup.Core.Interfaces;

namespace CompanySetup.Application.Company.Commands
{
    public class RejectCompanyCommandHandler : IRequestHandler<CompanyCommands.V1.RejectCompanyCommand, CompanyCommandVM>
    {
        public RejectCompanyCommandHandler(IAsyncRepository<CompanyEntities.Company, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.Company, Guid> _repository;
        public async Task<CompanyCommandVM> Handle(CompanyCommands.V1.RejectCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new CompanyCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.RejectCompany
                    (
                        request.Notes ?? entity.Notes
                    );
                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "company declined successfully.";
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
