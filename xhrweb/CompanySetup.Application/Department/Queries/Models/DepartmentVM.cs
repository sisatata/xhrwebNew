using System;

namespace CompanySetup.Application.Department.Queries.Models
{
    public class DepartmentVM
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get;  set; }
        public Guid BranchId { get;  set; }
        public string CompanyName { get; set; }
        public string CompanyLocalizedName { get;  set; }
        public string BranchName { get; set; }
        public string BranchLocalizedName { get;  set; }
        public string DepartmentName { get;  set; }
        public string DepartmentLocalizedName { get;  set; }
        public string ShortName { get;  set; }
        public uint SortOrder { get;  set; }
    }
}
