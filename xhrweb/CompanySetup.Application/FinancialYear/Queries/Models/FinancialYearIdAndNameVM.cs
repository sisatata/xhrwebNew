using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.FinancialYear.Queries.Models
{
   public class FinancialYearIdAndNameVM
    {
        public Guid? FinancialYearId { get; set;}
        public string FinancialYearName { get; set; }
    }
}
