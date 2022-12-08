using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Entities
{
    public class EmployeeDevice : BaseEntity<Guid>
    {
        public string AccessToken { get; private set; }
        //public string Location { get; private set; }
        //public string Device { get; private set; }
        //public string OperatingSystem { get; private set; }
        //public string OSVersion { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public string EmployeeName { get; set; }
        public DateTime JoiningDate { get; set; }
        public Guid? CompanyId { get; private set; }

        // not persisted
        public EmployeeDevice(Guid id) : base(id) { }
        private EmployeeDevice() : base(Guid.NewGuid()) { }
    }
}
