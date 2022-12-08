using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeBonusProcessedData.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class StartEmployeeBonusProcess : IRequest<EmployeeBonusProcessedDataCommandVM>
            {
                //public Guid? EmployeeId { get; set; }
                public Guid? BonusTypeId { get; set; }
                public DateTime? BonusDate { get; set; }
                public Guid? FinancialYearId { get; set; }
                //public Int16? PaymentOptionId { get; set; }
                public Guid? CompanyId { get; set; }
                //public Guid? BranchId { get; set; }
                //public Guid? DepartmentId { get; set; }
                //public Guid? PositionID { get; set; }
                //public DateTime? JoiningDate { get; set; }
                //public Guid? GradeID { get; set; }
                //public decimal? Basic { get; set; }
                //public decimal? HouseRent { get; set; }
                //public decimal? Medical { get; set; }
                //public decimal? Conveyance { get; set; }
                //public decimal? Food { get; set; }
                //public decimal? GrossSalary { get; set; }
                //public decimal? PayableBonus { get; set; }
                //public decimal? TaxDeductedAmount { get; set; }
                //public decimal? NetPayableBonus { get; set; }
                //public decimal? Basic_V2 { get; set; }
                //public decimal? HouseRent_V2 { get; set; }
                //public decimal? GrossSalary_V2 { get; set; }
                //public decimal? PayableBonus_V2 { get; set; }
                //public decimal? TaxDeductedAmount_V2 { get; set; }
                //public decimal? NetPayableBonus_V2 { get; set; }
                //public string Remarks { get; set; }
                //public Boolean IsApproved { get; set; }
                //public Guid? ApprovedBy { get; set; }
                //public DateTime? ApprovedTime { get; set; }
                //public Boolean IsDeleted { get; set; }
                //public Guid? BonusConfigurationId { get; set; }
            }

            //public class UpdateEmployeeBonusProcessedData : IRequest< EmployeeBonusProcessedDataCommandVM>
            //    {
            //     public Guid? EmployeeId {get; set;}
            //     public Int32? BonusTypeID {get; set;}
            //     public DateTime? BonusDate {get; set;}
            //     public Guid? FinancialYear {get; set;}
            //     public Int16? PaymentOptionId {get; set;}
            //     public Guid? CompanyId {get; set;}
            //     public Guid? BranchId {get; set;}
            //     public Guid? DepartmentId {get; set;}
            //     public Guid? PositionID {get; set;}
            //     public DateTime? JoiningDate {get; set;}
            //     public Guid? GradeID {get; set;}
            //     public decimal? Basic {get; set;}
            //     public decimal? HouseRent {get; set;}
            //     public decimal? Medical {get; set;}
            //     public decimal? Conveyance {get; set;}
            //     public decimal? Food {get; set;}
            //     public decimal? GrossSalary {get; set;}
            //     public decimal? PayableBonus {get; set;}
            //     public decimal? TaxDeductedAmount {get; set;}
            //     public decimal? NetPayableBonus {get; set;}
            //     public decimal? Basic_V2 {get; set;}
            //     public decimal? HouseRent_V2 {get; set;}
            //     public decimal? GrossSalary_V2 {get; set;}
            //     public decimal? PayableBonus_V2 {get; set;}
            //     public decimal? TaxDeductedAmount_V2 {get; set;}
            //     public decimal? NetPayableBonus_V2 {get; set;}
            //     public string Remarks {get; set;}
            //     public Boolean IsApproved {get; set;}
            //     public Guid? ApprovedBy {get; set;}
            //     public DateTime? ApprovedTime {get; set;}
            //     public Boolean IsDeleted {get; set;}
            //     public Guid? BonusConfigurationId {get; set;}
            //    }

            //    public class MarkAsDeleteEmployeeBonusProcessedData : IRequest< EmployeeBonusProcessedDataCommandVM>
            //        {
            //        public Guid Id { get; set; }
            //        }
            //        }
        }
    }
}
