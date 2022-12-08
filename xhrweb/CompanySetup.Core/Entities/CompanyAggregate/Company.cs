using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using CompanySetup.Core.ExtensionMethods;
using CompanySetup.Core.Interfaces;
using System;

namespace CompanySetup.Core.Entities.CompanyAggregate
{
    public class Company : BaseEntity<Guid>, IAggregateRoot
    {
        public string CompanyName { get; private set; }
        public string ShortName { get; private set; }
        public string CompanyLocalizedName { get; private set; }
        public string CompanySlogan { get; private set; }
        public string LicenseKey { get; private set; }
        public uint SortOrder { get; private set; }
        public string CompanyMaskingId { get; set; }
        public string ApprovalStatus { get; private set; }
        public string Notes { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsActive { get; private set; }
        public Guid? ParentCompanyId { get; private set; }
        public string CompanyWebsite { get; private set; }
        public string FacebookLink { get; private set; }

        public string CompanyLogoUri { get; private set; }
        public Company(Guid id) : base(id) { }
        private Company() : base(Guid.NewGuid()) { }

        // Factory method to create company
        public void Create(string companyName, string shortName, string companyLocalizedName,
            string companySlogan, string licenseKey, uint sortOrder, string companyWebsite, string facebookLink)
        {

            CompanyName = companyName;
            ShortName = shortName;
            CompanyLocalizedName = companyLocalizedName;
            CompanySlogan = companySlogan;
            LicenseKey = licenseKey;
            SortOrder = sortOrder;
            IsDeleted = false;
            ApprovalStatus = ((int)ApprovalStatuses.Pending).ToString();
            CompanyWebsite = companyWebsite;
            FacebookLink = facebookLink;
            IsActive = true;
        }

        public void UpdateCompany(string companyName, string shortName, string companyLocalizedName,
            string companySlogan, string licenseKey, uint sortOrder, string companyWebsite, string facebookLink)
        {
            CompanyName = companyName;
            ShortName = shortName;
            CompanyLocalizedName = companyLocalizedName;
            CompanySlogan = companySlogan;
            LicenseKey = licenseKey;
            SortOrder = sortOrder;
            CompanyWebsite = companyWebsite;
            FacebookLink = facebookLink;
        }
        public void UpdateCompanyLogo(string companyLogoUri)
        {
            Guard.Against.NullOrWhiteSpace(companyLogoUri, "CompanyLogoUri");

            CompanyLogoUri = companyLogoUri;


        }

        public void MarkCompanyAsDeleted()
        {
            IsDeleted = true;
        }

        public void ApproveCompany(string notes = "")
        {
            Guard.Against.InvalidCompanyStatus(ApprovalStatus, ((int)ApprovalStatuses.Approved).ToString(), "ApprovalStatus", "already approved company");
            ApprovalStatus = ((int)ApprovalStatuses.Approved).ToString();
            Notes = notes;
            IsActive = true;
        }
        public void DeactivateCompany()
        {
            IsActive = false;
        }
        public void RejectCompany(string notes = "")
        {
            Guard.Against.InvalidCompanyStatus(ApprovalStatus, ((int)ApprovalStatuses.Approved).ToString(), "ApprovalStatus", "already approved company");
            Guard.Against.InvalidCompanyStatus(ApprovalStatus, ((int)ApprovalStatuses.Declined).ToString(), "ApprovalStatus", "already declined company");
            ApprovalStatus = ((int)ApprovalStatuses.Declined).ToString();
            Notes = notes;
        }

    }
}
