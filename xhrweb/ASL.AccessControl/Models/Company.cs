using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.Models
{
    public class Company : BaseEntity<Guid>
    {
        private string _approvalStatus;
        public string CompanyName { get; private set; }
        public string CompanyLocalizedName { get; private set; }
        public string ShortName { get; private set; }
        //public string ApprovalStatusText { get; set; }

        public bool IsActive { get; set; }
        public string ApprovalStatus
        { get; set; }
        public Company(Guid id) : base(id) { }
        private Company() : base(Guid.NewGuid()) { }
    }
}
