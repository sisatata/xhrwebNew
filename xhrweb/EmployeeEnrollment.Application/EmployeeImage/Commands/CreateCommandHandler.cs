using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeImage.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeImage, EmployeeImageCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeImage, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeImage, Guid>
        _repository;

        public async Task<EmployeeImageCommandVM> Handle(Commands.V1.CreateEmployeeImage request, CancellationToken cancellationToken)
        {
            var response = new EmployeeImageCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity =  EmployeeEntities.EmployeeImage.Create(
                         request.EmployeeImageId,
                         request.EmployeeId,
                         request.FamilyMemberId,
                         request.Photo,
                         request.PhotoId,
                         request.CompanyId,
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
