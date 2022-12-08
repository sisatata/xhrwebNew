using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeDetail.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeDetail, EmployeeDetailCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeDetail, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeDetail, Guid>
        _repository;

        public async Task<EmployeeDetailCommandVM> Handle(Commands.V1.CreateEmployeeDetail request, CancellationToken cancellationToken)
        {
            var response = new EmployeeDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity =  EmployeeEntities.EmployeeDetail.Create(
                         request.EmployeeId,
                         request.FathersName,
                         request.MothersName,
                         request.SpouseName,
                         request.MaritalStatusId,
                         request.ReligionId,
                         request.NID,
                         request.BID,
                         request.BloodGroupId
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
