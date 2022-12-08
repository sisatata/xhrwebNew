using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.CompanyPhone.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateCompanyPhone, CompanyPhoneCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<CompanyEntities.CompanyPhone, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CompanyPhone, Guid> _repository;

        public async Task<CompanyPhoneCommandVM>
            Handle(Commands.V1.UpdateCompanyPhone request, CancellationToken cancellationToken)
        {
            var response = new CompanyPhoneCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateCompanyPhone(
                         request.CompanyId,
                         request.PhoneNumber,
                         request.PhoneTypeId

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

