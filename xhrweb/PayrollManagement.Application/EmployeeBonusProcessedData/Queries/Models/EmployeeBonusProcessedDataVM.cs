using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeBonusProcessedData.Queries.Models
{
    public class EmployeeBonusProcessedDataVM
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? BonusTypeId { get; set; }
        public DateTime? BonusDate { get; set; }
        public Guid? FinancialYearId { get; set; }
        public Int16? PaymentOptionId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public Guid? GradeId { get; set; }
        public decimal? Basic { get; set; }
        public decimal? HouseRent { get; set; }
        public decimal? Medical { get; set; }
        public decimal? Conveyance { get; set; }
        public decimal? Food { get; set; }
        public decimal? GrossSalary { get; set; }
        public decimal? PayableBonus { get; set; }
        public decimal? TaxDeductedAmount { get; set; }
        public decimal? NetPayableBonus { get; set; }
        public decimal? Basic_V2 { get; set; }
        public decimal? HouseRent_V2 { get; set; }
        public decimal? GrossSalary_V2 { get; set; }
        public decimal? PayableBonus_V2 { get; set; }
        public decimal? TaxDeductedAmount_V2 { get; set; }
        public decimal? NetPayableBonus_V2 { get; set; }
        public string Remarks { get; set; }
        public Boolean IsApproved { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedTime { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? BonusConfigurationId { get; set; }

        public string EmployeeName { get; set; }

        public string BonusYear { get; set; }
        public string BonusType { get; set; }
    }
}
