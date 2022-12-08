using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.Chart.Model
{
   public class ChartPayrollVM 
    {
        public string GrossSalary { get; set;}
        public string PayableSalary { get; set; }
        public string NetPayableAmount { get; set; }
        public string TotalDeductedAmount { get; set; }

      


    }
}
