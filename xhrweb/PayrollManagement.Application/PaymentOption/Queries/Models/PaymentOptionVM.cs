using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.PaymentOption .Queries.Models
{
    public class PaymentOptionVM
    {
         public Guid? Id {get; set;}
         public Int16? PaymentOptionId {get; set;}
         public string OptionName {get; set;}
         public string OptionCode {get; set;}
         public Int16? SortOrder {get; set;}
         public Boolean IsDeleted {get; set;}
    }
}
