using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.IncomeTaxParameter .Queries.Models
{
    public class IncomeTaxParameterVM
    {
         public Guid? Id {get; set;}
         public string ParameterName {get; set;}
         public decimal? LimitAmount {get; set;}
         public decimal? LimitPercentageOfBasic {get; set;}
         public DateTime? StartDate {get; set;}
         public DateTime? EndDate {get; set;}
         public string Remarks {get; set;}
         public Boolean IsActive {get; set;}
         public Boolean IsDeleted {get; set;}
         public Guid? CompanyId {get; set;}
         public string PayerTypeCode {get; set;}
        public string PayerTypeName { get; set; }
    }
}
