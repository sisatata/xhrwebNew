using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeEducation.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeEducation, EmployeeEducationCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeEducation, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeEducation, Guid> _repository;

        public async Task<EmployeeEducationCommandVM>
            Handle(Commands.V1.UpdateEmployeeEducation request, CancellationToken cancellationToken)
        {
            var response = new EmployeeEducationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeEducation(

                         request.EmployeeId,
                         request.EducationalDegreeId,
                         request.EducationalInstituteId,
                         request.Session,
                         request.PassingYear,
                         request.BoardorUniversity,
                         request.Result,
                         request.ResultType,
                         request.OutOf,
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

