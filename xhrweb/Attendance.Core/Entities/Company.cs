using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
    public class Company : BaseEntity<Guid>
    {
        public string CompanyName { get; private set; }
        public string CompanyLocalizedName { get; private set; }
        public string ShortName { get; private set; }
        public string LicenseKey { get; private set; }
        public int SortOrder { get; private set; }
        public string ApprovalStatus { get; private set; }
        public string Notes { get; private set; }
        public string CompanyMaskingId { get; set; }
        public Company(Guid id) : base(id) { }
        private Company() : base(Guid.NewGuid()) { }
    }
}

