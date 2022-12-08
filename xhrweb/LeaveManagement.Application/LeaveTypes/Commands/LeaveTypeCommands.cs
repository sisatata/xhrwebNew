using LeaveManagement.Application.LeaveTypes.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveTypes.Commands
{
    public class LeaveTypeCommands
    {
        public static class V1
        {
            public class CreateLeaveType : IRequest<LeaveTypeCommandVM>
            {
                public Guid CompanyId { get; set; }
                public string LeaveTypeName { get; set; }
                public string LeaveTypeShortName { get; set; }
                public string LeaveTypeLocalizedName { get; set; }
                public int Balance { get; set; }
                public bool IsCarryForward { get; set; }
                public bool IsFemaleSpecific { get; set; }
                public bool IsPaid { get; set; }
                public bool IsEncashable { get; set; }
                public int EncashReserveBalance { get; set; }
                public bool IsDependOnDateOfConfirmation { get; set; }
                public bool IsLeaveDaysFixed { get; set; }
                public bool IsBasedWorkingDays { get; set; }
                public int ConsecutiveLimit { get; set; }
                public bool IsAllowAdvanceLeaveApply { get; set; }
                public bool IsAllowWithPrecedingHoliday { get; set; }
                public bool IsAllowOpeningBalance { get; set; }
                public bool IsPreventPostLeaveApply { get; set; }
                public int LeaveTypeGroupId { get; set; }
            }

            public class UpdateLeaveType : IRequest<LeaveTypeCommandVM>
            {
                public Guid Id { get; set; }
                public Guid CompanyId { get; set; }
                public string LeaveTypeName { get; set; }
                public string LeaveTypeShortName { get; set; }
                public string LeaveTypeLocalizedName { get; set; }
                public int Balance { get; set; }
                public bool IsCarryForward { get; set; }
                public bool IsFemaleSpecific { get; set; }
                public bool IsPaid { get; set; }
                public bool IsEncashable { get; set; }
                public int EncashReserveBalance { get; set; }
                public bool IsDependOnDateOfConfirmation { get; set; }
                public bool IsLeaveDaysFixed { get; set; }
                public bool IsBasedWorkingDays { get; set; }
                public int ConsecutiveLimit { get; set; }
                public bool IsAllowAdvanceLeaveApply { get; set; }
                public bool IsAllowWithPrecedingHoliday { get; set; }
                public bool IsAllowOpeningBalance { get; set; }
                public bool IsPreventPostLeaveApply { get; set; }
                public int LeaveTypeGroupId { get; set; }
            }
            public class MarkLeaveTypeAsDelete : IRequest<LeaveTypeCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
