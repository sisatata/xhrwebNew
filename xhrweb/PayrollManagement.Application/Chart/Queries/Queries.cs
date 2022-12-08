using MediatR;
using PayrollManagement.Application.Chart.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.Chart.Queries
{
  public static class Queries
    {
        public class DepartmentMonthlySalary : IRequest<List<DepartmentSalaryVM>>
        {
            public Guid? CompanyId { get; set; }
            public string FinancialYearName { get; set; }
            public string MonthCycleName { get; set; }
        }

    }
}
