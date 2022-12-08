using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.FinancialYear.Queries.Models
{
  public  class MonthCycleIdAndNameVM
    {
        public Guid? Id { get; set; }
        public string MonthCycleName { get; set; }
    }
}
