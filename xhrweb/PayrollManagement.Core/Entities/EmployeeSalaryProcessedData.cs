using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeeSalaryProcessedData : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public Guid? FinancialYearId { get; private set; }
        public Guid? MonthCycleId { get; private set; }
        public Int16? PaymentOptionId { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Guid? BranchId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public Guid? PositionId { get; private set; }
        public Guid? GradeId { get; private set; }
        public Int16? TotalDaysInMonth { get; private set; }
        public Int16? TotalWorkingDays { get; private set; }
        public Int16? TotalPresentDays { get; private set; }
        public Int16? TotalAbsentDays { get; private set; }
        public Int16? TotalLeaveDays { get; private set; }
        public Int16? WeeklyOffDays { get; private set; }
        public Int16? GovernmentOffDays { get; private set; }
        public Int16? TotalWorkingHoliday { get; private set; }
        public string OTHour { get; private set; }
        public decimal? OTRate { get; private set; }
        public decimal? GrossSalary { get; private set; }
        public decimal? PayableSalary { get; private set; }
        public decimal? TotalDeductionAmount { get; private set; }
        public decimal? NetPayableAmount { get; private set; }
        public DateTime? ProcessDate { get; private set; }
        public Int16? StampCost { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? EmloyeeSalaryId { get; private set; }
        public Boolean IsApproved { get; private set; }
        public Guid? ApprovedBy { get; private set; }
        public DateTime? ApprovedTime { get; private set; }
        public string Remarks { get; private set; }

        // not persisted
        public TrackingState State { get; set; }
        public EmployeeSalaryProcessedData(Guid id) : base(id) { }
        private EmployeeSalaryProcessedData() : base(Guid.NewGuid()) { }

        public void Create(

         Guid? employeeId,
         Guid? financialYearId,
         Guid? monthCycleId,
         Int16? paymentOptionId,
         Guid? companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         Guid? gradeId,
         Int16? totalDaysInMonth,
         Int16? totalWorkingDays,
         Int16? totalPresentDays,
         Int16? totalAbsentDays,
         Int16? totalLeaveDays,
         Int16? weeklyOffDays,
         Int16? governmentOffDays,
         Int16? totalWorkingHoliday,
         string oTHour,
         decimal? oTRate,
         decimal? grossSalary,
         decimal? payableSalary,
         decimal? totalDeductionAmount,
         decimal? netPayableAmount,
         DateTime? processDate,
         Int16? stampCost,
         Boolean isDeleted,
         Guid? emloyeeSalaryId,
         Boolean isApproved,
         Guid? approvedBy,
         DateTime? approvedTime,
         string remarks
        )

        {
            //var oModel = new EmployeeSalaryProcessedData(Guid.NewGuid());

            EmployeeId = employeeId;
            FinancialYearId = financialYearId;
            MonthCycleId = monthCycleId;
            PaymentOptionId = paymentOptionId;
            CompanyId = companyId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            GradeId = gradeId;
            TotalDaysInMonth = totalDaysInMonth;
            TotalWorkingDays = totalWorkingDays;
            TotalPresentDays = totalPresentDays;
            TotalAbsentDays = totalAbsentDays;
            TotalLeaveDays = totalLeaveDays;
            WeeklyOffDays = weeklyOffDays;
            GovernmentOffDays = governmentOffDays;
            TotalWorkingHoliday = totalWorkingHoliday;
            OTHour = oTHour;
            OTRate = oTRate;
            GrossSalary = grossSalary;
            PayableSalary = payableSalary;
            TotalDeductionAmount = totalDeductionAmount;
            NetPayableAmount = netPayableAmount;
            ProcessDate = processDate;
            StampCost = stampCost;
            IsDeleted = isDeleted;
            EmloyeeSalaryId = emloyeeSalaryId;
            IsApproved = isApproved;
            ApprovedBy = approvedBy;
            ApprovedTime = approvedTime;
            Remarks = remarks;
            State = TrackingState.Added;
            // return oModel;

        }


        public void UpdateEmployeeSalaryProcessedData
            (

         Guid? employeeId,
         Guid? financialYearId,
         Guid? monthCycleId,
         Int16? paymentOptionId,
         Guid? companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         Guid? gradeId,
         Int16? totalDaysInMonth,
         Int16? totalWorkingDays,
         Int16? totalPresentDays,
         Int16? totalAbsentDays,
         Int16? totalLeaveDays,
         Int16? weeklyOffDays,
         Int16? governmentOffDays,
         Int16? totalWorkingHoliday,
         string oTHour,
         decimal? oTRate,
         decimal? grossSalary,
         decimal? payableSalary,
         decimal? totalDeductionAmount,
         decimal? netPayableAmount,
         DateTime? processDate,
         Int16? stampCost,
         Boolean isDeleted,
         Guid? emloyeeSalaryId,
         Boolean isApproved,
         Guid? approvedBy,
         DateTime? approvedTime,
         string remarks
        )
        {
            EmployeeId = employeeId;
            FinancialYearId = financialYearId;
            MonthCycleId = monthCycleId;
            PaymentOptionId = paymentOptionId;
            CompanyId = companyId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            GradeId = gradeId;
            TotalDaysInMonth = totalDaysInMonth;
            TotalWorkingDays = totalWorkingDays;
            TotalPresentDays = totalPresentDays;
            TotalAbsentDays = totalAbsentDays;
            TotalLeaveDays = totalLeaveDays;
            WeeklyOffDays = weeklyOffDays;
            GovernmentOffDays = governmentOffDays;
            TotalWorkingHoliday = totalWorkingHoliday;
            OTHour = oTHour;
            OTRate = oTRate;
            GrossSalary = grossSalary;
            PayableSalary = payableSalary;
            TotalDeductionAmount = totalDeductionAmount;
            NetPayableAmount = netPayableAmount;
            ProcessDate = processDate;
            StampCost = stampCost;
            IsDeleted = isDeleted;
            EmloyeeSalaryId = emloyeeSalaryId;
            IsApproved = isApproved;
            ApprovedBy = approvedBy;
            ApprovedTime = approvedTime;
            Remarks = remarks;
            State = TrackingState.Modified;
        }


        public void MarkAsDeleteEmployeeSalaryProcessedData()
        {
            IsDeleted = true;
        }


    }
}

