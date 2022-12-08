using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeePaidIncomeTax : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public string FinancialYear { get; private set; }
        public string MonthName { get; private set; }
        public Guid? FinancialYearId { get; private set; }
        public Guid? MonthCycleId { get; private set; }
        public decimal? Basic { get; private set; }
        public decimal? HouseRent { get; private set; }
        public decimal? Medical { get; private set; }
        public decimal? Conveyance { get; private set; }
        public decimal? FestivalBonus { get; private set; }
        public decimal? TaxAmount { get; private set; }
        public DateTime ProcessingDate { get; private set; }
        public string Remarks { get; private set; }
        public Guid? CompanyId { get; private set; }
        public TrackingState State { get; set; }

        public EmployeePaidIncomeTax(Guid id) : base(id) { }
        private EmployeePaidIncomeTax() : base(Guid.NewGuid()) { }

        public void Create(

         Guid? employeeId,
         string financialYear,
         string monthName,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? basic,
         decimal? houseRent,
         decimal? medical,
         decimal? conveyance,
         decimal? festivalBonus,
         decimal? taxAmount,
         DateTime processingDate,
         string remarks,
         Guid? companyId
        )

        {
            //var oModel = new EmployeePaidIncomeTax(Guid.NewGuid());
            EmployeeId = employeeId;
            FinancialYear = financialYear;
            MonthName = monthName;
            FinancialYearId = financialYearId;
            MonthCycleId = monthCycleId;
            Basic = basic;
            HouseRent = houseRent;
            Medical = medical;
            Conveyance = conveyance;
            FestivalBonus = festivalBonus;
            TaxAmount = taxAmount;
            ProcessingDate = processingDate;
            Remarks = remarks;
            CompanyId = companyId;
            State = TrackingState.Added;
            //return oModel;

        }


        public void UpdateEmployeePaidIncomeTax
            (

         Guid? employeeId,
         string financialYear,
         string monthName,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? basic,
         decimal? houseRent,
         decimal? medical,
         decimal? conveyance,
         decimal? festivalBonus,
         decimal? taxAmount,
         DateTime processingDate,
         string remarks,
         Guid? companyId

        )
        {
            EmployeeId = employeeId;
            FinancialYear = financialYear;
            MonthName = monthName;
            FinancialYearId = financialYearId;
            MonthCycleId = monthCycleId;
            Basic = basic;
            HouseRent = houseRent;
            Medical = medical;
            Conveyance = conveyance;
            FestivalBonus = festivalBonus;
            TaxAmount = taxAmount;
            ProcessingDate = processingDate;
            Remarks = remarks;
            CompanyId = companyId;
            State = TrackingState.Modified;
        }


        //public void MarkAsDeleteEmployeePaidIncomeTax()
        //{
        //    IsDeleted = true;
        //}


    }
}

