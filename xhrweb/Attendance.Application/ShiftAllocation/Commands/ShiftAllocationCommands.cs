using Attendance.Application.ShiftAllocation.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.ShiftAllocation.Commands
{
    public class ShiftAllocationCommands
    {
        public static class V1
        {
            public class SetShiftAllocation : IRequest<ShiftAllocationCommandVM>
            {
                public Guid Id { get; set; }
                public Guid FinancialYearId { get; set; }
                public string DutyYear { get; set; }
                public Guid MonthCycleId { get; set; }
                public string DutyMonth { get; set; }
                public Guid EmployeeId { get; set; }
                public Guid CompanyId { get; set; }
                public Guid BranchId { get; set; }
                public Guid PrimaryShiftId { get; set; }
                public int RotationDay { get; set; }
                public string C1 { get; set; }
                public string C2 { get; set; }
                public string C3 { get; set; }
                public string C4 { get; set; }
                public string C5 { get; set; }
                public string C6 { get; set; }
                public string C7 { get; set; }
                public string C8 { get; set; }
                public string C9 { get; set; }
                public string C10 { get; set; }
                public string C11 { get; set; }
                public string C12 { get; set; }
                public string C13 { get; set; }
                public string C14 { get; set; }
                public string C15 { get; set; }
                public string C16 { get; set; }
                public string C17 { get; set; }
                public string C18 { get; set; }
                public string C19 { get; set; }
                public string C20 { get; set; }
                public string C21 { get; set; }
                public string C22 { get; set; }
                public string C23 { get; set; }
                public string C24 { get; set; }
                public string C25 { get; set; }
                public string C26 { get; set; }
                public string C27 { get; set; }
                public string C28 { get; set; }
                public string C29 { get; set; }
                public string C30 { get; set; }
                public string C31 { get; set; }
                public bool IsDeleted { get; set; }
            }

            public class UpdateShiftAllocation : IRequest<ShiftAllocationCommandVM>
            {
                public Guid Id { get; set; }
                public Guid FinancialYearId { get; set; }
                public string DutyYear { get; set; }
                public Guid MonthCycleId { get; set; }
                public string DutyMonth { get; set; }
                public Guid EmployeeId { get; set; }
                public Guid CompanyId { get; set; }
                public Guid PrimaryShiftId { get; set; }
                public int RotationDay { get; set; }
                public string C1 { get; set; }
                public string C2 { get; set; }
                public string C3 { get; set; }
                public string C4 { get; set; }
                public string C5 { get; set; }
                public string C6 { get; set; }
                public string C7 { get; set; }
                public string C8 { get; set; }
                public string C9 { get; set; }
                public string C10 { get; set; }
                public string C11 { get; set; }
                public string C12 { get; set; }
                public string C13 { get; set; }
                public string C14 { get; set; }
                public string C15 { get; set; }
                public string C16 { get; set; }
                public string C17 { get; set; }
                public string C18 { get; set; }
                public string C19 { get; set; }
                public string C20 { get; set; }
                public string C21 { get; set; }
                public string C22 { get; set; }
                public string C23 { get; set; }
                public string C24 { get; set; }
                public string C25 { get; set; }
                public string C26 { get; set; }
                public string C27 { get; set; }
                public string C28 { get; set; }
                public string C29 { get; set; }
                public string C30 { get; set; }
                public string C31 { get; set; }
                public bool IsDeleted { get; set; }
            }

            public class MarkShiftAllocationAsDelete : IRequest<ShiftAllocationCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class ProcessShiftAllocationBulk : IRequest<ShiftAllocationCommandVM>
            {
                public Guid? CompanyId { get; set; }
                public DateTime ProcessDate { get; set; }
            }
        }
    }
}
