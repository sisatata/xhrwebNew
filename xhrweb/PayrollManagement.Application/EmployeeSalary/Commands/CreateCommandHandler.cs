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
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeSalary, EmployeeSalaryCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalary, Guid> repository,
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

        public async Task<EmployeeSalaryCommandVM> Handle(Commands.V1.CreateEmployeeSalary request, CancellationToken cancellationToken)
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


                var employeeSalaryComponent = new EmployeeSalaryComponentFilterSpecificaion(request.CompanyId.Value, Guid.Empty);
                var employeeSalaryComponentList = await _repositoryEmployeeSalaryComponent.ListAsync(employeeSalaryComponent).ConfigureAwait(false);
                if (request.IsRequestFromPromotion)
                {
                    var oModel = employeeSalaryList.FirstOrDefault(x => (x.StartDate >= request.StartDate && x.StartDate <= request.EndDate) || (x.EndDate >= request.StartDate && x.EndDate <= request.EndDate));
                    if (oModel != null)
                    {
                        oModel.UpdateEndDateEmployeeSalary(request.StartDate.Value.AddDays(-1));
                        await _repository.UpdateAsync(oModel).ConfigureAwait(false);
                    }
                }
                var employeeSalaryAggregate = new EmployeeSalaryAggregate(employeeSalaryList.ToList(), salaryStructureComponentList.ToList(), employeeSalaryComponentList.ToList());
                employeeSalaryAggregate.CreateEmployeeSalary(
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
                var data = await _repository.AddAsync(employeeSalaryAggregate.EmployeeSalary).ConfigureAwait(false);
                foreach (var empSalComp in employeeSalaryAggregate.EmployeeSalaryComponentList)
                {
                    empSalComp.EmployeeSalaryId = data.Id;
                    var data2 = await _repositoryEmployeeSalaryComponent.AddAsync(empSalComp).ConfigureAwait(false);
                }
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
