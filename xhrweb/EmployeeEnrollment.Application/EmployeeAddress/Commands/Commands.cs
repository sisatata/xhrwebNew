using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeAddress.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeAddress : IRequest<EmployeeAddressCommandVM>
            {
                public Guid? EmployeeId { get; set; }
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
                public Boolean IsDeleted { get; set; }
            }

            public class UpdateEmployeeAddress : IRequest<EmployeeAddressCommandVM>
            {
                public Guid EmployeeAddressId { get; set; }
                public Guid? EmployeeId { get; set; }
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
                public Boolean IsDeleted { get; set; }
            }

            public class MarkAsDeleteEmployeeAddress : IRequest<EmployeeAddressCommandVM>
            {
                public Guid EmployeeAddressId { get; set; }
            }
        }
    }
}
