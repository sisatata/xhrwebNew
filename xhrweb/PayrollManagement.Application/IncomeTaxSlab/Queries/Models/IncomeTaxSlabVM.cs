using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.IncomeTaxSlab .Queries.Models
{
    public class IncomeTaxSlabVM
    {
         public Guid? Id {get; set;}
         public string SlabName {get; set;}
         public string Description {get; set;}
         public DateTime? StartDate {get; set;}
         public DateTime? EndDate {get; set;}
         public Boolean IsRunningFlag {get; set;}
         public Boolean IsDeleted {get; set;}
         public Guid? CompanyId {get; set;}
         public decimal? SlabAmount {get; set;}
         public decimal? TaxablePercent {get; set;}
         public string TaxPayerTypeCode { get; set; }
         public string PayerTypeName { get; set; }
        public Int32? SlabOrder {get; set;}
    }
}
