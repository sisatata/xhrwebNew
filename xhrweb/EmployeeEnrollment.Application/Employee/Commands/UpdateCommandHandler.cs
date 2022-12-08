using EmployeeEnrollment.Core.Entities.EmployeeAggregate;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployee, EmployeeCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.Employee, Guid>
            repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.Employee, Guid>
            _repository;

        public async Task<EmployeeCommandVM>
            Handle(Commands.V1.UpdateEmployee request, CancellationToken cancellationToken)
        {
            var response = new EmployeeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var employeeFilterSpecification = new EmployeeByCompanyFilterSpecification(request.CompanyId);

                var employeeList = await _repository.ListAsync(employeeFilterSpecification).ConfigureAwait(false);

                var employeeAggregate = new EmployeeAggregate(employeeList);

                var entity = await _repository.GetByIdAsync(request.Id);
                employeeAggregate._employee = entity;
                //employeeAggregate = new EmployeeAggregate(entity);

                employeeAggregate.StartEmployeeUpdate(
                         request.EmployeeId,
                         request.CompanyId,
                         request.BranchId,
                         request.DepartmentId,
                         request.PositionId,
                         request.FullName,
                         request.FullNameLC,
                         request.DateOfBirth,
                         request.ReferenceNumber,
                         request.NationalityId,
                         request.GenderId

    );

                await _repository.UpdateAsync(entity).ConfigureAwait(false);

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




