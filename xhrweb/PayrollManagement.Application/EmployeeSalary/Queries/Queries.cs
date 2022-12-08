using PayrollManagement.Application.EmployeeSalary.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeeSalary.Queries
{
    public static class Queries
    {
        public class GetEmployeeSalaryListByCompany : IRequest<List<EmployeeSalaryVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeSalaryListByEmployee : IRequest<List<EmployeeSalaryVM>>
        {
            public Guid EmployeeId { get; set; }
        }
        public class GetEmployeeSalary : IRequest<EmployeeSalaryVM>
        {
            public Guid Id { get; set; }
        }

        public class GetEmployeeSalaryHistoryByEmployee : IRequest<EmployeeSalaryHistoryVM>
        {
            public Guid EmployeeId { get; set; }
        }
        public class GetEmployeeSalaryHistoryByCompany : IRequest<EmployeeSalaryHistoryVM>
        {
            public Guid CompanyId { get; set; }
        }
        public class GetEmployeeSalaryWithFilter : IRequest<List<EmployeeSalaryVM>>
        {
            public Guid CompanyId { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public string SearchText { get; set; }
        }
    }
}
