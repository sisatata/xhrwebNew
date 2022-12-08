using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Company.Queries.Models
{
    public class CompanyVOA
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogoUri { get; set; }
        //public string ShortName { get; set; }
        //public string CompanyLocalizedName { get; set; }
        //public string CompanySlogan { get; set; }
        //public string LicenseKey { get; set; }
        //public uint SortOrder { get; set; }
        //public string ApprovalStatusText { get; set; }
        

        //public string Notes { get; set; }
    }
}
