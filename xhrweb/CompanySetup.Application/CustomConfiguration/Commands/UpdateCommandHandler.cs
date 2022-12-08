using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CustomEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.CustomConfiguration.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateCustomConfiguration, CustomConfigurationCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<CustomEntities.CustomConfiguration, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CustomEntities.CustomConfiguration, Guid> _repository;

        public async Task<CustomConfigurationCommandVM>
            Handle(Commands.V1.UpdateCustomConfiguration request, CancellationToken cancellationToken)
        {
            var response = new CustomConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateCustomConfiguration(


                         request.Code,
                         request.CustomValue,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted,
                         request.CompanyId

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

