using LeaveManagement.Application.LeaveTypes.Commands.Models;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveTypes.Commands
{
    public class CreateCommandHandler : IRequestHandler<LeaveTypeCommands.V1.CreateLeaveType, LeaveTypeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<LeaveType, Guid> leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        private readonly IAsyncRepository<LeaveType, Guid> _leaveTypeRepository;
        
        public async Task<LeaveTypeCommandVM> Handle(LeaveTypeCommands.V1.CreateLeaveType request, CancellationToken cancellationToken)
        {
            var response = new LeaveTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                // need to refactor. should use auto mapper
                var entity = LeaveType.CreateLeaveType(request.CompanyId, request.LeaveTypeName, request.LeaveTypeShortName
                    , request.LeaveTypeLocalizedName, request.Balance, request.IsCarryForward, request.IsFemaleSpecific, request.IsPaid,
                    request.IsEncashable, request.EncashReserveBalance, request.IsDependOnDateOfConfirmation, request.IsLeaveDaysFixed,
                    request.IsBasedWorkingDays, request.ConsecutiveLimit, request.IsAllowAdvanceLeaveApply, request.IsAllowWithPrecedingHoliday,
                    request.IsAllowOpeningBalance, request.IsPreventPostLeaveApply,request.LeaveTypeGroupId);

                var data = await _leaveTypeRepository.AddAsync(entity);

                response.Id = data.Id;
                response.Status = true;
                response.Message = "success";
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }
            
            return response;
        }
       
    }
}
