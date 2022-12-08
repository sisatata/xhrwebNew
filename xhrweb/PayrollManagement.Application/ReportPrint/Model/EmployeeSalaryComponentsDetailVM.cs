using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
    public class EmployeeSalaryComponentsDetailVM
    {
        public Guid Id { get; set; }
        public decimal? ComponentAmount { get; set; }
        public Guid EmployeeSalaryId { get; set; }
        public string SalaryComponentName { get; set; }

        public string MonthCycleId { get; set; }

    }
}
