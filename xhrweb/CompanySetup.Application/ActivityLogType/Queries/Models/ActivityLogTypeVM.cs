using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.ActivityLogType .Queries.Models
{
    public class ActivityLogTypeVM
    {
         public Guid? Id {get; set;}
         public string SystemKeyword {get; set;}
         public string Name {get; set;}
         public Boolean Enabled {get; set;}
    }
}
