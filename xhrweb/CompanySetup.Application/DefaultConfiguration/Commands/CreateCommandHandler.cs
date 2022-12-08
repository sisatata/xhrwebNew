using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DefaultEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.DefaultConfiguration.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateDefaultConfiguration, DefaultConfigurationCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<DefaultEntities.DefaultConfiguration, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<DefaultEntities.DefaultConfiguration, Guid>
        _repository;

        public async Task<DefaultConfigurationCommandVM> Handle(Commands.V1.CreateDefaultConfiguration request, CancellationToken cancellationToken)
        {
            var response = new DefaultConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = DefaultEntities.DefaultConfiguration.Create(
                         request.Code,
                         request.DefaultValue,
                         request.Description,
                         request.IsDeleted


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
