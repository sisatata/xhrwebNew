using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using System.Linq;
using ASL.Utility.FileManager.Interfaces;

namespace PayrollManagement.Application.BenefitBillClaim.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBenefitBillClaim, BenefitBillClaimCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> repository, IAttachedFileManager attachedFileManager,
            IActivityLogService activityLogService)
        {
            _repository = repository;
            _attachedFileManager = attachedFileManager;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> _repository;
        private readonly IAttachedFileManager _attachedFileManager;
        private readonly IActivityLogService _activityLogService;

        public async Task<BenefitBillClaimCommandVM>
            Handle(Commands.V1.UpdateBenefitBillClaim request, CancellationToken cancellationToken)
        {
            var response = new BenefitBillClaimCommandVM
            {
                Status = false,
                Message = "error"
            };
            request.Status = ((int)ApprovalStatuses.Pending).ToString();
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

                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateBenefitBillClaim(
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

                await _repository.UpdateAsync(entity);

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

