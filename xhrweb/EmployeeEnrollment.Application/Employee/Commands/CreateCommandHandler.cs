using EmployeeEnrollment.Core.Entities.EmployeeAggregate;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using EmployeeStatusCommand = EmployeeEnrollment.Application.EmployeeStatusHistory.Commands;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployee, EmployeeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.Employee, Guid>
        repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IAsyncRepository<EmployeeEntities.Employee, Guid> _repository;
        private readonly IMediator _mediator;

        public async Task<EmployeeCommandVM>
        Handle(Commands.V1.CreateEmployee request, CancellationToken cancellationToken)
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

                employeeAggregate.NewEmployeeCreate(
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
                var data = await _repository.AddAsync(employeeAggregate._employee).ConfigureAwait(false);

                var employeeStatusHistory = await _mediator.Send(new EmployeeStatusCommand.Commands.V1.CreateEmployeeStatusHistory
                {
                    EmployeeId = data.Id,
                    StatusId = 1,
                    ChangedDate = DateTime.Now,
                    Remarks = "Initial Value set"
                }).ConfigureAwait(false);

                response.Status = true;
                response.Message = "entity created successfully.";
                response.Id = data.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}




