using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.FinancialYear.Queries.Models
{
   public class FinancialYearIdVM
    {
        public string FinancialYearName { get; set; }
        public Guid? FinancialYearId { get; set; }
    }
}
