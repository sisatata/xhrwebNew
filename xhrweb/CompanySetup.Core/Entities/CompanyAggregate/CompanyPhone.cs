using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class CompanyPhone : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? CompanyId { get; private set; }
        public string PhoneNumber { get; private set; }
        public Guid? PhoneTypeId { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public CompanyPhone(Guid id) : base(id) { }
        private CompanyPhone() : base(Guid.NewGuid()) { }

        public static CompanyPhone Create(

         Guid? companyId,
         string phoneNumber,
         Guid? phoneTypeId

        )

        {
            var oModel = new CompanyPhone(Guid.NewGuid());

            oModel.CompanyId = companyId;
            oModel.PhoneNumber = phoneNumber;
            oModel.PhoneTypeId = phoneTypeId;
            oModel.IsDeleted = false;
            return oModel;

        }


        public void UpdateCompanyPhone
            (
         Guid? companyId,
         string phoneNumber,
         Guid? phoneTypeId

        )
        {
            CompanyId = companyId;
            PhoneNumber = phoneNumber;
            PhoneTypeId = phoneTypeId;
        }


        public void MarkAsDeleteCompanyPhone()
        {
            IsDeleted = true;
        }


    }
}

