using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.SalaryRuleDetail .Queries.Models
{
    public class SalaryRuleDetailVM
    {
         public Guid? Id {get; set;}
         public Guid? SalaryRuleId {get; set;}
         public string SalaryHead {get; set;}
         public decimal? Value {get; set;}
         public string ValueType {get; set;}
         public string PercentDependOn {get; set;}
    }
}
