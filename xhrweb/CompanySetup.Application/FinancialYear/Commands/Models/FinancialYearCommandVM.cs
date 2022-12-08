using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.FinancialYear.Commands.Models
{
   public  class FinancialYearCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
