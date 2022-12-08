using CompanySetup.Application.Department.Commands.Models;
using MediatR;
using System;

namespace CompanySetup.Application.Department.Commands
{
    public class DepartmentCommands
    {
        public static class V1
        {
            public class CreateDepartment : IRequest<DepartmentCommandVM>
            {
                public Guid CompanyId { get; set; }
                public Guid BranchId { get; set; }
                public string DepartmentName { get; set; }
                public string DepartmentLocalizedName { get; set; }
                public string ShortName { get; set; }
                
                public uint SortOrder { get; set; }
            }

            public class UpdateDepartment : IRequest<DepartmentCommandVM>
            {
                public Guid Id { get; set; }
                public Guid CompanyId { get; set; }
                public Guid BranchId { get; set; }
                public string DepartmentName { get; set; }
                public string DepartmentLocalizedName { get; set; }
                public string ShortName { get; set; }
                
                public uint SortOrder { get; set; }
            }

            public class MarkDepartmentAsDelete : IRequest<DepartmentCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
