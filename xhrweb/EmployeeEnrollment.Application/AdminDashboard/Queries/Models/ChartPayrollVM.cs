using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.AdminDashBoard.Queries.Models
{
    public class ChartPayrollVM
    {
        public decimal? GrossSalary { get; set; }
        public decimal? PayableSalary { get; set; }
        public decimal? NetPayableAmount { get; set; }
        public decimal? TotalDeductedAmount { get; set; }
    }
}
