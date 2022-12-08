using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitDeductionCode .Queries.Models
{
    public class BenefitDeductionCodeVM
    {
         public Guid Id {get; set;}
         public Guid? CompanyId {get; set;}
         public string BenifitDeductionCode {get; set;}
         public string BenifitDeductionCodeName {get; set;}
    }
}
