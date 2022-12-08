using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeeLeaveEncashment : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        //public Guid? Id { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public Guid? LeaveTypeId { get; private set; }
        public DateTime EncashDate { get; private set; }
        public Guid? FinancialYearId { get; private set; }
        public Guid? MonthCycleId { get; private set; }
        public decimal? ELEncashedDays { get; private set; }
        public decimal? EncashedAmount { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsDeleted { get; set; }

        // not persisted
        public TrackingState State { get; set; }
        public EmployeeLeaveEncashment(Guid id) : base(id) { }
        private EmployeeLeaveEncashment() : base(Guid.NewGuid()) { }

        public void Create(
         Guid? employeeId,
         Guid? leaveTypeId,
         DateTime encashDate,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? eLEncashedDays,
         decimal? encashedAmount,
         string remarks
        )

        {
            EmployeeId = employeeId;
            LeaveTypeId = leaveTypeId;
            EncashDate = encashDate;
            FinancialYearId = financialYearId;
            MonthCycleId = monthCycleId;
            ELEncashedDays = eLEncashedDays;
            EncashedAmount = encashedAmount;
            Remarks = remarks;
            State = TrackingState.Added;
        }


        public void UpdateEmployeeLeaveEncashment
            (

         Guid? employeeId,
         Guid? leaveTypeId,
         DateTime encashDate,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? eLEncashedDays,
         decimal? encashedAmount,
         string remarks

        )
        {
            EmployeeId = employeeId;
            LeaveTypeId = leaveTypeId;
            EncashDate = encashDate;
            FinancialYearId = financialYearId;
            MonthCycleId = monthCycleId;
            ELEncashedDays = eLEncashedDays;
            EncashedAmount = encashedAmount;
            Remarks = remarks;
            State = TrackingState.Modified;
        }


        public void MarkAsDeleteEmployeeLeaveEncashment()
        {
            IsDeleted = true;
        }


    }
}

