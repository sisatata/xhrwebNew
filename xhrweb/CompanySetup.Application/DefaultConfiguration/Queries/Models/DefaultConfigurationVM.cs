using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.DefaultConfiguration.Queries.Models
{
    public class DefaultConfigurationVM
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? CustomConfigurationId { get; set; }
        public string CustomValue { get; set; }
        public string CustomDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? CompanyId { get; set; }

    }
}
