using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeStatus.Queries.Models
{
    public class EmployeeStatusVM
    {
        public Guid Id { get; set; }
        public string EmployeeStatusName { get; set; }
        public string EmployeeStatusNameLC { get; set; }
        public Int16? Rank { get; set; }
        public Int16? StatusId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
