using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeePhone.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeePhone, EmployeePhoneCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeePhone, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeePhone, Guid> _repository;

        public async Task<EmployeePhoneCommandVM>
            Handle(Commands.V1.UpdateEmployeePhone request, CancellationToken cancellationToken)
        {
            var response = new EmployeePhoneCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeePhone(
                         request.EmployeeId,
                         request.PhoneNumber,
                         request.PhoneTypeId

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

