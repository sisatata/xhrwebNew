using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeStatusHistory .Queries.Models
{
    public class EmployeeStatusHistoryVM
    {
         public Guid Id {get; set;}
         public Guid EmployeeId {get; set;}
         //public Guid StatusId {get; set;}
         public DateTime? ChangedDate {get; set;}
         public string Remarks {get; set;}
    }
}
