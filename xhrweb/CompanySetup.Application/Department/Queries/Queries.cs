using CompanySetup.Application.Department.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.Department.Queries
{
    public static class Queries
    {
        public class GetDepartmentByBranch : IRequest<List<DepartmentVM>>
        {
            public Guid BranchId { get; set; }
        }

        public class GetDepartmentsByBranchList : IRequest<List<DepartmentVM>>
        {
            public Guid CompanyId { get; set; }
            public List<Guid> BranchIds { get; set; }
        }

        public class GetDepartmentById : IRequest<DepartmentVM>
        {
            public Guid Id { get; set; }
        }
    }
}
