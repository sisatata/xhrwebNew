using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeePhone.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeePhone, EmployeePhoneCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeePhone, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeePhone, Guid>
        _repository;

        public async Task<EmployeePhoneCommandVM> Handle(Commands.V1.CreateEmployeePhone request, CancellationToken cancellationToken)
        {
            var response = new EmployeePhoneCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity =  EmployeeEntities.EmployeePhone.Create(
                         request.EmployeeId,
                         request.PhoneNumber,
                         request.PhoneTypeId
            


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
