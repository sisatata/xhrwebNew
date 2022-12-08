using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using CompanySetup.Core.Interfaces;
using System;

namespace CompanySetup.Core.Entities
{
    public class Branch : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }
        public string BranchName { get; private set; }
        public string ShortName { get; private set; }
        public string BranchLocalizedName { get; private set; }
        public uint SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public Branch(Guid id) : base(id) { }
        private Branch() : base(Guid.NewGuid()) { }

        // Factory method to create branch
        public static Branch Create(Guid companyId, string branchName, string shortName, string branchLocalizedName, uint sortOrder)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(branchName, "branchName");

            var branch = new Branch(Guid.NewGuid())
            {
                CompanyId = companyId,
                BranchName = branchName,
                ShortName = shortName,
                BranchLocalizedName = branchLocalizedName,
                SortOrder = sortOrder,
            };

            return branch;
        }

        public void UpdateBranch(Guid companyId, string branchName, string shortName, string branchLocalizedName, uint sortOrder)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(branchName, "branchName");

            CompanyId = companyId;
            BranchName = branchName;
            ShortName = shortName;
            BranchLocalizedName = branchLocalizedName;
            SortOrder = sortOrder;
        }

        public void MarkBranchAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
