using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeePromotionTransfer.Commands
{
    public class ApproveEmployeePromotionTransferCommandHandler : IRequestHandler<Commands.V1.ApproveEmployeePromotionTransferCommand, EmployeePromotionTransferCommandVM>
    {
        public ApproveEmployeePromotionTransferCommandHandler(IAsyncRepository<EmployeeEntities.EmployeePromotionTransfer, Guid> repository,
            IAsyncRepository<EmployeeEntities.Employee, Guid> repositoryEmployee,
            IReferenceRepository<EmployeeEntities.EmployeeSalary, Guid> repositoryEmployeeSalary,
            IReferenceRepository<EmployeeEntities.EmployeeSalaryComponent, Guid> repositoryEmployeeSalaryComponent,
            IReferenceRepository<EmployeeEntities.SalaryStructureComponent, Guid> repositorySalaryStructureComponent,
            IServiceBus serviceBus,
            IConfiguration configuration,
            IActivityLogService activityLogService,
            IEmployeeNoteService employeeNoteService
            )
        {
            _repository = repository;
            _repositoryEmployeeSalary = repositoryEmployeeSalary;
            _repositoryEmployeeSalaryComponent = repositoryEmployeeSalaryComponent;
            _repositorySalaryStructureComponent = repositorySalaryStructureComponent;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _activityLogService = activityLogService;
            _employeeNoteService = employeeNoteService;
            _repositoryEmployee = repositoryEmployee;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeePromotionTransfer, Guid> _repository;
        private readonly IAsyncRepository<EmployeeEntities.Employee, Guid> _repositoryEmployee;

        private readonly IReferenceRepository<EmployeeEntities.EmployeeSalary, Guid> _repositoryEmployeeSalary;
        private readonly IReferenceRepository<EmployeeEntities.EmployeeSalaryComponent, Guid> _repositoryEmployeeSalaryComponent;
        private readonly IReferenceRepository<EmployeeEntities.SalaryStructureComponent, Guid> _repositorySalaryStructureComponent;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;
        private readonly IEmployeeNoteService _employeeNoteService;
        public async Task<EmployeePromotionTransferCommandVM> Handle(Commands.V1.ApproveEmployeePromotionTransferCommand request, CancellationToken cancellationToken)
        {
            var response = new EmployeePromotionTransferCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {

                var entity = await _repository.GetByIdAsync(request.EmployeePromotionTransferId).ConfigureAwait(false);


                entity.ApproveEmployeePromotionTransfer(request.Notes);

                await _repository.UpdateAsync(entity).ConfigureAwait(false);

                var employee = await _repositoryEmployee.GetByIdAsync(entity.EmployeeId.Value).ConfigureAwait(false);

                var ret = await _activityLogService.InsertActivity("Employee", $"{Enum.GetName(typeof(PromotionTransferTypes), entity.IncrementTypeId)} has been approved {employee.FullName} for increamented amount {entity.IncrementAmount}", entity).ConfigureAwait(false);
                var empNote = await _employeeNoteService.InsertEmployeeNote(entity.EmployeeId.Value, $"{Enum.GetName(typeof(PromotionTransferTypes), entity.IncrementTypeId)}  has been approved for increamented amount {entity.IncrementAmount}", Guid.Empty, true).ConfigureAwait(false);

                // publish the event in message queue.

                var @event = new Core.Events.PromotionTransferApproveEvent
                {
                    EmployeeId = entity.EmployeeId,
                    BranchId = (entity.NewBranchId == null || entity.NewBranchId == Guid.Empty) ? entity.PreviousBranchId : entity.NewBranchId,
                    DepartmentId = (entity.NewDepartmentId == null || entity.NewDepartmentId == Guid.Empty) ? entity.PreviousDepartmentId : entity.NewDepartmentId,
                    PositionId = (entity.NewPositionId == null || entity.NewPositionId == Guid.Empty) ? entity.PreviousPositionId : entity.NewPositionId,

                    GradeId = entity.NewGradeId,
                    SalaryStructureId = entity.NewSalaryStructureId,
                    PaymentOptionId = entity.NewPaymentOptionId,
                    GrossSalary = entity.NewGross,
                    CompanyId = (entity.NewCompanyId == null || entity.NewCompanyId == Guid.Empty) ? entity.PreviousCompanyId : entity.NewCompanyId,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    IsDeleted = false,
                    CommandPublished = DateTime.Now
                };

                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);


                response.Status = true;
                response.Message = "entity approved successfully.";
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
