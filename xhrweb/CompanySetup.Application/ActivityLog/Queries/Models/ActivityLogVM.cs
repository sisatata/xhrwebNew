using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.ActivityLog.Queries.Models
{
    public class ActivityLogVM
    {
        public Guid? Id { get; set; }
        public Guid? ActivityLogTypeId { get; set; }
        public Guid? CustomerId { get; set; }
        public string Comment { get; set; }
    }
}
