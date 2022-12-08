using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class SalaryStructure : BaseEntity<Guid>
    {
        public string StructureName { get; private set; }
        public string Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public SalaryStructure(Guid id) : base(id) { }
        private SalaryStructure() : base(Guid.NewGuid()) { }

        
    }
}

