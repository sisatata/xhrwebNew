using System;
using System.Collections.Generic;
using System.Text;
using PayrollManagement.Application.FinancialYear.Queries.Models;
using MediatR;

namespace PayrollManagement.Application.FinancialYear.Queries
{
    public static class Queries
    {
        public class GetFinancialYearByEmployeeId : IRequest<List<FinancialYearVM>>
        {
            public Guid EmployeeId { get; set; }
        }
    }
}
