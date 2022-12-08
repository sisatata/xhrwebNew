using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using CompanySetup.Core.ExtensionMethods;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CompanySetup.Core.Entities.CompanyAggregate
{
    public class CompanyAggregate : BaseEntity<Guid>, IAggregateRoot

    {
        public CompanyAggregate(IReadOnlyList<Company> companies)
        {
            _companies = companies;
            Company = new Company(Guid.NewGuid());
        }
        public CompanyAggregate(Company company)
        {
            Company = company;
        }

        private IReadOnlyList<Company> _companies = new List<Company>();
        public Company Company { get; set; }


        public void NewCompanyCreate(
                string companyName,
                string shortName,
                string companyLocalizedName,
                string companySlogan,
                string licenseKey,
                uint sortOrder,
                string companyWebsite,
                string facebookLink
        )
        {
            Guard.Against.NullOrWhiteSpace(companyName, "CompanyName");
            Guard.Against.NullOrWhiteSpace(shortName, "ShortName");
            Guard.Against.DuplicateCompanyName(companyName, _companies, "Company name");

            Company.Create(companyName, shortName, companyLocalizedName, companySlogan, licenseKey, sortOrder, companyWebsite, facebookLink);
        }

        public void StartCompanyUpdate(
                string companyName,
                string shortName,
                string companyLocalizedName,
                string companySlogan,
                string licenseKey,
                uint sortOrder,
                string companyWebsite,
                string facebookLink
        )
        {
            Guard.Against.NullOrWhiteSpace(companyName, "CompanyName");
            Guard.Against.NullOrWhiteSpace(shortName, "ShortName");
            Guard.Against.DuplicateCompanyNameUpdateMode(Company.Id, companyName, _companies, "Company name");
            Company.UpdateCompany(companyName, shortName, companyLocalizedName, companySlogan, licenseKey, sortOrder, companyWebsite, facebookLink);
        }
    }
}
