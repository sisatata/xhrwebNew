using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using CompanySetup.Core.Interfaces;

namespace CompanySetup.Core.Entities
{
    public class FinancialYear : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }
        public string FinancialYearName { get; private set; }
        public string FinancialYearLocalizedName { get; private set; }
        public DateTime FinancialYearStartDate { get; private set; }
        public DateTime FinancialYearEndDate { get; private set; }
        public bool IsRunningYear { get; private set; }
        public short SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public FinancialYear(Guid id) : base(id) { }
        private FinancialYear() : base(Guid.NewGuid()) { }

        public static FinancialYear CreateFinancialYear(Guid companyId, string financialYearName, string financialYearLocalizedName, DateTime financialYearStartDate, DateTime financialYearEndDate,
            bool isRunningYear, short sortOrder, bool isDeleted)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(financialYearName, "financialYearName");

            var finyear = new FinancialYear(Guid.NewGuid())
            {
                CompanyId = companyId,
                FinancialYearName = financialYearName,
                FinancialYearLocalizedName = financialYearLocalizedName,
                FinancialYearStartDate = financialYearStartDate,
                FinancialYearEndDate = financialYearEndDate,
                IsRunningYear = isRunningYear,
                SortOrder = sortOrder,
                IsDeleted = isDeleted
            };
            return finyear;
        }
        public void UpdateFinancialYear(Guid companyId, string financialYearName, string financialYearLocalizedName, DateTime financialYearStartDate,
            DateTime financialYearEndDate, bool isRunningYear, short sortOrder, bool isDeleted)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(financialYearName, "financialYearName");

            CompanyId = companyId;
            FinancialYearName = financialYearName;
            FinancialYearLocalizedName = financialYearLocalizedName;
            FinancialYearStartDate = financialYearStartDate;
            FinancialYearEndDate = financialYearEndDate;
            IsRunningYear = isRunningYear;
            SortOrder = sortOrder;
            IsDeleted = isDeleted;
        }
        public void MarkFinancialYearDeleted()
        {
            IsDeleted = true;
        }
    }
}
