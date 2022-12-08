using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LeaveManagement.Application.LeaveTypes.Commands.Models;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Interfaces;
using MediatR;

namespace LeaveManagement.Application.LeaveTypes.Commands
{
    public class UpdateCommandHandler : IRequestHandler<LeaveTypeCommands.V1.UpdateLeaveType, LeaveTypeCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<LeaveType, Guid> leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        private readonly IAsyncRepository<LeaveType, Guid> _leaveTypeRepository;
        public async Task<LeaveTypeCommandVM> Handle(LeaveTypeCommands.V1.UpdateLeaveType request, CancellationToken cancellationToken)
        {
            var response = new LeaveTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _leaveTypeRepository.GetByIdAsync(request.Id);

                entity.UpdateLeaveType(
                   //request.CompanyId == Guid.Empty || request.CompanyId == null) ? entity.CompanyId : request.CompanyId,
                   request.CompanyId,
                request.LeaveTypeName,
                request.LeaveTypeShortName,
                request.LeaveTypeLocalizedName,
                request.Balance,
                request.IsCarryForward,
                request.IsFemaleSpecific,
                request.IsPaid,
                request.IsEncashable,
                request.EncashReserveBalance,
                request.IsDependOnDateOfConfirmation,
                request.IsLeaveDaysFixed,
                request.IsBasedWorkingDays,
                request.ConsecutiveLimit,
                request.IsAllowAdvanceLeaveApply,
                request.IsAllowWithPrecedingHoliday,
                request.IsAllowOpeningBalance,
                request.IsPreventPostLeaveApply
                , request.LeaveTypeGroupId
                );
                await _leaveTypeRepository.UpdateAsync(entity);
                response.Id = entity.Id;
                response.Status = true;
                response.Message = "success";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
