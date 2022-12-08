using System;

namespace CompanySetup.Application.Designation.Queries.Models
{
    public class DesignationVM
    {
        public Guid Id { get; set; }
        public Guid CompanyId { set; get; }
        public string CompanyName { set; get; }
        public Guid BranchId { set; get; }
        public string BranchName { set; get; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLocalizedName { get; set; }
        public string DesignationName { get; set; }
        public string DesignationLocalizedName { get; set; }
        public string ShortName { get; set; }
        public uint SortOrder { get; set; }
    }
}
