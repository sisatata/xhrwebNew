using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CompanyAddress.Queries.Models
{
    public class CompanyAddressVM
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
        public string ThanaName { get; set; }
        public string DistrictName { get; set; }
        public string CountryName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public Guid? AddressTypeId { get; set; }
        public string AddressTypeName { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
