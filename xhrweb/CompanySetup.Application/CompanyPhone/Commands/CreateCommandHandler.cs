using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.CompanyPhone.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateCompanyPhone, CompanyPhoneCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.CompanyPhone, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CompanyPhone, Guid>
        _repository;

        public async Task<CompanyPhoneCommandVM> Handle(Commands.V1.CreateCompanyPhone request, CancellationToken cancellationToken)
        {
            var response = new CompanyPhoneCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = CompanyEntities.CompanyPhone.Create(
                         request.CompanyId,
                         request.PhoneNumber,
                         request.PhoneTypeId
                );
                var data = await _repository.AddAsync(entity);

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
