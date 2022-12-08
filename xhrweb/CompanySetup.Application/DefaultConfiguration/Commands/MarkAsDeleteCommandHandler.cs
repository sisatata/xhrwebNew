using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DefaultEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.DefaultConfiguration.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteDefaultConfiguration, DefaultConfigurationCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<DefaultEntities.DefaultConfiguration, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<DefaultEntities.DefaultConfiguration, Guid> _repository;

        public async Task<DefaultConfigurationCommandVM>
            Handle(Commands.V1.MarkAsDeleteDefaultConfiguration request, CancellationToken cancellationToken)
        {
            var response = new DefaultConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteDefaultConfiguration();

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
