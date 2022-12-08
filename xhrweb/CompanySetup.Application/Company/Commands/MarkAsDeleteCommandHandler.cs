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
    public class MarkAsDeleteCommandHandler : IRequestHandler<CompanyCommands.V1.MarkCompanyAsDelete, CompanyCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<CompanyEntities.Company, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.Company, Guid> _repository;

        public async Task<CompanyCommandVM> Handle(CompanyCommands.V1.MarkCompanyAsDelete request, CancellationToken cancellationToken)
        {
            var response = new CompanyCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkCompanyAsDeleted();
                await _repository.UpdateAsync(entity);

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
