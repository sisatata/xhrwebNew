using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using PayrollManagement.Core.Specifications;
using PayrollManagement.Core.Entities.EmployeeSalaryAggregate;
using System.Linq;

namespace PayrollManagement.Application.EmployeeSalary.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeSalary, EmployeeSalaryCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> repository,
            IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> repositorySalaryStructureComponent,
            IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> repositoryEmployeeSalary,
            IAsyncRepository<PayrollEntities.EmployeeSalaryComponent, Guid> repositoryEmployeeSalaryComponent)
        {
            _repository = repository;
            _repositorySalaryStructureComponent = repositorySalaryStructureComponent;
            _repositoryEmployeeSalary = repositoryEmployeeSalary;
            _repositoryEmployeeSalaryComponent = repositoryEmployeeSalaryComponent;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> _repository;
        private readonly IAsyncRepository<PayrollEntities.SalaryStructureComponent, Guid> _repositorySalaryStructureComponent;
        private readonly IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> _repositoryEmployeeSalary;
        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryComponent, Guid> _repositoryEmployeeSalaryComponent;

        public async Task<EmployeeSalaryCommandVM>
            Handle(Commands.V1.UpdateEmployeeSalary request, CancellationToken cancellationToken)
        {
            var response = new EmployeeSalaryCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var salaryStructureComponent = new SalaryStructureComponentFilterSpecificaion(request.SalaryStructureId.Value);
                var salaryStructureComponentList = await _repositorySalaryStructureComponent.ListAsync(salaryStructureComponent).ConfigureAwait(false);

                var employeeSalary = new EmployeeSalaryFilterSpecificaion(request.EmployeeId.Value);
                var employeeSalaryList = await _repositoryEmployeeSalary.ListAsync(employeeSalary).ConfigureAwait(false);

                var employeeSalaryComponent = new EmployeeSalaryComponentFilterSpecificaion(request.CompanyId.Value, request.Id);
                var employeeSalaryComponentList = await _repositoryEmployeeSalaryComponent.ListAsync(employeeSalaryComponent).ConfigureAwait(false);

                var employeeSalaryAggregate = new EmployeeSalaryAggregate(employeeSalaryList.ToList(), salaryStructureComponentList.ToList(), employeeSalaryComponentList.ToList());

                var entity = await _repository.GetByIdAsync(request.Id.Value);
                employeeSalaryAggregate.EmployeeSalary = entity;

                employeeSalaryAggregate.StartEmployeeSalaryUpdate(

                         request.EmployeeId,
                         request.BranchId,
                         request.DepartmentId,
                         request.PositionId,
                         request.GradeId,
                         request.SalaryStructureId,
                         request.PaymentOptionId,
                         request.GrossSalary,
                         request.CompanyId.Value,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted
    );
                foreach (var item in employeeSalaryAggregate.employeeSalaryComponents)
                {
                    await _repositoryEmployeeSalaryComponent.UpdateAsync(item).ConfigureAwait(false);
                }
                await _repository.UpdateAsync(employeeSalaryAggregate.EmployeeSalary).ConfigureAwait(false);
                //New component if added
                foreach (var empSalComp in employeeSalaryAggregate.EmployeeSalaryComponentList)
                {
                    empSalComp.EmployeeSalaryId = employeeSalaryAggregate.EmployeeSalary.Id;
                    var data2 = await _repositoryEmployeeSalaryComponent.AddAsync(empSalComp).ConfigureAwait(false);
                }
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

