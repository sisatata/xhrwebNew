using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeAddress : BaseEntity<Guid>, IAggregateRoot
    {
        //public Guid EmployeeAddressId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string Village { get; private set; }
        public string PostOffice { get; private set; }
        public Guid? ThanaId { get; private set; }
        public Guid? DistrictId { get; private set; }
        public Guid? CountryId { get; private set; }
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public Guid? AddressTypeId { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public EmployeeAddress(Guid id) : base(id) { }
        private EmployeeAddress() : base(Guid.NewGuid()) { }

        public static EmployeeAddress Create(

         //Guid employeeAddressId,
         Guid? employeeId,
         string addressLine1,
         string addressLine2,
         string village,
         string postOffice,
         Guid? thanaId,
         Guid? districtId,
         Guid? countryId,
         decimal? latitude,
         decimal? longitude,
         Guid? addressTypeId,
         Boolean isDeleted

        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(addressLine1, "AddressLine1");
            var oModel = new EmployeeAddress(Guid.NewGuid());

            //oModel.EmployeeAddressId = employeeAddressId;
            oModel.EmployeeId = employeeId;
            oModel.AddressLine1 = addressLine1;
            oModel.AddressLine2 = addressLine2;
            oModel.Village = village;
            oModel.PostOffice = postOffice;
            oModel.ThanaId = thanaId;
            oModel.DistrictId = districtId;
            oModel.CountryId = countryId;
            oModel.Latitude = latitude;
            oModel.Longitude = longitude;
            oModel.AddressTypeId = addressTypeId;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdateEmployeeAddress
            (

         //Guid employeeAddressId,
         Guid? employeeId,
         string addressLine1,
         string addressLine2,
         string village,
         string postOffice,
         Guid? thanaId,
         Guid? districtId,
         Guid? countryId,
         decimal? latitude,
         decimal? longitude,
         Guid? addressTypeId,
         Boolean isDeleted

        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(addressLine1, "AddressLine1");

            //EmployeeAddressId = employeeAddressId;
            EmployeeId = employeeId;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Village = village;
            PostOffice = postOffice;
            ThanaId = thanaId;
            DistrictId = districtId;
            CountryId = countryId;
            Latitude = latitude;
            Longitude = longitude;
            AddressTypeId = addressTypeId;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteEmployeeAddress()
        {
            IsDeleted = true;
        }


    }
}

