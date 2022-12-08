using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeStatus.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeStatus, EmployeeStatusCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeStatus, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeStatus, Guid> _repository;

        public async Task<EmployeeStatusCommandVM>
            Handle(Commands.V1.UpdateEmployeeStatus request, CancellationToken cancellationToken)
        {
            var response = new EmployeeStatusCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeStatus(
                         request.EmployeeStatusName,
                         request.EmployeeStatusNameLC,
                         request.Rank,
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

