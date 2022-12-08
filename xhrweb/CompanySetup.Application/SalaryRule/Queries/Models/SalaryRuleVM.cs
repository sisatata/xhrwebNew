using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.SalaryRule.Queries.Models
{
    public class SalaryRuleVM
    {
        public Guid? Id { get; set; }
        public string RuleName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
