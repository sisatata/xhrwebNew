using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities
{
    public class EmployeeManager : BaseEntity<Guid>
    {
        public Guid EmployeeId { get; private set; }
        public Guid? ManagerId { get; private set; }
        public Boolean IsPrimaryManager { get; private set; }
        public Guid CompanyId { get; private set; }
        public Guid? ManagerTypeId { get; private set; }
        public string ManagerTypeName { get; set; }
        public string ManagerTypeCode { get; set; }
        public EmployeeManager(Guid id) : base(id) { }
        private EmployeeManager() : base(Guid.NewGuid()) { }
    }
}
