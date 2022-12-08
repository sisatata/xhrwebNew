using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CompanyPhone .Queries.Models
{
    public class CompanyPhoneVM
    {
         public Guid? Id {get; set;}
         public Guid? CompanyId {get; set;}
         public string PhoneNumber {get; set;}
         public Guid? PhoneTypeId {get; set;}
    }
}
