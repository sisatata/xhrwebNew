using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries
{
    public static class Queries
    {
        public class GetEmployeeSalaryProcessedDataComponentList : IRequest<List<EmployeeSalaryProcessedDataComponentVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeSalaryProcessedDataComponent : IRequest<EmployeeSalaryProcessedDataComponentVM>
        {
            public Guid Id { get; set; }
        }
    }
}
