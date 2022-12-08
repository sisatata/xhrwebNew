using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeEducation.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeEducation, EmployeeEducationCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeEducation, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeEducation, Guid>
        _repository;

        public async Task<EmployeeEducationCommandVM> Handle(Commands.V1.CreateEmployeeEducation request, CancellationToken cancellationToken)
        {
            var response = new EmployeeEducationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeEducation.Create(
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
