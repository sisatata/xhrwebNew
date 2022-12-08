using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class CompanyAddress : BaseEntity<Guid>, IAggregateRoot
    {

        public Guid? CompanyId { get; private set; }
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


        public CompanyAddress(Guid id) : base(id) { }
        private CompanyAddress() : base(Guid.NewGuid()) { }

        public static CompanyAddress Create(


         Guid? companyId,
         string addressLine1,
         string addressLine2,
         string village,
         string postOffice,
         Guid? thanaId,
         Guid? districtId,
         Guid? countryId,
         decimal? latitude,
         decimal? longitude,
         Guid? addressTypeId

        )

        {
            var oModel = new CompanyAddress(Guid.NewGuid());


            oModel.CompanyId = companyId;
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
            oModel.IsDeleted = false;

            return oModel;

        }


        public void UpdateCompanyAddress
            (


         Guid? companyId,
         string addressLine1,
         string addressLine2,
         string village,
         string postOffice,
         Guid? thanaId,
         Guid? districtId,
         Guid? countryId,
         decimal? latitude,
         decimal? longitude,
         Guid? addressTypeId

        )
        {

            CompanyId = companyId;
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
        }


        public void MarkAsDeleteCompanyAddress()
        {
            IsDeleted = true;
        }


    }
}

