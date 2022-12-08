using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeePhone .Queries.Models
{
    public class EmployeePhoneVM
    {
         public Guid Id {get; set;}
         public Guid? EmployeeId {get; set;}
         public string PhoneNumber {get; set;}
         public Guid? PhoneTypeId {get; set;}
    }
}
