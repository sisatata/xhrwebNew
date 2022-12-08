using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeePFTransaction : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Guid? EmlpoyeeDesignationId { get; private set; }
        public Guid? EmployeeDepartmentId { get; private set; }
        public Guid? PFYearId { get; private set; }
        public Guid? PFMonthId { get; private set; }
        public DateTime? TransactionDate { get; private set; }
        public decimal? CompanyContribution { get; private set; }
        public decimal? EmployeeContribution { get; private set; }
        public decimal? EmployeeInterestRate { get; private set; }
        public decimal? CompanyInterestRate { get; private set; }
        public decimal? InterestOnEmployeeContribution { get; private set; }
        public decimal? InterestOnCompanyContribution { get; private set; }
        public decimal? TotalContribution { get; private set; }
        public decimal? TotalInterest { get; private set; }
        public decimal? EmployeeCumulativeBalance { get; private set; }
        public decimal? CompanyCumulativeBalance { get; private set; }
        public decimal? TotalCumulativeBalance { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public TrackingState State { get; set; }
        public EmployeePFTransaction(Guid id) : base(id) { }
        private EmployeePFTransaction() : base(Guid.NewGuid()) { }

        public void Create(

         Guid? employeeId,
         Guid? emlpoyeeDesignationId,
         Guid? employeeDepartmentId,
         Guid? pFYearId,
         Guid? pFMonthId,
         DateTime? transactionDate,
         decimal? companyContribution,
         decimal? employeeContribution,
         decimal? employeeInterestRate,
         decimal? companyInterestRate,
         decimal? interestOnEmployeeContribution,
         decimal? interestOnCompanyContribution,
         decimal? totalContribution,
         decimal? totalInterest,
         decimal? employeeCumulativeBalance,
         decimal? companyCumulativeBalance,
         decimal? totalCumulativeBalance,
         string remarks,
         Guid? companyId

        )

        {
            EmployeeId = employeeId;
            EmlpoyeeDesignationId = emlpoyeeDesignationId;
            EmployeeDepartmentId = employeeDepartmentId;
            PFYearId = pFYearId;
            PFMonthId = pFMonthId;
            TransactionDate = transactionDate;
            CompanyContribution = companyContribution;
            EmployeeContribution = employeeContribution;
            EmployeeInterestRate = employeeInterestRate;
            CompanyInterestRate = companyInterestRate;
            InterestOnEmployeeContribution = interestOnEmployeeContribution;
            InterestOnCompanyContribution = interestOnCompanyContribution;
            TotalContribution = totalContribution;
            TotalInterest = totalInterest;
            EmployeeCumulativeBalance = employeeCumulativeBalance;
            CompanyCumulativeBalance = companyCumulativeBalance;
            TotalCumulativeBalance = totalCumulativeBalance;
            Remarks = remarks;
            IsDeleted = false;
            CompanyId = companyId;
            State = TrackingState.Added;
        }


        public void UpdateEmployeePFTransaction
            (

         Guid? employeeId,
         Guid? emlpoyeeDesignationId,
         Guid? employeeDepartmentId,
         Guid? pFYearId,
         Guid? pFMonthId,
         DateTime? transactionDate,
         decimal? companyContribution,
         decimal? employeeContribution,
         decimal? employeeInterestRate,
         decimal? companyInterestRate,
         decimal? interestOnEmployeeContribution,
         decimal? interestOnCompanyContribution,
         decimal? totalContribution,
         decimal? totalInterest,
         decimal? employeeCumulativeBalance,
         decimal? companyCumulativeBalance,
         decimal? totalCumulativeBalance,
         string remarks,
         Guid? companyId

        )
        {
            EmployeeId = employeeId;
            EmlpoyeeDesignationId = emlpoyeeDesignationId;
            EmployeeDepartmentId = employeeDepartmentId;
            PFYearId = pFYearId;
            PFMonthId = pFMonthId;
            TransactionDate = transactionDate;
            CompanyContribution = companyContribution;
            EmployeeContribution = employeeContribution;
            EmployeeInterestRate = employeeInterestRate;
            CompanyInterestRate = companyInterestRate;
            InterestOnEmployeeContribution = interestOnEmployeeContribution;
            InterestOnCompanyContribution = interestOnCompanyContribution;
            TotalContribution = totalContribution;
            TotalInterest = totalInterest;
            EmployeeCumulativeBalance = employeeCumulativeBalance;
            CompanyCumulativeBalance = companyCumulativeBalance;
            TotalCumulativeBalance = totalCumulativeBalance;
            Remarks = remarks;
            CompanyId = companyId;
            State = TrackingState.Modified;
        }


        public void MarkAsDeleteEmployeePFTransaction()
        {
            IsDeleted = true;
            State = TrackingState.Deleted;
        }


    }
}

