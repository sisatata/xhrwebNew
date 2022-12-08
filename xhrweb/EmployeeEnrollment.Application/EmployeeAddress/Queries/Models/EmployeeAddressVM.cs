using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeAddress .Queries.Models
{
    public class EmployeeAddressVM
    {
         public Guid Id {get; set;}
         public Guid? EmployeeId {get; set;}
         public string AddressLine1 {get; set;}
         public string AddressLine2 {get; set;}
         public string Village {get; set;}
         public string PostOffice {get; set;}
         public Guid? ThanaId {get; set;}
         public Guid? DistrictId {get; set;}
         public Guid? CountryId {get; set;}
         public decimal? Latitude {get; set;}
         public decimal? Longitude {get; set;}
         public Guid? AddressTypeId {get; set;}
         public Boolean IsDeleted {get; set;}
    }
}
