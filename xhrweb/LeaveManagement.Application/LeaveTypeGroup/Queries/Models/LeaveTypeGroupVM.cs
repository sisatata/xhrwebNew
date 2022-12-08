using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveTypeGroup .Queries.Models
{
    public class LeaveTypeGroupVM
    {
         public int Id {get; set;}
         public string LeaveTypeGroupName {get; set;}
         public string LeaveTypeGroupNameLC {get; set;}
         public Guid? CompanyId {get; set;}
         public Boolean IsDeleted {get; set;}
    }
}
