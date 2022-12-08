using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models
{
    public class OfficeNoticeVM
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public Boolean IsGeneral { get; set; }
        public Boolean IsSectionSpecific { get; set; }
        public string ApplicableSections { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsPublished { get; set; }
    }
}
