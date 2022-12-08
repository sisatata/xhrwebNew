using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeImage .Queries.Models
{
    public class EmployeeImageVM
    {
         public Guid EmployeeImageId {get; set;}
         public Guid? EmployeeId {get; set;}
         public Guid? FamilyMemberId {get; set;}
         public string Photo {get; set;}
         public Guid? PhotoId {get; set;}
         public Guid? CompanyId {get; set;}
         public Boolean IsDeleted {get; set;}
    }
}
