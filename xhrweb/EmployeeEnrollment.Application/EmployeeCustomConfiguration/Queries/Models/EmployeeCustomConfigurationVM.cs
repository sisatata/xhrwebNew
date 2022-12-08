using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries.Models
{
    public class EmployeeCustomConfigurationVM
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string CustomValue { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
