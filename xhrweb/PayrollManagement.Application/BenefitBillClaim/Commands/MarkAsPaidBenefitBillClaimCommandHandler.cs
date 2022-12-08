using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using ASL.Hrms.SharedKernel.Enums;
using PayrollManagement.Core.Entities;
using PayrollManagement.Core.Specifications;
using System.Linq;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PayrollManagement.Application.BenefitBillClaim.Commands
{
    public class MarkAsPaidBenefitBillClaimCommandHandler : IRequestHandler<Commands.V1.MarkAsPaidBenefitBillClaim, BenefitBillClaimCommandVM>
    {
        public MarkAsPaidBenefitBillClaimCommandHandler(IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> repository,
            IReferenceRepository<EmployeeManager, Guid> employeeManagerRepository,
            IAsyncRepository<BenefitBillClaimApproveQueue, Guid> requestApproveQueueRepository,
            ICurrentUserContext userContext,
            IServiceBus serviceBus,
            IConfiguration configuration,
            IActivityLogService activityLogService)
        {
            _repository = repository;
            _employeeManagerRepository = employeeManagerRepository;
            _requestApproveQueueRepository = requestApproveQueueRepository;
            _userContext = userContext;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> _repository;
        private readonly IReferenceRepository<EmployeeManager, Guid> _employeeManagerRepository;
        private readonly IAsyncRepository<BenefitBillClaimApproveQueue, Guid> _requestApproveQueueRepository;
        private readonly ICurrentUserContext _userContext;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;

        public async Task<BenefitBillClaimCommandVM>
            Handle(Commands.V1.MarkAsPaidBenefitBillClaim request, CancellationToken cancellationToken)
        {
            var response = new BenefitBillClaimCommandVM
            {
                Status = false,
                Message = "error"
            };
            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                var ManagerFilter = new EmployeeBillManagerSpecificaion(entity.EmployeeId.Value);
                var managers = await _employeeManagerRepository.ListAsync(ManagerFilter).ConfigureAwait(false);

                if (managers.Any())
                {
                    var accountManager = managers.FirstOrDefault(x => x.ManagerTypeCode.ToLower() == "accounts");
                    if (accountManager != null)
                    {
                        entity.MarkAsPaidBenefitBillClaim(accountManager.ManagerId, DateTime.Now, (int)PaymentStatuses.Paid);
                    }
                }

                //entity.MarkAsPaidBenefitBillClaim(

                //          new Guid(_userContext.CurrentUserId),
                //         DateTime.Now,
                //          (int)PaymentStatuses.Paid
                //          );

                await _repository.UpdateAsync(entity);

                //var ManagerFilter = new EmployeeBillManagerSpecificaion(entity.EmployeeId.Value);
                //var managers = await _employeeManagerRepository.ListAsync(ManagerFilter).ConfigureAwait(false);

                //if (managers.Any())
                //{
                //    var Manager = managers.FirstOrDefault();
                //    var QueueFilter = new BenefitBillClaimApproveQueueFilterSpecification(request.Id, Manager.ManagerId.Value);

                //    var Queues = await _requestApproveQueueRepository.ListAsync(QueueFilter).ConfigureAwait(false);
                //    var Queue = Queues.FirstOrDefault();
                //    Queue.Note = entity.ApprovedNotes;
                //    Queue.ApprovalStatus = entity.ApprovalStatus;
                //    Queue.ApprovedDate = DateTime.Now;
                //    await _requestApproveQueueRepository.UpdateAsync(Queue).ConfigureAwait(false);

                //    // publish the event in message queue.

                var @event = new Core.ApplicationEvents.BillPaidEvent
                {
                    AccountManagerId = entity.PaidBy.Value,
                    EmployeeId = entity.EmployeeId.Value,
                    ApplicationId = entity.Id,
                    PaidAmount = entity.ApprovedAmount.Value,
                    CommandPublished = DateTime.Now
                };

                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                //}

                response.Status = true;
                response.Message = "entity marked as paid successfully.";
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

