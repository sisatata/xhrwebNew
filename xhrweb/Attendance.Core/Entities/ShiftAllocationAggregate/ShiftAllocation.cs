using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities.ShiftAllocationAggregate
{
    public class ShiftAllocation : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {


        public Guid FinancialYearId { get; private set; }
        public string DutyYear { get; private set; }
        public Guid MonthCycleId { get; private set; }
        public string DutyMonth { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid CompanyId { get; private set; }
        public Guid PrimaryShiftId { get; private set; }
        public int RotationDay { get; private set; }
        public string C1 { get; private set; }
        public string C2 { get; private set; }
        public string C3 { get; private set; }
        public string C4 { get; private set; }
        public string C5 { get; private set; }
        public string C6 { get; private set; }
        public string C7 { get; private set; }
        public string C8 { get; private set; }
        public string C9 { get; private set; }
        public string C10 { get; private set; }
        public string C11 { get; private set; }
        public string C12 { get; private set; }
        public string C13 { get; private set; }
        public string C14 { get; private set; }
        public string C15 { get; private set; }
        public string C16 { get; private set; }
        public string C17 { get; private set; }
        public string C18 { get; private set; }
        public string C19 { get; private set; }
        public string C20 { get; private set; }
        public string C21 { get; private set; }
        public string C22 { get; private set; }
        public string C23 { get; private set; }
        public string C24 { get; private set; }
        public string C25 { get; private set; }
        public string C26 { get; private set; }
        public string C27 { get; private set; }
        public string C28 { get; private set; }
        public string C29 { get; private set; }
        public string C30 { get; private set; }
        public string C31 { get; private set; }
        public bool IsDeleted { get; private set; }
        public ShiftAllocation(Guid id) : base(id) { }
        private ShiftAllocation() : base(Guid.NewGuid()) { }

        // not persisted
        public TrackingState State { get; set; }

        public void SetShiftAllocation(Guid financialYearId, string dutyYear, Guid monthCycleId, string dutyMonth, Guid employeeId,
            Guid companyId, Guid primaryShiftId, int rotationDay, string c1, string c2, string c3, string c4, string c5, string c6, string c7,
            string c8, string c9, string c10, string c11, string c12, string c13, string c14, string c15, string c16, string c17, string c18,
            string c19, string c20, string c21, string c22, string c23, string c24, string c25, string c26, string c27, string c28, string c29,
            string c30, string c31, bool isDeleted)
        {


            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrEmptyGuid(financialYearId, "financialYearId");
            Guard.Against.NullOrEmptyGuid(monthCycleId, "monthCycleId");
            Guard.Against.NullOrEmptyGuid(employeeId, "employeeId");
            Guard.Against.NullOrEmptyGuid(primaryShiftId, "primaryShiftId");


            FinancialYearId = financialYearId;
            DutyYear = dutyYear;
            MonthCycleId = monthCycleId;
            DutyMonth = dutyMonth;
            EmployeeId = employeeId;
            CompanyId = companyId;
            PrimaryShiftId = primaryShiftId;
            RotationDay = rotationDay;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = c5;
            C6 = c6;
            C7 = c7;
            C8 = c8;
            C9 = c9;
            C10 = c10;
            C11 = c11;
            C12 = c12;
            C13 = c13;
            C14 = c14;
            C15 = c15;
            C16 = c16;
            C17 = c17;
            C18 = c18;
            C19 = c19;
            C20 = c20;
            C21 = c21;
            C22 = c22;
            C23 = c23;
            C24 = c24;
            C25 = c25;
            C26 = c26;
            C27 = c27;
            C28 = c28;
            C29 = c29;
            C30 = c30;
            C31 = c31;
            IsDeleted = isDeleted;
            State = TrackingState.Added;
            //return shiftAllocation;
        }

        public void UpdateShiftAllocation(Guid financialYearId, string dutyYear, Guid monthCycleId, string dutyMonth, Guid employeeId, Guid companyId,
            Guid primaryShiftId, int rotationDay, string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8,
            string c9, string c10, string c11, string c12, string c13, string c14, string c15, string c16, string c17, string c18, string c19,
            string c20, string c21, string c22, string c23, string c24, string c25, string c26, string c27, string c28, string c29, string c30,
            string c31, bool isDeleted)
        {

            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrEmptyGuid(financialYearId, "financialYearId");
            Guard.Against.NullOrEmptyGuid(monthCycleId, "monthCycleId");
            Guard.Against.NullOrEmptyGuid(employeeId, "employeeId");
            Guard.Against.NullOrEmptyGuid(primaryShiftId, "primaryShiftId");


            FinancialYearId = financialYearId;
            DutyYear = dutyYear;
            MonthCycleId = monthCycleId;
            DutyMonth = dutyMonth;
            EmployeeId = employeeId;
            CompanyId = companyId;
            PrimaryShiftId = primaryShiftId;
            RotationDay = rotationDay;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = c5;
            C6 = c6;
            C7 = c7;
            C8 = c8;
            C9 = c9;
            C10 = c10;
            C11 = c11;
            C12 = c12;
            C13 = c13;
            C14 = c14;
            C15 = c15;
            C16 = c16;
            C17 = c17;
            C18 = c18;
            C19 = c19;
            C20 = c20;
            C21 = c21;
            C22 = c22;
            C23 = c23;
            C24 = c24;
            C25 = c25;
            C26 = c26;
            C27 = c27;
            C28 = c28;
            C29 = c29;
            C30 = c30;
            C31 = c31;
            IsDeleted = isDeleted;
        }
        public void MarkShiftAllocationAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
