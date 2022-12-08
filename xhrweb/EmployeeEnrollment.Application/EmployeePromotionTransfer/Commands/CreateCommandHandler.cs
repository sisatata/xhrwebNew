using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
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
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeePromotionTransfer, EmployeePromotionTransferCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeePromotionTransfer, Guid> repository,
            IAsyncRepository<EmployeeEntities.Employee, Guid> repositoryEmployee,
            IReferenceRepository<EmployeeEntities.EmployeeSalary, Guid> repositoryEmployeeSalary,
            IReferenceRepository<EmployeeEntities.EmployeeSalaryComponent, Guid> repositoryEmployeeSalaryComponent,
            IReferenceRepository<EmployeeEntities.SalaryStructureComponent, Guid> repositorySalaryStructureComponent,
            IActivityLogService activityLogService,
            IEmployeeNoteService employeeNoteService
            )
        {
            _repository = repository;
            _repositoryEmployeeSalary = repositoryEmployeeSalary;
            _repositoryEmployeeSalaryComponent = repositoryEmployeeSalaryComponent;
            _repositorySalaryStructureComponent = repositorySalaryStructureComponent;
            _activityLogService = activityLogService;
            _employeeNoteService = employeeNoteService;
            _repositoryEmployee = repositoryEmployee;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeePromotionTransfer, Guid> _repository;
        private readonly IAsyncRepository<EmployeeEntities.Employee, Guid> _repositoryEmployee;

        private readonly IReferenceRepository<EmployeeEntities.EmployeeSalary, Guid> _repositoryEmployeeSalary;
        private readonly IReferenceRepository<EmployeeEntities.EmployeeSalaryComponent, Guid> _repositoryEmployeeSalaryComponent;
        private readonly IReferenceRepository<EmployeeEntities.SalaryStructureComponent, Guid> _repositorySalaryStructureComponent;
        private readonly IActivityLogService _activityLogService;
        private readonly IEmployeeNoteService _employeeNoteService;
        public async Task<EmployeePromotionTransferCommandVM> Handle(Commands.V1.CreateEmployeePromotionTransfer request, CancellationToken cancellationToken)
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
                employeePromotionTransferAggregate.PrepareEmployeePromotionTransfer(request.EmployeeId,
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
                         request.NewPaymentOptionId);

                var data = await _repository.AddAsync(employeePromotionTransferAggregate.EmployeePromotionTransfer);
                var employee = await _repositoryEmployee.GetByIdAsync(request.EmployeeId.Value).ConfigureAwait(false);

                var ret = await _activityLogService.InsertActivity("Employee", $"{employee.FullName} has been proposed {Enum.GetName(typeof(PromotionTransferTypes), request.IncrementTypeId)} and increamented amount {data.IncrementAmount}", data).ConfigureAwait(false);
                var empNote = await _employeeNoteService.InsertEmployeeNote(data.EmployeeId.Value, $"{Enum.GetName(typeof(PromotionTransferTypes), request.IncrementTypeId)} and increamented amount {data.IncrementAmount}", Guid.Empty, false).ConfigureAwait(false);

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
