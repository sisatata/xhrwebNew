using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.DefaultConfiguration.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateDefaultConfiguration, DefaultConfigurationCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<CompanyEntities.DefaultConfiguration, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.DefaultConfiguration, Guid> _repository;

        public async Task<DefaultConfigurationCommandVM>
            Handle(Commands.V1.UpdateDefaultConfiguration request, CancellationToken cancellationToken)
        {
            var response = new DefaultConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateDefaultConfiguration(


                         request.Code,
                         request.DefaultValue,
                         request.Description,
                         request.IsDeleted

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

