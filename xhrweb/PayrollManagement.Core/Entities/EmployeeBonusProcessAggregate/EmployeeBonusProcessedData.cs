using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate
{
    public class EmployeeBonusProcessedData : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public Guid? BonusTypeId { get; private set; }
        public DateTime? BonusDate { get; private set; }
        public Guid? FinancialYearId { get; private set; }
        public Int16? PaymentOptionId { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Guid? BranchId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public Guid? PositionId { get; private set; }
        public DateTime? JoiningDate { get; private set; }
        public Guid? GradeId { get; private set; }
        public Guid? ReligionId { get; private set; }
        public decimal? Basic { get; private set; }
        public decimal? HouseRent { get; private set; }
        public decimal? Medical { get; private set; }
        public decimal? Conveyance { get; private set; }
        public decimal? Food { get; private set; }
        public decimal? GrossSalary { get; private set; }
        public decimal? PayableBonus { get; private set; }
        public decimal? TaxDeductedAmount { get; private set; }
        public decimal? NetPayableBonus { get; private set; }
        public decimal? Basic_V2 { get; private set; }
        public decimal? HouseRent_V2 { get; private set; }
        public decimal? GrossSalary_V2 { get; private set; }
        public decimal? PayableBonus_V2 { get; private set; }
        public decimal? TaxDeductedAmount_V2 { get; private set; }
        public decimal? NetPayableBonus_V2 { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsApproved { get; private set; }
        public Guid? ApprovedBy { get; private set; }
        public DateTime? ApprovedTime { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? BonusConfigurationId { get; private set; }
        // not persisted
        public TrackingState State { get; set; }
        public EmployeeBonusProcessedData(Guid id) : base(id) { }
        private EmployeeBonusProcessedData() : base(Guid.NewGuid()) { }

        public void Create(

         Guid? employeeId,
         Guid? bonusTypeId,
         DateTime? bonusDate,
         Guid? financialYearId,
         Int16? paymentOptionId,
         Guid? companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         DateTime? joiningDate,
         Guid? gradeId,
         Guid? religionId,
         decimal? basic,
         decimal? houseRent,
         decimal? medical,
         decimal? conveyance,
         decimal? food,
         decimal? grossSalary,
         decimal? payableBonus,
         decimal? taxDeductedAmount,
         decimal? netPayableBonus,
         decimal? basic_V2,
         decimal? houseRent_V2,
         decimal? grossSalary_V2,
         decimal? payableBonus_V2,
         decimal? taxDeductedAmount_V2,
         decimal? netPayableBonus_V2,
         string remarks,
         Guid? bonusConfigurationId

        )

        {
            EmployeeId = employeeId;
            BonusTypeId = bonusTypeId;
            BonusDate = bonusDate;
            FinancialYearId = financialYearId;
            PaymentOptionId = paymentOptionId;
            CompanyId = companyId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            JoiningDate = joiningDate;
            GradeId = gradeId;
            ReligionId = religionId;
            Basic = basic;
            HouseRent = houseRent;
            Medical = medical;
            Conveyance = conveyance;
            Food = food;
            GrossSalary = grossSalary;
            PayableBonus = payableBonus;
            TaxDeductedAmount = taxDeductedAmount;
            NetPayableBonus = netPayableBonus;
            Basic_V2 = basic_V2;
            HouseRent_V2 = houseRent_V2;
            GrossSalary_V2 = grossSalary_V2;
            PayableBonus_V2 = payableBonus_V2;
            TaxDeductedAmount_V2 = taxDeductedAmount_V2;
            NetPayableBonus_V2 = netPayableBonus_V2;
            Remarks = remarks;

            IsDeleted = false;
            BonusConfigurationId = bonusConfigurationId;
            State = TrackingState.Added;


        }


        public void UpdateEmployeeBonusProcessedData
            (

         Guid? employeeId,
         Guid? bonusTypeId,
         DateTime? bonusDate,
         Guid? financialYearId,
         Int16? paymentOptionId,
         Guid? companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         DateTime? joiningDate,
         Guid? gradeId,
         Guid? religionId,
         decimal? basic,
         decimal? houseRent,
         decimal? medical,
         decimal? conveyance,
         decimal? food,
         decimal? grossSalary,
         decimal? payableBonus,
         decimal? taxDeductedAmount,
         decimal? netPayableBonus,
         decimal? basic_V2,
         decimal? houseRent_V2,
         decimal? grossSalary_V2,
         decimal? payableBonus_V2,
         decimal? taxDeductedAmount_V2,
         decimal? netPayableBonus_V2,
         string remarks,
         Guid? bonusConfigurationId

        )
        {
            EmployeeId = employeeId;
            BonusTypeId = bonusTypeId;
            BonusDate = bonusDate;
            FinancialYearId = financialYearId;
            PaymentOptionId = paymentOptionId;
            CompanyId = companyId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            JoiningDate = joiningDate;
            GradeId = gradeId;
            ReligionId = religionId;
            Basic = basic;
            HouseRent = houseRent;
            Medical = medical;
            Conveyance = conveyance;
            Food = food;
            GrossSalary = grossSalary;
            PayableBonus = payableBonus;
            TaxDeductedAmount = taxDeductedAmount;
            NetPayableBonus = netPayableBonus;
            Basic_V2 = basic_V2;
            HouseRent_V2 = houseRent_V2;
            GrossSalary_V2 = grossSalary_V2;
            PayableBonus_V2 = payableBonus_V2;
            TaxDeductedAmount_V2 = taxDeductedAmount_V2;
            NetPayableBonus_V2 = netPayableBonus_V2;
            Remarks = remarks;
            BonusConfigurationId = bonusConfigurationId;
            State = TrackingState.Modified;
            IsDeleted = false;
        }


        public void MarkAsDeleteEmployeeBonusProcessedData()
        {
            IsDeleted = true;
            State = TrackingState.Modified;
        }


    }
}

