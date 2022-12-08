using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeCustomConfiguration, EmployeeCustomConfigurationCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeCustomConfiguration, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeCustomConfiguration, Guid>
        _repository;

        public async Task<EmployeeCustomConfigurationCommandVM> Handle(Commands.V1.CreateEmployeeCustomConfiguration request, CancellationToken cancellationToken)
        {
            var response = new EmployeeCustomConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeCustomConfiguration.Create(
                         request.Code,
                         request.CustomValue,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted,
                         request.EmployeeId
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
