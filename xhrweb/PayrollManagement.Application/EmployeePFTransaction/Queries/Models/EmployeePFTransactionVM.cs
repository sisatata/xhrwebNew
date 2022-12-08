using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeePFTransaction.Queries.Models
{
    public class EmployeePFTransactionVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? EmlpoyeeDesignationId { get; set; }
        public Guid? EmployeeDepartmentId { get; set; }
        public Guid? PFYearId { get; set; }
        public Guid? PFMonthId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? CompanyContribution { get; set; }
        public decimal? EmployeeContribution { get; set; }
        public decimal? EmployeeInterestRate { get; set; }
        public decimal? CompanyInterestRate { get; set; }
        public decimal? InterestOnEmployeeContribution { get; set; }
        public decimal? InterestOnCompanyContribution { get; set; }
        public decimal? TotalContribution { get; set; }
        public decimal? TotalInterest { get; set; }
        public decimal? EmployeeCumulativeBalance { get; set; }
        public decimal? CompanyCumulativeBalance { get; set; }
        public decimal? TotalCumulativeBalance { get; set; }
        public string Remarks { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
