using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using CompanySetup.Core.Interfaces;
using System;

namespace CompanySetup.Core.Entities
{
    public class Department : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }
        public Guid BranchId { get; private set; }
        public string DepartmentName { get; private set; }
        public string ShortName { get; private set; }
        public string DepartmentLocalizedName { get; private set; }
        public uint SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public Department(Guid id) : base(id) { }
        private Department() : base(Guid.NewGuid()) { }

        // Factory method to create Department
        public static Department Create(Guid companyId, Guid branchId, string departmentName, 
            string shortName, string departmentLocalizedName, uint sortOrder)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrEmptyGuid(branchId, "branchId");
            Guard.Against.NullOrWhiteSpace(departmentName, "departmentName");

            var department = new Department(Guid.NewGuid())
            {
                CompanyId = companyId,
                BranchId = branchId,
                DepartmentName = departmentName,
                ShortName = shortName,
                DepartmentLocalizedName = departmentLocalizedName,
                SortOrder = sortOrder,
            };

            return department;
        }

        public void UpdateDepartment(Guid companyId, Guid branchId, string departmentName, string shortName, string departmentLocalizedName, uint sortOrder)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrEmptyGuid(branchId, "branchId");
            Guard.Against.NullOrWhiteSpace(departmentName, "departmentName");

            CompanyId = companyId;
            BranchId = branchId;
            DepartmentName = departmentName;
            ShortName = shortName;
            DepartmentLocalizedName = departmentLocalizedName;
            SortOrder = sortOrder;
        }

        public void MarkDepartmentAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
