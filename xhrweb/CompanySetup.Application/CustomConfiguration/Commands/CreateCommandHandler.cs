using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.CustomConfiguration.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateCustomConfiguration, CustomConfigurationCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.CustomConfiguration, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.CustomConfiguration, Guid>
        _repository;

        public async Task<CustomConfigurationCommandVM> Handle(Commands.V1.CreateCustomConfiguration request, CancellationToken cancellationToken)
        {
            var response = new CustomConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = CompanyEntities.CustomConfiguration.Create(
                         
                         request.Code,
                         request.CustomValue,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted,
                         request.CompanyId
            


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
