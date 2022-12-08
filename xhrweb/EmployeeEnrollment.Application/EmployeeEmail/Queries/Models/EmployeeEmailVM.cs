using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeEmail .Queries.Models
{
    public class EmployeeEmailVM
    {
         public Guid Id {get; set;}
         public Guid EmployeeId { get; set;}
         public string EmailAddress {get; set;}
         public bool IsPrimary {get; set;}
    }
}
