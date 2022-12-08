using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeDetail.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeDetail, EmployeeDetailCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeDetail, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeDetail, Guid> _repository;

        public async Task<EmployeeDetailCommandVM>
            Handle(Commands.V1.UpdateEmployeeDetail request, CancellationToken cancellationToken)
        {
            var response = new EmployeeDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeDetail(

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

