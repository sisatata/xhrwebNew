using PayrollManagement.Application.EmployeePaidIncomeTax.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeePaidIncomeTax.Queries
{
    public static class Queries
    {
        public class GetEmployeePaidIncomeTaxList : IRequest<List<EmployeePaidIncomeTaxVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeePaidIncomeTax : IRequest<EmployeePaidIncomeTaxVM>
        {
            public Guid Id { get; set; }
        }
    }
}
