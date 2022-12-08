using ASL.Hrms.SharedKernel.Models;
using CompanySetup.Application.Company.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace CompanySetup.Application.Company.Commands
{
    public static class CompanyCommands
    {
        public static class V1
        {
            public class CreateCompany : IRequest<CompanyCommandVM>
            {
                public string CompanyName { get; set; }
                public string ShortName { get; set; }
                public string CompanyLocalizedName { get; set; }
                public string CompanySlogan { get; set; }
                public string LicenseKey { get; set; }
                public uint SortOrder { get; set; }
                public string UserId { get; set; }
                public string Password { get; set; }
                public string EmployeFullName { get; set; }

                public string CompanyWebsite { get; set; }
                public string FacebookLink { get; set; }
            }

            public class UpdateCompany : IRequest<CompanyCommandVM>
            {
                public Guid Id { get; set; }
                public string CompanyName { get; set; }
                public string ShortName { get; set; }
                public string CompanyLocalizedName { get; set; }
                public string CompanySlogan { get; set; }
                public string LicenseKey { get; set; }
                public uint SortOrder { get; set; }
                public string CompanyWebsite { get; set; }
                public string FacebookLink { get; set; }
            }
            public class UpdateCompanyImage : IRequest<UpdateCompanyImageCommandVM>
            {
                public Guid Id { get; set; }              
                public IFormFile CompanyLogo { get; set; }

                public PlanetHRSettings Settings { get; set; }
            }
            public class MarkCompanyAsDelete : IRequest<CompanyCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class DeactivateCompany : IRequest<CompanyCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class ApproveCompanyCommand : IRequest<CompanyCommandVM>
            {
                public Guid Id { get; set; }
                public string Notes { get; set; }
            }
            public class RejectCompanyCommand : IRequest<CompanyCommandVM>
            {
                public Guid Id { get; set; }
                public string Notes { get; set; }
            }
        }
    }
}
