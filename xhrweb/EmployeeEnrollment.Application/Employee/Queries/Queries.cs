
using ASL.Hrms.SharedKernel.Models;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.Employee .Queries
{
    public static class Queries
    {
        public class GetEmployeeList : IRequest<List<EmployeeVM>>
        {
            //public int CompanyId { get; set; }
        }

        public class GetEmployeePagedList : IRequest<PagedResult<EmployeeVM>>
        {
            //public int CompanyId { get; set; }

            public int PageNumber { get; set; } = 1;

            public int PageSize { get; set; } = 10;
            public bool GetAll { get; set; }
        }

        public class GetEmployeeFilteredList : IRequest<PagedResult<EmployeeVM>>
        {
            //public int CompanyId { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public int PageNumber { get; set; } = 1;
            public string SearchText { get; set; }
            public int PageSize { get; set; } = 10;
            public bool GetAll { get; set; }
        }
        public class GetEmployeeFilteredListWithoutPagination : IRequest<List<EmployeeVM>>
        {
            //public int CompanyId { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public bool GetAll { get; set; }
        }

        public class GetEmployee : IRequest< EmployeeVM>
        {
            public Guid Id { get; set; }
        }
       

    }
}





