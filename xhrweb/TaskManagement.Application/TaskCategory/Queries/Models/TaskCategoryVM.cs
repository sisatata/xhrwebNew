using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.TaskCategory .Queries.Models
{
    public class TaskCategoryVM
    {
         public Guid? Id {get; set;}
         public string TaskCategoryName {get; set;}
         public string Remarks {get; set;}
         public Guid? CompanyId {get; set;}
    }
}
