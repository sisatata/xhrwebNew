using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using ASL.Hrms.SharedKernel.Enums;
using PayrollManagement.Core.Specifications;
using PayrollManagement.Core.Entities;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Utility.FileManager.Interfaces;

namespace PayrollManagement.Application.BenefitBillClaim.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateBenefitBillClaim, BenefitBillClaimCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid>
        repository,
            IAsyncRepository<BenefitBillClaimApproveQueue, Guid> benefitBillClaimApproveQueueRepository,
            IReferenceRepository<EmployeeManager, Guid> employeeManagerRepository,
            IServiceBus serviceBus,
            IConfiguration configuration, IAttachedFileManager attachedFileManager,
            IActivityLogService activityLogService
            )
        {
            _repository = repository;
            _benefitBillClaimApproveQueueRepository = benefitBillClaimApproveQueueRepository;
            _employeeManagerRepository = employeeManagerRepository;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _attachedFileManager = attachedFileManager;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> _repository;
        private readonly IAsyncRepository<PayrollEntities.BenefitBillClaimApproveQueue, Guid> _benefitBillClaimApproveQueueRepository;
        private readonly IReferenceRepository<EmployeeManager, Guid> _employeeManagerRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IAttachedFileManager _attachedFileManager;
        private readonly IActivityLogService _activityLogService;
        public async Task<BenefitBillClaimCommandVM> Handle(Commands.V1.CreateBenefitBillClaim request, CancellationToken cancellationToken)
        {
            var response = new BenefitBillClaimCommandVM
            {
                Status = false,
                Message = "error"
            };


            try
            {
                var pictureUri = string.Empty;

                if (request.BillAttachment != null)
                {
                    pictureUri = _attachedFileManager.UploadAttachedFile(request.BillAttachment,
                            $"{request.Settings.ContentRoot.RootPath}{request.Settings.ContentRoot.AttachedFilePath}",
                            new ASL.Utility.FileManager.Models.AttachedFileSettings
                            {
                                MaxFileSize = request.Settings.PlanetHRAttachedFileSettings.MaxFileSize,
                                ValidExtensions = request.Settings.PlanetHRAttachedFileSettings.ValidExtensions.Split(',').ToList()
                            });
                }

                var entity = PayrollEntities.BenefitBillClaim.Create(

                         request.BenefitDeductionId,
                         request.EmployeeId,
                         request.BillDate,
                         request.ClaimDate,
                         request.AllocatedAmount,
                         request.ClaimAmount,
                         request.Remarks,
                         request.IsDeleted,
                         request.CompanyId,
                         "A",
                          ((int)ApprovalStatuses.Pending).ToString(),
                          pictureUri,
                          request.LocationFrom,
                          request.LocationTo,
                          request.VehicleTypeId,
                          request.NumberOfAttendees,
                          request.NameOfAttendees
                );
                var data = await _repository.AddAsync(entity).ConfigureAwait(false);
                data.BillNoMaskingValue = $"{DateTime.Now.ToString("yyMM")}{data.BillNo.ToString().PadLeft(8, '0')}";

                await _repository.UpdateAsync(data);

                var billManagerFilter = new EmployeeBillManagerSpecificaion(request.EmployeeId.Value);
                var billManagers = await _employeeManagerRepository.ListAsync(billManagerFilter).ConfigureAwait(false);

                if (billManagers.Any())
                {
                    var attnManager = billManagers.FirstOrDefault();
                    BenefitBillClaimApproveQueue oQueue = PayrollEntities.BenefitBillClaimApproveQueue.Create(data.Id, attnManager.ManagerId, null,
                        ((int)ApprovalStatuses.Pending).ToString()
                        , null, "");

                    var approvalQueue = await _benefitBillClaimApproveQueueRepository.AddAsync(oQueue).ConfigureAwait(false);

                    // publish the event in message queue.

                    var @event = new Core.ApplicationEvents.BillAppliedEvent
                    {
                        ManagerId = attnManager.ManagerId.Value,
                        EmployeeId = attnManager.EmployeeId,
                        ApplicationId = entity.Id,
                        BillAmount = entity.ClaimAmount.Value,
                        CommandPublished = DateTime.Now
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                }

                response.Status = true;
                response.Message = "entity created successfully.";
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
