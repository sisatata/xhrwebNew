using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using System.Linq;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Commands
{
    public class UpdateStatusEmployeeHandler : IRequestHandler<Commands.V1.UpdateStatusEmployee, EmployeeEnrollmentCommandVM>
    {
        public UpdateStatusEmployeeHandler(IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> _repository;

        public async Task<EmployeeEnrollmentCommandVM> Handle(Commands.V1.UpdateStatusEmployee request, CancellationToken cancellationToken)
        {
            var response = new EmployeeEnrollmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var employeeEnrollmentFilter = new EmployeeEnrollmentByEmployeeIdSpecification(request.Id);
                var employees = await _repository.ListAsync(employeeEnrollmentFilter);

                if (!employees.Any())
                    return response;

                var employee = employees.FirstOrDefault();
                employee.UpsertStatusId(request.StatusId);

                await _repository.UpdateAsync(employee);

                response.Status = true;
                response.Message = "Status Id updated successfully.";
                response.Id = employee.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}


