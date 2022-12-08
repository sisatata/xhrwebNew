using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Entities
{
    public class MonthCycle : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }
        public Guid FinancialYearId { get; private set; }
        public string MonthCycleName { get; private set; }
        public string MonthCycleLocalizedName { get; private set; }
        public DateTime MonthStartDate { get; private set; }
        public DateTime MonthEndDate { get; private set; }
        public double TotalWorkingDays { get; private set; }
        public bool RunningFlag { get; private set; }
        public bool IsSelected { get; private set; }
        public int SortOrder { get; private set; }

        public bool IsDeleted { get; private set; }

        //public DateTime CreatedDate { get; private set; }
        //public string CreatedBy { get;private  set; }
        //public DateTime UpdateDate { get; private  set; }
        //public string UpdateBy { get; private set; }

        // not persisted
        public TrackingState State { get; set; }
        public MonthCycle(Guid id) : base(id) { }
        private MonthCycle() : base(Guid.NewGuid()) { }

        public static MonthCycle CreateMonthCycle(Guid companyId, Guid financialYearId, string monthCycleName, string monthCycleLocalizedName,
            DateTime monthStartDate, DateTime monthEndDate, double totalWorkingDays, bool runningFlag, bool isSelected, int sortOrder, bool isdeleted)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrEmptyGuid(financialYearId, "financialYearId");
            Guard.Against.NullOrWhiteSpace(monthCycleName, "monthCycleName");

            var monthCycle = new MonthCycle(Guid.NewGuid())
            {
                CompanyId = companyId,
                FinancialYearId = financialYearId,
                MonthCycleName = monthCycleName,
                MonthCycleLocalizedName = monthCycleLocalizedName,
                MonthStartDate = monthStartDate,
                MonthEndDate = monthEndDate,
                TotalWorkingDays = totalWorkingDays,
                RunningFlag = runningFlag,
                IsSelected = isSelected,
                SortOrder = sortOrder,
            };
            return monthCycle;

        }
        public void UpdateMonthCycle(Guid companyId, Guid financialYearId, string monthCycleName, string monthCycleLocalizedName,
            DateTime monthStartDate, DateTime monthEndDate, double totalWorkingDays,
            bool runningFlag, bool isSelected, int sortOrder, bool isDeleted)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrEmptyGuid(financialYearId, "financialYearId");
            Guard.Against.NullOrWhiteSpace(monthCycleName, "monthCycleName");
            CompanyId = companyId;
            FinancialYearId = financialYearId;
            MonthCycleName = monthCycleName;
            MonthCycleLocalizedName = monthCycleLocalizedName;
            MonthStartDate = monthStartDate;
            MonthEndDate = monthEndDate;
            TotalWorkingDays = totalWorkingDays;
            RunningFlag = runningFlag;
            IsSelected = isSelected;
            SortOrder = sortOrder;
        }

        public void MarkMonthCycleDeleted()
        {
            IsDeleted = true;
        }

        public void CreateMonthCycle(Guid companyId, Guid financialYearId, string monthCycleName,
            DateTime monthStartDate, DateTime monthEndDate, double totalWorkingDays, int sortOrder)
        {
            //Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            //Guard.Against.NullOrEmptyGuid(financialYearId, "financialYearId");
            //Guard.Against.NullOrWhiteSpace(monthCycleName, "monthCycleName");
            CompanyId = companyId;
            FinancialYearId = financialYearId;
            MonthCycleName = monthCycleName;
            MonthCycleLocalizedName = monthCycleName;
            MonthStartDate = monthStartDate;
            MonthEndDate = monthEndDate;
            TotalWorkingDays = totalWorkingDays;
            RunningFlag = true;
            IsSelected = false;
            SortOrder = sortOrder;
            State = TrackingState.Added;
        }
    }
}
