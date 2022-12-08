using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using Attendance.Core.ExtensionMethods;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Entities.ShiftAllocationAggregate
{
    public class ShiftAllocationAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        public ShiftAllocationAggregate(Guid companyId, Guid employeeId, IReadOnlyList<ShiftAllocation> shiftAllocations)
        {
            Guard.Against.NullOrEmptyGuid(companyId, nameof(companyId));
            Guard.Against.NullOrEmptyGuid(employeeId, nameof(employeeId));

            CompanyId = companyId;
            EmployeeId = employeeId;
            _shiftAllocations = shiftAllocations;
            ShiftAllocation = new ShiftAllocation(Guid.NewGuid());
        }

        public Guid CompanyId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public IReadOnlyList<ShiftAllocation> _shiftAllocations { get; set; }
        public ShiftAllocation ShiftAllocation { get; set; }

        public void NewShiftAllocation(Guid financialYearId, string dutyYear, Guid monthCycleId, string dutyMonth, Guid primaryShiftId, int rotationDay, string c1, string c2, string c3, string c4, string c5, string c6, string c7,
            string c8, string c9, string c10, string c11, string c12, string c13, string c14, string c15, string c16, string c17, string c18,
            string c19, string c20, string c21, string c22, string c23, string c24, string c25, string c26, string c27, string c28, string c29,
            string c30, string c31, bool isDeleted)
        {
            var allocatedShift = _shiftAllocations.FirstOrDefault(x => x.CompanyId == CompanyId && x.EmployeeId == EmployeeId
            && x.MonthCycleId == monthCycleId && x.FinancialYearId == financialYearId);

            Guard.Against.DuplicateEntry(allocatedShift);

            ShiftAllocation.SetShiftAllocation(financialYearId, dutyYear, monthCycleId, dutyMonth, EmployeeId,
             CompanyId, primaryShiftId, rotationDay, c1, c2, c3, c4, c5, c6, c7,
             c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18,
             c19, c20, c21, c22, c23, c24, c25, c26, c27, c28, c29,
             c30, c31, isDeleted);
        }
    }
}
