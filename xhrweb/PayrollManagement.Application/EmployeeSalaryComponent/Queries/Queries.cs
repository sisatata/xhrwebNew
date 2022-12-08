using PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeeSalaryComponent.Queries
{
    public static class Queries
    {
        public class GetEmployeeSalaryComponentListByParent : IRequest<List<EmployeeSalaryComponentVM>>
        {
            public Guid EmployeeSalaryId { get; set; }
        }

        public class GetEmployeeSalaryComponent : IRequest<EmployeeSalaryComponentVM>
        {
            public Guid Id { get; set; }
        }
    }
}
