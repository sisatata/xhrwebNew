using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.ConfirmationRule.Queries.Models
{
    public class ConfirmationRuleVM
    {
        public Guid? Id { get; set; }
        public string RuleName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int16? ConfirmationType { get; set; }
        public Int16? ConfirmationMonths { get; set; }
        public Int16? SortOrder { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
