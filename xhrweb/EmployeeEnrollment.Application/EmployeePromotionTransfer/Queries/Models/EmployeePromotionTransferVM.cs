using System;

namespace EmployeeEnrollment.Application.EmployeePromotionTransfer.Queries.Models
{
    public class EmployeePromotionTransferVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string CompanyEmployeeId { get; set; }
        public Guid? PreviousCompanyId { get; set; }
        public Guid? PreviousBranchId { get; set; }
        public Guid? PreviousDepartmentId { get; set; }
        public Guid? PreviousPositionId { get; set; }
        public Guid? NewCompanyId { get; set; }
        public Guid? NewBranchId { get; set; }
        public Guid? NewDepartmentId { get; set; }
        public Guid? NewPositionId { get; set; }
        public DateTime? ProposedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? PreviousGross { get; set; }
        public decimal? NewGross { get; set; }
        public decimal? PreviousBasic { get; set; }
        public decimal? NewBasic { get; set; }
        public decimal? PreviousHouserent { get; set; }
        public decimal? NewHouserent { get; set; }
        public Int32? IncrementTypeId { get; set; }
        public Int32? IncrementValueTypeId { get; set; }
        public decimal? IncrementValue { get; set; }
        public decimal? IncrementAmount { get; set; }
        public Int32? IncrementOn { get; set; }
        public string Reason { get; set; }
        public string Reference { get; set; }
        public DateTime? ApproveDate { get; set; }
        public Guid? ApproveBy { get; set; }
        public string ApproveNote { get; set; }
        public string ApprovalStatus { get; set; }
        public string PreviousCompanyName { get; set; }
        public string PreviousBranchName { get; set; }
        public string PreviousDepartmentName { get; set; }
        public string PreviousPositionName { get; set; }
        public string NewCompanyName { get; set; }
        public string NewBranchName { get; set; }
        public string NewDepartmentName { get; set; }
        public string NewPositionName { get; set; }
        public Guid? PreviousGradeId { get; set; }
        public Guid? PreviousSalaryStructureId { get; set; }
        public short? PreviousPaymentOptionId { get; set; }
        public Guid? NewGradeId { get; set; }
        public Guid? NewSalaryStructureId { get; set; }
        public short? NewPaymentOptionId { get; set; }
        public string PreviousGradeName { get; set; }
        public string PreviousStructureName { get; set; }
        public string PreviousOptionName { get; set; }
        public string NewGradeName { get; set; }
        public string NewStructureName { get; set; }
        public string NewOptionName { get; set; }



    }
}
