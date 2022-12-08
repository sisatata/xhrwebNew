using PayrollManagement.Application.FinancialYear.Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalary.Queries.Models
{
   public class EmployeeSalaryHistoryVM
    {
        public List<EmployeeSalaryVM> EmployeeSalaryList { get; set; }
        public List<FinancialYearVM> FinancialYearList { get; set; }

    }
}
