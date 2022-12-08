using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.CompanyEmail.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateCompanyEmail, CompanyEmailCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.CompanyEmail, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CompanyEmail, Guid>
        _repository;

        public async Task<CompanyEmailCommandVM> Handle(Commands.V1.CreateCompanyEmail request, CancellationToken cancellationToken)
        {
            var response = new CompanyEmailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = CompanyEntities.CompanyEmail.Create(
                         request.CompanyId,
                         request.EmailAddress,
                         request.Remarks,
                         request.IsPrimary
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
