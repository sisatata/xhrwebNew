using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeCustomConfiguration, EmployeeCustomConfigurationCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeCustomConfiguration, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeCustomConfiguration, Guid> _repository;

        public async Task<EmployeeCustomConfigurationCommandVM>
            Handle(Commands.V1.UpdateEmployeeCustomConfiguration request, CancellationToken cancellationToken)
        {
            var response = new EmployeeCustomConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeCustomConfiguration(
                         request.Code,
                         request.CustomValue,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted,
                         request.EmployeeId

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

