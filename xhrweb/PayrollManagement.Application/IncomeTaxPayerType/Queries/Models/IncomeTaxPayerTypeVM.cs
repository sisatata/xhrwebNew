using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.IncomeTaxPayerType .Queries.Models
{
    public class IncomeTaxPayerTypeVM
    {
         public Guid? Id {get; set;}
         public string PayerTypeCode {get; set;}
         public string PayerTypeName {get; set;}
         public string Remarks {get; set;}
         public Boolean IsActive {get; set;}
         public Boolean IsDeleted {get; set;}
         public Guid? CompanyId {get; set;}
    }
}
