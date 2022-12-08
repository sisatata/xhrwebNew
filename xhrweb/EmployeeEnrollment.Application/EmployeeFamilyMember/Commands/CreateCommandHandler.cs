using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeFamilyMember.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeFamilyMember, EmployeeFamilyMemberCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeFamilyMember, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeFamilyMember, Guid>
        _repository;

        public async Task<EmployeeFamilyMemberCommandVM> Handle(Commands.V1.CreateEmployeeFamilyMember request, CancellationToken cancellationToken)
        {
            var response = new EmployeeFamilyMemberCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeFamilyMember.Create(
                         request.EmployeeId,
                         request.FamiliyMemberName,
                         request.DateOfBirth,
                         request.EducationClass,
                         request.EducationalInstitute,
                         request.EducationYear,
                         request.RelationTypeId,
                         request.IsDependant,
                         request.IsEligibleForMedical,
                         request.IsEligibleForEducation,
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
