using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CompanyAddress.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateCompanyAddress : IRequest<CompanyAddressCommandVM>
            {
                public Guid? CompanyId { get; set; }
                public string AddressLine1 { get; set; }
                public string AddressLine2 { get; set; }
                public string Village { get; set; }
                public string PostOffice { get; set; }
                public Guid? ThanaId { get; set; }
                public Guid? DistrictId { get; set; }
                public Guid? CountryId { get; set; }
                public decimal? Latitude { get; set; }
                public decimal? Longitude { get; set; }
                public Guid? AddressTypeId { get; set; }
            }

            public class UpdateCompanyAddress : IRequest<CompanyAddressCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? CompanyId { get; set; }
                public string AddressLine1 { get; set; }
                public string AddressLine2 { get; set; }
                public string Village { get; set; }
                public string PostOffice { get; set; }
                public Guid? ThanaId { get; set; }
                public Guid? DistrictId { get; set; }
                public Guid? CountryId { get; set; }
                public decimal? Latitude { get; set; }
                public decimal? Longitude { get; set; }
                public Guid? AddressTypeId { get; set; }
            }

            public class MarkAsDeleteCompanyAddress : IRequest<CompanyAddressCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
