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
    public class RejectCommandHandler : IRequestHandler<Commands.V1.RejectBenefitBillClaim, BenefitBillClaimCommandVM>
    {
        public RejectCommandHandler(IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> repository,
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
            Handle(Commands.V1.RejectBenefitBillClaim request, CancellationToken cancellationToken)
        {
            var response = new BenefitBillClaimCommandVM
            {
                Status = false,
                Message = "error"
            };
            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.RejectBenefitBillClaim(
                         new Guid("00000000-0000-0000-0000-000000000000"),//new Guid(_userContext.CurrentUserId),
                         DateTime.Now,
                         request.ApprovedNotes,
                          ((int)ApprovalStatuses.Declined).ToString()
                         );

                await _repository.UpdateAsync(entity);

                var ManagerFilter = new EmployeeBillManagerSpecificaion(entity.EmployeeId.Value);
                var managers = await _employeeManagerRepository.ListAsync(ManagerFilter).ConfigureAwait(false);

                if (managers.Any())
                {
                    var Manager = managers.FirstOrDefault();
                    var QueueFilter = new BenefitBillClaimApproveQueueFilterSpecification(request.Id, Manager.ManagerId.Value);

                    var Queues = await _requestApproveQueueRepository.ListAsync(QueueFilter).ConfigureAwait(false);
                    var Queue = Queues.FirstOrDefault();
                    Queue.Note = entity.ApprovedNotes;
                    Queue.ApprovalStatus = entity.ApprovalStatus;
                    Queue.ApprovedDate = DateTime.Now;
                    await _requestApproveQueueRepository.UpdateAsync(Queue).ConfigureAwait(false);

                    // publish the event in message queue.
                    var @event = new Core.ApplicationEvents.BillDeclinedEvent
                    {
                        ManagerId = Manager.ManagerId.Value,
                        EmployeeId = Manager.EmployeeId,
                        ApplicationId = entity.Id,
                        CommandPublished = DateTime.Now
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                }

                response.Status = true;
                response.Message = "entity rejected successfully.";
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

