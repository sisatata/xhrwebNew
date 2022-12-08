using Attendance.Application.ShiftAllocation.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.ShiftAllocation.Queries
{
   public class Queries
    {
        public class GetShiftAllocationByCompanyAndEmployee : IRequest<List<ShiftAllocationVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid BranchId { get; set; }
            public Guid FinancialYearId { get; set; }
            public int DutyYear { get; set; }
            public int DutyMonth { get; set; }
            public Guid MonthCycleId { get; set; }
            public Guid? DepartmentId { get; set; }
            public Guid? DesignationId { get; set; }
            public Guid? EmployeeId { get; set; }
        }

        public class GetShiftAllocationById : IRequest<ShiftAllocationVM>
        {
            public Guid ShiftAllocationId { get; set; }
        }
    }
}
