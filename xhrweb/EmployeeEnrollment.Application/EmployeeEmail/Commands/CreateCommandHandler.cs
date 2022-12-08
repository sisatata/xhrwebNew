using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeEmail.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeEmail, EmployeeEmailCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeEmail, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeEmail, Guid>
        _repository;

        public async Task<EmployeeEmailCommandVM> Handle(Commands.V1.CreateEmployeeEmail request, CancellationToken cancellationToken)
        {
            var response = new EmployeeEmailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity =  EmployeeEntities.EmployeeEmail.Create(
                         request.EmployeeId,
                         request.EmailAddress,
                         request.IsPrimary
            


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
