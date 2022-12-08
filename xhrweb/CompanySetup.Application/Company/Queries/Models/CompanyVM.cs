using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Company.Queries.Models
{
    public class CompanyVM
    {
        private string _approvalStatus;
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public string CompanyLocalizedName { get; set; }
        public string CompanySlogan { get; set; }
        public string LicenseKey { get; set; }
        public string CompanyWebsite { get; set; }
        public string FacebookLink { get; set; }
        public uint SortOrder { get; set; }
        public string ApprovalStatusText { get; set; }

        public string CompanyLogoUri { get; set; }
        public string ApprovalStatus
        {
            get { return this._approvalStatus; }
            set
            {
                this._approvalStatus = value;
                ApprovalStatusText = ((ApprovalStatuses)Convert.ToInt32(!string.IsNullOrEmpty(this._approvalStatus) ? this._approvalStatus : "1")).ToString();
            }
        }

        public string Notes { get; set; }
    }
}
