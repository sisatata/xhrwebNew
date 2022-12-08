using EmployeeEnrollment.Core.Entities.EmployeePromotionTransferAggregate;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeePromotionTransfer.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeePromotionTransfer, EmployeePromotionTransferCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeePromotionTransfer, Guid> repository,
            IReferenceRepository<EmployeeEntities.EmployeeSalary, Guid> repositoryEmployeeSalary,
            IReferenceRepository<EmployeeEntities.EmployeeSalaryComponent, Guid> repositoryEmployeeSalaryComponent,
            IReferenceRepository<EmployeeEntities.SalaryStructureComponent, Guid> repositorySalaryStructureComponent)
        {
            _repository = repository;
            _repositoryEmployeeSalary = repositoryEmployeeSalary;
            _repositoryEmployeeSalaryComponent = repositoryEmployeeSalaryComponent;
            _repositorySalaryStructureComponent = repositorySalaryStructureComponent;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeePromotionTransfer, Guid> _repository;
        private readonly IReferenceRepository<EmployeeEntities.EmployeeSalary, Guid> _repositoryEmployeeSalary;
        private readonly IReferenceRepository<EmployeeEntities.EmployeeSalaryComponent, Guid> _repositoryEmployeeSalaryComponent;
        private readonly IReferenceRepository<EmployeeEntities.SalaryStructureComponent, Guid> _repositorySalaryStructureComponent;


        public async Task<EmployeePromotionTransferCommandVM>
            Handle(Commands.V1.UpdateEmployeePromotionTransfer request, CancellationToken cancellationToken)
        {
            var response = new EmployeePromotionTransferCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var employeePromotionTransferFilterSpecificaion = new EmployeePromotionTransferFilterSpecificaion(request.PreviousCompanyId.Value, request.EmployeeId.Value);
                var employeePromotionTransfers = await _repository.ListAsync(employeePromotionTransferFilterSpecificaion).ConfigureAwait(false);

                var salryStructureComponentFilter = new SalaryStructureComponentByCompanyFilterSpecificaion(request.PreviousCompanyId.Value);
                var salaryStructureComponents = await _repositorySalaryStructureComponent.ListAsync(salryStructureComponentFilter).ConfigureAwait(false);

                var employeeSalaryByCompanyFilter = new EmployeeSalaryByCompanyFilterSpecificaion(request.PreviousCompanyId.Value, request.EmployeeId.Value, request.StartDate);
                var employeeSalaries = await _repositoryEmployeeSalary.ListAsync(employeeSalaryByCompanyFilter).ConfigureAwait(false);

                var employeeSalaryComponentByCompanyFilter = new EmployeeSalaryComponentByCompanyFilterSpecificaion(request.PreviousCompanyId.Value);
                var employeeSalaryComponents = await _repositoryEmployeeSalaryComponent.ListAsync(employeeSalaryComponentByCompanyFilter).ConfigureAwait(false);

                var employeePromotionTransferAggregate = new EmployeePromotionTransferAggregate(employeePromotionTransfers.ToList(),
                    employeeSalaries.ToList(), employeeSalaryComponents.ToList(), salaryStructureComponents.ToList());

                var entity = await _repository.GetByIdAsync(request.Id.Value);
                employeePromotionTransferAggregate.EmployeePromotionTransfer = entity;


                employeePromotionTransferAggregate.PrepareEmployeePromotionTransferUpdate(

                         request.EmployeeId,
                         request.PreviousCompanyId,
                         request.PreviousBranchId,
                         request.PreviousDepartmentId,
                         request.PreviousPositionId,
                         request.NewCompanyId,
                         request.NewBranchId,
                         request.NewDepartmentId,
                         request.NewPositionId,
                         request.ProposedDate,
                         request.StartDate,
                         request.EndDate,
                         request.PreviousGross,
                         request.NewGross,
                         request.PreviousBasic,
                         request.NewBasic,
                         request.PreviousHouserent,
                         request.NewHouserent,
                         request.IncrementTypeId,
                         request.IncrementValueTypeId,
                         request.IncrementValue,
                         request.IncrementAmount,
                         request.IncrementOn,
                         request.Reason,
                         request.Reference,
                         request.PreviousGradeId,
                         request.NewGradeId,
                         request.PreviousSalaryStructureId,
                         request.NewSalaryStructureId,
                         request.PreviousPaymentOptionId,
                         request.NewPaymentOptionId

    );

                await _repository.UpdateAsync(employeePromotionTransferAggregate.EmployeePromotionTransfer);

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

