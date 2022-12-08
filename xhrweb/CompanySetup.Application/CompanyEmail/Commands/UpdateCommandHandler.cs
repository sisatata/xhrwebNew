using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.CompanyEmail.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateCompanyEmail, CompanyEmailCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<CompanyEntities.CompanyEmail, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CompanyEmail, Guid> _repository;

        public async Task<CompanyEmailCommandVM>
            Handle(Commands.V1.UpdateCompanyEmail request, CancellationToken cancellationToken)
        {
            var response = new CompanyEmailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateCompanyEmail(
                         request.CompanyId,
                         request.EmailAddress,
                         request.Remarks,
                         request.IsPrimary

    );

                await _repository.UpdateAsync(entity);

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

