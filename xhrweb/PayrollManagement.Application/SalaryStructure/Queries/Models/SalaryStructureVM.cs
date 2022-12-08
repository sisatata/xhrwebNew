using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.SalaryStructure.Queries.Models
{
    public class SalaryStructureVM
    {
        public Guid? Id { get; set; }
        public string StructureName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
